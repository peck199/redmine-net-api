using System;
using System.Net.Cache;

namespace Redmine.Net.Api.Internals
{
    public static class CachePolicyExtensions
    {
        public static void UseCacheIfAvailable(this RequestCachePolicy requestCachePolicy)
        {
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
            requestCachePolicy = policy;
        }

        public static void DoNotUseCache(this RequestCachePolicy requestCachePolicy)
        {
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            requestCachePolicy = policy;
        }

        public static void OnlyUseCache(this RequestCachePolicy requestCachePolicy)
        {
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheOnly);
            requestCachePolicy = policy;
        }

        public static void DoNotUseLocalCache(this RequestCachePolicy requestCachePolicy)
        {
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Refresh);
            requestCachePolicy = policy;
        }

        public static void SendToServer(this RequestCachePolicy requestCachePolicy)
        {
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Reload);
            requestCachePolicy = policy;
        }

        public static void CheckServer(this RequestCachePolicy requestCachePolicy)
        {
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
            requestCachePolicy = policy;
        }

        public static void SetDefaultTimeBasedPolicy(this RequestCachePolicy requestCachePolicy)
        {
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy();
            requestCachePolicy = policy;
        }

        public static void CreateLastSyncPolicy(this RequestCachePolicy requestCachePolicy, DateTime when)
        {
            var policy = new HttpRequestCachePolicy(when);
            requestCachePolicy = policy;
        }

        public static void CreateMinFreshPolicy(this RequestCachePolicy requestCachePolicy, TimeSpan span)
        {
            var policy = new HttpRequestCachePolicy(HttpCacheAgeControl.MinFresh, span);
            requestCachePolicy = policy;
        }

        public static void CreateFreshAndAgePolicy(this RequestCachePolicy requestCachePolicy, TimeSpan freshMinimum, TimeSpan ageMaximum)
        {
            var policy = new HttpRequestCachePolicy(HttpCacheAgeControl.MaxAgeAndMinFresh, ageMaximum, freshMinimum);
            requestCachePolicy = policy;
        }
    }
}