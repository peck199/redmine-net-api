using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using RedmineClient.Internals;
using RedmineClient.Internals.Serialization;
using RedmineClient.Types;

namespace RedmineClient
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RedmineApiClient
    {
        private readonly IRedmineApiClientSettings clientSettings;
        private readonly IRedmineSerializer serializer;
        private readonly IRedmineApiRequester redmineApiRequester;

        /// <summary>
        /// 
        /// </summary>
        public RedmineApiClient(IRedmineApiClientSettings clientSettings)
        {
            this.clientSettings = clientSettings;

            if (this.clientSettings.SerializationType == RedmineSerializationType.Json)
            {
                serializer = new JsonRedmineSerializer();
            }
            else
            {
                serializer = new XmlRedmineSerializer();
            }

            redmineApiRequester = new RedmineApiRequester(new RedmineHttpClientFactory(), clientSettings, serializer);
        }

        /// <summary>
        /// Host address of Redmine instance.
        /// </summary>
        public string HostUrl => clientSettings.HostUrl;


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public User GetCurrentUser(params string[] optionalFetchData)
        {
            NameValueCollection parameters = null;
            if (optionalFetchData != null)
            {
                parameters = new NameValueCollection();
                parameters.Add(RedmineKeys.INCLUDE, string.Join(",", optionalFetchData));
            }

            var url = ApiUrls.CurrentUser(clientSettings.HostUrl);
            var user = redmineApiRequester.Get<User>(url, parameters);
            return user;
        }

        //if > 4.0
        //private string help(string str, string key, string value)
        //{
        //    var builder = new UriBuilder("str");
        //    builder.Port = -1;
        //    var query = builder.Uri.ParseQueryString(builder.Query);
        //    query["foo"] = "bar<>&-baz";
        //    query["bar"] = "bazinga";
        //    builder.Query = query.ToString();
        //    string url = builder.ToString();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="id"></param>
        /// <param name="optionalFetchData"></param>
        /// <returns></returns>
        public TOut GetItem<TOut>(int id, params string[] optionalFetchData) where TOut : class, new()
        {
            if (optionalFetchData != null)
            {
                // parameters.Add(RedmineKeys.INCLUDE, string.Join(",", optionalFetchData));
            }
            var url = ApiUrls.Get<TOut>(HostUrl, id);
            return redmineApiRequester.Get<TOut>(url, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <returns></returns>
        public int Count<TOut>() where TOut : class, new()
        {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <returns></returns>
        public List<TOut> GetItems<TOut>(params string[] optionalFetchData) where TOut : class, new()
        {
            if (optionalFetchData != null)
            {
                // parameters.Add(RedmineKeys.INCLUDE, string.Join(",", optionalFetchData));
            }
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="optionalFetchData"></param>
        /// <returns></returns>
        public PagedResults<TOut> GetPagedItems<TOut>(int limit, int offset, params string[] optionalFetchData) where TOut : class, new()
        {
            if (optionalFetchData != null)
            {
                // parameters.Add(RedmineKeys.INCLUDE, string.Join(",", optionalFetchData));
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <returns></returns>
        private List<TOut> GetItems<TOut>(int limit, int offset, params string[] optionalFetchData) where TOut : class, new()
        {
            if (optionalFetchData != null)
            {
                // parameters.Add(RedmineKeys.INCLUDE, string.Join(",", optionalFetchData));
            }
            return null;
        }


    }

    /// <summary>
    /// 
    /// </summary>
    public interface IRedmineAuthentication
    {
        /// <summary>
        /// 
        /// </summary>
        void SetAuthorization();
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class BasicAuthorization : IRedmineAuthentication
    {
        private readonly string username;
        private readonly string password;

        private string base64;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public BasicAuthorization(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetAuthorization()
        {
            if (base64 == null)
            {
                base64 = "";
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ApiKeyAuthorization : IRedmineAuthentication
    {
        private readonly string apiKey;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiKey"></param>
        public ApiKeyAuthorization(string apiKey)
        {
            this.apiKey = apiKey;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetAuthorization()
        {

        }
    }
}
