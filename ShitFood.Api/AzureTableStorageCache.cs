using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShitFood.Api
{
    public class AzureTableStorageCache : IDistributedCache
    {
        private readonly string _partitionKey;
        private readonly string _accountKey;
        private readonly string _connectionString;
        private readonly string TableName;
        private CloudTableClient _client;
        private CloudTable _table;

        private AzureTableStorageCache(string tableName, string partitionKey)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException("tableName cannot be null or empty");
            }
            if (string.IsNullOrWhiteSpace(partitionKey))
            {
                throw new ArgumentNullException("partitionKey cannot be null or empty");
            }
            TableName = tableName;
            _partitionKey = partitionKey;
        }

        public AzureTableStorageCache(string connectionString, string tableName, string partitionKey)
            : this(tableName, partitionKey)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("Connection string cannot be null or empty");
            }

            _connectionString = connectionString;
            Connect();
        }

        public void Connect()
        {
            ConnectAsync().Wait();
        }

        public async Task ConnectAsync()
        {
            if (_client == null)
            {
                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    var creds = new StorageCredentials(_accountKey, _accountKey);
                    _client = new CloudStorageAccount(creds, true).CreateCloudTableClient();
                }
                else
                {
                    _client = CloudStorageAccount.Parse(_connectionString).CreateCloudTableClient();
                }
            }
            if (_table == null)
            {
                _table = _client.GetTableReference(TableName);
                await _table.CreateIfNotExistsAsync();
            }
        }

        public byte[] Get(string key)
        {
            return GetAsync(key).Result;
        }

        public async Task<byte[]> GetAsync(string key, CancellationToken token = default)
        {
            var cachedItem = await RetrieveAsync(key);
            return cachedItem?.Data;
        }

        public void Refresh(string key)
        {
            RefreshAsync(key).Wait();
        }

        public async Task RefreshAsync(string key, CancellationToken token = default)
        {
            await RetrieveAsync(key);
        }

        private async Task<CachedItem> RetrieveAsync(string key)
        {
            var op = TableOperation.Retrieve<CachedItem>(_partitionKey, key);
            var result = await _table.ExecuteAsync(op);
            var data = result?.Result as CachedItem;
            return data;
        }

        public void Remove(string key)
        {
            RemoveAsync(key).Wait();
        }

        public async Task RemoveAsync(string key, CancellationToken token = default)
        {
            var item = new CachedItem(_partitionKey, key);
            item.ETag = "*";
            var op = TableOperation.Delete(item);
            try
            {
                await _table.ExecuteAsync(op);
            }
            catch (StorageException)
            {
                // Object probably wasn't in cache
            }
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            SetAsync(key, value, options).Wait();
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            var item = new CachedItem(_partitionKey, key, value);
            var op = TableOperation.InsertOrReplace(item);
            return _table.ExecuteAsync(op);
        }
    }
}
