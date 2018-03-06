using System.Threading.Tasks;

namespace Wp.CIS.LynkSystems.WebApi.Common
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task AddCacheAsync<T>(string id, T obj) where T: class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        T RetrieveCache<T>(string id, T obj) where T: class;
    }
}