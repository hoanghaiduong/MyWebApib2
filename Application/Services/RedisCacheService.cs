
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using MyWebApi.Application.Interfaces;

namespace MyWebApi.Application.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T GetData<T>(string key)
        {
            var data = _cache.GetString(key);
            if (string.IsNullOrEmpty(data))
            {
                return default;
            }
            //JSON STRING -> OBJECT
            return JsonSerializer.Deserialize<T>(data);
        }

        public void SetData<T>(string key, T data)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(15)
            };
            _cache.SetString(key, JsonSerializer.Serialize(data), options);
        }

    }
}