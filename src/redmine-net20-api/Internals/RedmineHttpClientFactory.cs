using RedmineClient.Internals.Serialization;

namespace RedmineClient.Internals
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class RedmineHttpClientFactory : IRedmineHttpClientFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IRedmineRestClient GetClient(IRedmineApiClientSettings apiClientSettings,IRedmineSerializer serializer)
        {
#if (NET20 || NET40)
            return new RedmineHttpClient(apiClientSettings,serializer);
#else
            //cache the httpClient

            return null;
#endif
        }
    }
}