using Microsoft.Azure.Cosmos.Table;

namespace ShitFood.Api
{
    public class CachedItem : TableEntity
    {
        public CachedItem() { }
        public CachedItem(string partitionKey, string rowKey, byte[] data = null)
            : base(partitionKey, rowKey)
        {
            Data = data;
        }

        public byte[] Data { get; set; }
    }
}
