[assembly:Xamarin.Forms.Dependency(typeof(XFLabsPCL.Services.StorageService))]
namespace XFLabsPCL.Services
{
    using System;
    using System.Threading.Tasks;
    using System.Reactive.Linq;
    using Akavache;

    public class StorageService : IStorageService
    {
        private readonly IBlobCache localBlobCache;

        public StorageService()
        {
            BlobCache.ApplicationName = "xflabspcl";
            localBlobCache = BlobCache.LocalMachine;
        }

        public async Task ClearAsync()
        {
            await localBlobCache.InvalidateAll();
        }

        public void Shutdown()
        {
            BlobCache.Shutdown().Wait();
        }

        public async Task<T> GetOrFetchObjectAsync<T>(string key, Func<Task<T>> fetchFunc, DateTimeOffset? absoluteExpiration = default(DateTimeOffset?))
        {
            return await localBlobCache.GetOrFetchObject(key, fetchFunc, absoluteExpiration);
        }

        public async Task SaveAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class
        {
            if (expiration != null && expiration.HasValue)
                await localBlobCache.InsertObject(key, value, expiration.Value);
            else
                await localBlobCache.InsertObject(key, value);
        }
    }
}
