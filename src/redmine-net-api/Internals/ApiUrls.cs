using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using Redmine.Net.Api.Extensions;
using Redmine.Net.Api.Types;
using Version = Redmine.Net.Api.Types.Version;

namespace Redmine.Net.Api.Internals
{
    internal static class ApiUrls
    {
        private static readonly Dictionary<Type, string> MapTypesToRedminePath = new Dictionary<Type, string>
        {
            {typeof(Issue), "issues"},
            {typeof(Project), "projects"},
            {typeof(User), "users"},
            {typeof(News), "news"},
            {typeof(Query), "queries"},
            {typeof(Version), "versions"},
            {typeof(Attachment), "attachments"},
            {typeof(IssueRelation), "relations"},
            {typeof(TimeEntry), "time_entries"},
            {typeof(IssueStatus), "issue_statuses"},
            {typeof(Tracker), "trackers"},
            {typeof(IssueCategory), "issue_categories"},
            {typeof(Role), "roles"},
            {typeof(ProjectMembership), "memberships"},
            {typeof(Group), "groups"},
            {typeof(TimeEntryActivity), "enumerations/time_entry_activities"},
            {typeof(IssuePriority), "enumerations/issue_priorities"},
            {typeof(Watcher), "watchers"},
            {typeof(IssueCustomField), "custom_fields"},
            {typeof(CustomField), "custom_fields"}
        };


