using RedmineClient.Internals.Serialization;

namespace RedmineClient.Internals
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IRedmineHttpClientFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IRedmineRestClient GetClient(IRedmineApiClientSettings apiClientSettings, IRedmineSerializer serializer);
    }
}