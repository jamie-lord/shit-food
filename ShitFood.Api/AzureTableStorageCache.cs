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
        private readonly string _accountName;
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

        public AzureTableStorageCache(string accountName, string accountKey, string tableName, string partitionKey)
            : this(tableName, partitionKey)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentNullException("accountName cannot be null or empty");
            }
            if (string.IsNullOrWhiteSpace(accountKey))
            {
                throw new ArgumentNullException("accountKey cannot be null or empty");
            }

            _accountName = accountName;
            _accountKey = accountKey;
            Connect();
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
            if (cachedItem != null && cachedItem.Data != null && ShouldDelete(cachedItem))
            {
                await RemoveAsync(key);
                return null;
            }
            return cachedItem?.Data;
        }

        public void Refresh(string key)
        {
            RefreshAsync(key).Wait();
        }

        public async Task RefreshAsync(string key, CancellationToken token = default)
        {
            var data = await RetrieveAsync(key);
            if (data != null)
            {
                if (ShouldDelete(data))
                {
                    await RemoveAsync(key);
                    return;
                }
            }
        }

        private async Task<CachedItem> RetrieveAsync(string key)
        {
            var op = TableOperation.Retrieve<CachedItem>(_partitionKey, key);
            var result = await _table.ExecuteAsync(op);
            var data = result?.Result as CachedItem;
            return data;
        }

        private bool ShouldDelete(CachedItem data)
        {
            var currentTime = DateTimeOffset.UtcNow;
            if (data.AbsolutExperiation != null && data.AbsolutExperiation.Value <= currentTime)
            {
                return true;
            }
            if (data.SlidingExperiation.HasValue && data.LastAccessTime.HasValue && data.LastAccessTime.Value.Add(data.SlidingExperiation.Value) < currentTime)
            {
                return true;
            }

            return false;
        }

        public void Remove(string key)
        {
            RemoveAsync(key).Wait();
        }

        public Task RemoveAsync(string key, CancellationToken token = default)
        {
            var op = TableOperation.Delete(new CachedItem(_partitionKey, key));
            return _table.ExecuteAsync(op);
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            SetAsync(key, value, options).Wait();
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            DateTimeOffset? absoluteExpiration = null;
            var currentTime = DateTimeOffset.UtcNow;
            if (options.AbsoluteExpirationRelativeToNow.HasValue)
            {
                absoluteExpiration = currentTime.Add(options.AbsoluteExpirationRelativeToNow.Value);
            }
            else if (options.AbsoluteExpiration.HasValue)
            {
                if (options.AbsoluteExpiration.Value <= currentTime)
                {
                    throw new ArgumentOutOfRangeException(
                       nameof(options.AbsoluteExpiration),
                       options.AbsoluteExpiration.Value,
                       "The absolute expiration value must be in the future.");
                }
                absoluteExpiration = options.AbsoluteExpiration;
            }
            var item = new CachedItem(_partitionKey, key, value) { LastAccessTime = currentTime };
            if (absoluteExpiration.HasValue)
            {
                item.AbsolutExperiation = absoluteExpiration;
            }
            if (options.SlidingExpiration.HasValue)
            {
                item.SlidingExperiation = options.SlidingExpiration;
            }
            var op = TableOperation.InsertOrReplace(item);
            return _table.ExecuteAsync(op);
        }
    }
}
