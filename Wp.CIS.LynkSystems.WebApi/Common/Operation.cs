using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wp.CIS.LynkSystems.WebApi.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class Operation : IOperation
    {
        private readonly IDistributedCache _cache;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        public Operation(IDistributedCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual T RetrieveCache<T>(string id, T obj) where T: class
        {
            //string jsonString = string.Empty;
            //return !string.IsNullOrEmpty(
            //                    jsonString = _cache.GetString(id)) ?
            //                     (T)JsonConvert.DeserializeObject<T>(jsonString) :
            //                     default(T);
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual async Task AddCacheAsync<T>(string id, T obj) where T: class
        {
            //DistributedCacheEntryOptions _options = new DistributedCacheEntryOptions
            //{
            //    AbsoluteExpiration = DateTime.Now.AddMinutes(1)
            //};
            //await _cache.SetStringAsync(id, JsonConvert.SerializeObject(obj), _options);

            await Task.Run(() => 1 + 1);
        }
    }
}
