using System.Collections.Specialized;
using RedmineClient.Types;

namespace RedmineClient.Internals
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRedmineRestClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        TOut Get<TOut>(string url, NameValueCollection parameters) where TOut : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        PagedResults<TOut> List<TOut>(string url) where TOut : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="serializedData"></param>
        void Post(string url, string serializedData);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="serializedData"></param>
        T Post<T>(string url, string serializedData) where T : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="serializedData"></param>
        void Put(string url, string serializedData);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="serializedData"></param>
        /// <returns></returns>
        T Put<T>(string url, string serializedData) where T : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="serializedData"></param>
        void Patch(string url, string serializedData);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="serializedData"></param>
        /// <returns></returns>
        T Patch<T>(string url, string serializedData) where T : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        void Delete(string url);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        T Delete<T>(string url) where T : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        byte[] Download(string url);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Upload Upload(string address, byte[] data);
    }
}