        private static readonly Dictionary<Type, Func<string, int, string, string>> mapToGet = new Dictionary<Type, Func<string, int, string, string>>()
        {
            { typeof(Version), ApiUrls.CreateOrGetVersion},
            { typeof(IssueCategory), ApiUrls.CreateOrGetIssueCategory},
            { typeof(ProjectMembership), ApiUrls.CreateOrGetProjectMembership},
            { typeof(IssueRelation), ApiUrls.CreateOrGetIssueRelation},
            { typeof(File), ApiUrls.CreateOrGetFile}
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="projectId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string CreateOrGetFile(string basePath, int projectId, string format)
        {
            EnsureOwnerIsValid(projectId);

            return $"{basePath}/{RedmineKeys.PROJECTS}/{projectId.ToString(CultureInfo.InvariantCulture)}/{RedmineKeys.FILES}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="issueId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string CreateOrGetIssueRelation(string basePath, int issueId, string format)
        {
            EnsureOwnerIsValid(issueId, "issue");

            return $"{basePath}/{RedmineKeys.ISSUES}/{issueId.ToString(CultureInfo.InvariantCulture)}/{RedmineKeys.RELATIONS}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="projectId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string CreateOrGetVersion(string basePath, int projectId, string format)
        {
            EnsureOwnerIsValid(projectId);

            return $"{basePath}/{RedmineKeys.PROJECTS}/{projectId.ToString(CultureInfo.InvariantCulture)}/{RedmineKeys.VERSIONS}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="projectId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string CreateOrGetIssueCategory(string basePath, int projectId, string format)
        {
            EnsureOwnerIsValid(projectId);

            return $"{basePath}/{RedmineKeys.PROJECTS}/{projectId.ToString(CultureInfo.InvariantCulture)}/{RedmineKeys.ISSUE_CATEGORIES}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="projectId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string CreateOrGetProjectMembership(string basePath, int projectId, string format)
        {
            EnsureOwnerIsValid(projectId);

            return $"{basePath}/{RedmineKeys.PROJECTS}/{projectId.ToString(CultureInfo.InvariantCulture)}/{RedmineKeys.MEMBERSHIPS}.{format}";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basePath"></param>
        /// <param name="id"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string Update<T>(string basePath, string id, string format)
        {
            var type = typeof(T);
            return $"{basePath}/{MapTypesToRedminePath[type]}/{id}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basePath"></param>
        /// <param name="id"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string Update<T>(string basePath, int id, string format)
        {
            var type = typeof(T);
            return $"{basePath}/{MapTypesToRedminePath[type]}/{id.ToString(CultureInfo.InvariantCulture)}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basePath"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string Create<T>(string basePath, string format)
        {
            var type = typeof(T);
            return $"{basePath}/{MapTypesToRedminePath[type]}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basePath"></param>
        /// <param name="id"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string Delete<T>(string basePath, int id, string format)
        {
            var type = typeof(T);
            return $"{basePath}/{MapTypesToRedminePath[type]}/{id.ToString(CultureInfo.InvariantCulture)}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basePath"></param>
        /// <param name="id"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string Get<T>(string basePath, int id, string format)
        {
            var type = typeof(T);
            return $"{basePath}/{MapTypesToRedminePath[type]}/{id.ToString(CultureInfo.InvariantCulture)}.{format}";
        }

        public static string Upload<T>(string basePath, int uploadId, string format)
        {
            EnsureOwnerIsValid(uploadId, "upload");
            var type = typeof(T);
            return $"{basePath}/{MapTypesToRedminePath[type]}/{uploadId.ToString(CultureInfo.InvariantCulture)}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string UploadFile(string basePath, string format)
        {
            return $"{basePath}/{RedmineKeys.UPLOADS}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string CurrentUser(string basePath, string format)
        {
            return $"{basePath}/{RedmineKeys.USERS}/{RedmineKeys.CURRENT}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="issueId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string UpdateIssueAttachment(string basePath, int issueId, string format)
        {
            EnsureOwnerIsValid(issueId, "issue");

            return $"{basePath}/{RedmineKeys.ATTACHMENTS}/{RedmineKeys.ISSUES}/{issueId.ToString(CultureInfo.InvariantCulture)}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="issueId"></param>
        /// <param name="watcherId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string RemoveIssueWatcher(string basePath, int issueId, int watcherId, string format)
        {
            EnsureOwnerIsValid(issueId, "issue");
            EnsureOwnerIsValid(watcherId, "watcher");

            return $"{basePath}/{RedmineKeys.ISSUES}/{issueId.ToString(CultureInfo.InvariantCulture)}/{RedmineKeys.WATCHERS}/{watcherId.ToString(CultureInfo.InvariantCulture)}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="issueId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string AddIssueWatcher(string basePath, int issueId, string format)
        {
            EnsureOwnerIsValid(issueId, "issue");

            return $"{basePath}/{RedmineKeys.ISSUES}/{issueId.ToString(CultureInfo.InvariantCulture)}/{RedmineKeys.WATCHERS}.{format}";
        }

        /// <summary>
        /// Create, Update, Delete - has same url
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="projectId"></param>
        /// <param name="pageName"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string Wiki(string basePath, int projectId, string pageName, string format)
        {
            EnsureOwnerIsValid(projectId);
            EnsureIsNotNullOrWhitespace(pageName);

            return $"{basePath}/{RedmineKeys.PROJECTS}/{projectId.ToString(CultureInfo.InvariantCulture)}/{RedmineKeys.WIKI}/{pageName}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string RemoveGroupUser(string basePath, int groupId, int userId, string format)
        {
            EnsureOwnerIsValid(groupId, "group");
            EnsureOwnerIsValid(userId, "user");

            return $"{basePath}/{RedmineKeys.GROUPS}/{groupId.ToString(CultureInfo.InvariantCulture)}/{RedmineKeys.USERS}/{userId.ToString(CultureInfo.InvariantCulture)}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="groupId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string AddGroupUser(string basePath, int groupId, string format)
        {
            EnsureOwnerIsValid(groupId, "group");

            return $"{basePath}/{RedmineKeys.GROUPS}/{groupId.ToString(CultureInfo.InvariantCulture)}/{RedmineKeys.USERS}.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="projectId"></param>
        /// <param name="pageName"></param>
        /// <param name="version"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetWikiPage(string basePath, int projectId, string pageName, uint version, string format)
        {
            EnsureOwnerIsValid(projectId);
            EnsureIsNotNullOrWhitespace(pageName);

            var uri = version == 0
                ? $"{basePath}/{RedmineKeys.PROJECTS}/{projectId.ToString(CultureInfo.InvariantCulture)}/{RedmineKeys.WIKI}/{pageName}.{format}"
                : $"{basePath}/{RedmineKeys.PROJECTS}/{projectId.ToString(CultureInfo.InvariantCulture)}/{RedmineKeys.WIKI}/{pageName}/{version.ToString(CultureInfo.InvariantCulture)}.{format}";
            return uri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="projectId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetWikis(string basePath, int projectId, string format)
        {
            EnsureOwnerIsValid(projectId);

            return $"{basePath}/{RedmineKeys.PROJECTS}/{projectId.ToString(CultureInfo.InvariantCulture)}/{RedmineKeys.WIKI}/index.{format}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ownerType"></param>
        /// <exception cref="ArgumentException"></exception>
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static void EnsureOwnerIsValid(int id, string ownerType = "project")
        {
            if (id <= 0)
            {
                throw new ArgumentException($"The {ownerType} id must be greater than 0.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="errorMessage"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void EnsureIsNotNullOrWhitespace(string text, string errorMessage = "The wiki page is null or empty.")
        {
            if (text.IsNullOrWhiteSpace())
            {
                throw new ArgumentException(errorMessage);
            }
        }

        internal static string List<T>(string basePath, NameValueCollection parameters, string format)
        {
            string url;
            var type = typeof(T);

            if (mapToGet.ContainsKey(type))
            {
                var id = parameters.GetValue(RedmineKeys.PROJECT_ID);
                url = mapToGet[type].Invoke(basePath, int.Parse(id, NumberStyles.Integer, CultureInfo.InvariantCulture), format);
            }
            else
            {
                url = $"{basePath}/{MapTypesToRedminePath[type]}.{format}";
            }

            return url;
        }
    }
}