#if !(NET20)
using System.Threading.Tasks;
#else
#endif
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using Redmine.Net.Api.Exceptions;
using Redmine.Net.Api.Extensions;
using Redmine.Net.Api.Internals;
using Redmine.Net.Api.Serialization;

namespace Redmine.Net.Api
{
    public partial class RedmineManager
    {
        private readonly RestClient restClient;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="redmineRestApiSettings"></param>
        public RedmineManager(RedmineRestApiSettings redmineRestApiSettings)
        {
            RedmineRestApiSettings = redmineRestApiSettings;
            restClient = new RestClient(redmineRestApiSettings);
            Serializer = redmineRestApiSettings.ApiSerializationType == RedmineApiSerializationType.Xml ? (IRedmineSerializer)new XmlRedmineSerializer() : new JsonRedmineSerializer();
        }

        /// <summary>
        /// 
        /// </summary>
        public RedmineRestApiSettings RedmineRestApiSettings { get; }

        /// <summary>
        ///     Returns the complete list of objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="include">Optional fetched data.</param>
        /// <remarks>
        /// Optional fetched data:
        ///     Project: trackers, issue_categories, enabled_modules (since 2.6.0)
        ///     Issue: children, attachments, relations, changesets, journals, watchers - Since 2.3.0
        ///     Users: memberships, groups (added in 2.1)
        ///     Groups: users, memberships
        /// </remarks>
        /// <returns>Returns the complete list of objects.</returns>
        public List<T> GetAll<T>(params string[] include) where T : class, new()
        {
            return GetAll<T>(PageSize, 0, include);
        }

        /// <summary>
        ///     Returns the complete list of objects.
        /// </summary>
        /// <typeparam name="T">The type of objects to retrieve.</typeparam>
        /// <param name="parameters">Optional filters and/or optional fetched data.</param>
        /// <returns>
        ///     Returns a complete list of objects.
        /// </returns>
        public List<T> GetAll<T>(NameValueCollection parameters) where T : class, new()
        {
            int totalCount = 0, pageSize = 0, offset = 0;
            var isLimitSet = false;
            List<T> resultList = null;

            if (parameters == null)
            {
                parameters = new NameValueCollection();
            }
            else
            {
                isLimitSet = int.TryParse(parameters[RedmineKeys.LIMIT], out pageSize);
                int.TryParse(parameters[RedmineKeys.OFFSET], out offset);
            }

            if (pageSize == default(int))
            {
                pageSize = PageSize > 0 ? PageSize : DEFAULT_PAGE_SIZE_VALUE;
                parameters.Set(RedmineKeys.LIMIT, pageSize.ToString(CultureInfo.InvariantCulture));
            }

            try
            {
                var hasOffset = typesWithOffset.ContainsKey(typeof(T));
                if (hasOffset)
                {
                    do
                    {
                        parameters.Set(RedmineKeys.OFFSET, offset.ToString(CultureInfo.InvariantCulture));
                        var tempResult = GetPagedResults<T>(parameters);
                        if (tempResult != null)
                        {
                            if (resultList == null)
                            {
                                resultList = new List<T>(tempResult.Items);
                                totalCount = isLimitSet ? pageSize : tempResult.TotalItems;
                            }
                            else
                            {
                                resultList.AddRange(tempResult.Items);
                            }
                        }
                        offset += pageSize;
                    } while (offset < totalCount);
                }
                else
                {
                    var result = GetPagedResults<T>(parameters);
                    if (result != null)
                    {
                        return new List<T>(result.Items);
                    }
                }
            }
            catch (WebException wex)
            {
                wex.HandleWebException(Serializer);
            }
            return resultList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="include"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAll<T>(int limit, int offset, params string[] include) where T : class, new()
        {
            var parameters = new NameValueCollection
            {
                { RedmineKeys.LIMIT, limit.ToString(CultureInfo.InvariantCulture) },
                { RedmineKeys.OFFSET, offset.ToString(CultureInfo.InvariantCulture) }
            };
            if (include != null)
            {
                parameters.Add(RedmineKeys.INCLUDE, string.Join(",", include));
            }

            return GetAll<T>(parameters);
        }

        /// <summary>
        ///     Gets the paginated objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public PagedResults<T> GetPagedResults<T>(NameValueCollection parameters) where T : class, new()
        {

            var rest = new RestRequest { Parameters =parameters
              , Method = HttpVerbs.GET
              , ContentType = RedmineRestApiSettings.ContentType
              , Uri = ApiUrls.List<T>(RedmineRestApiSettings.Host, parameters, RedmineRestApiSettings.Format)}
;

           var result = restClient.Get(  rest);


            if(result.StatusCode == (int)HttpStatusCode.OK)
            {
                return Serializer.DeserializeToPagedResults<T>(result.Content);
            }

            throw new RedmineException("TBD");
        }
    }

}