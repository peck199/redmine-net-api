using System;
using RedmineClient.Extensions;

namespace RedmineClient.Internals
{
    internal static class ApiUrls
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static string Create<T>(string basePath)
        {
            var type = typeof(T);
            //TODO: replace 1 with type mapping
            return $"{basePath}/{1}.";
        }

        public static string CreateOrGetFile(string basePath, int projectId)
        {
            EnsureOwnerIsValid(projectId);

            return $"{basePath}/{RedmineKeys.PROJECTS}/{projectId}/{RedmineKeys.FILES}.";
        }

        public static string CreateOrGetIssueRelation(string basePath, int issueId)
        {
            EnsureOwnerIsValid(issueId, "issue");

            return $"{basePath}/{RedmineKeys.ISSUES}/{issueId}/{RedmineKeys.RELATIONS}.";
        }

        public static string CreateOrGetVersion(string basePath, int projectId)
        {
            EnsureOwnerIsValid(projectId);

            return $"{basePath}/{RedmineKeys.PROJECTS}/{projectId}/{RedmineKeys.VERSIONS}.";
        }

        public static string CreateOrGetIssueCategory(string basePath, int projectId)
        {
            EnsureOwnerIsValid(projectId);

            return $"{basePath}/{RedmineKeys.PROJECTS}/{projectId}/{RedmineKeys.ISSUE_CATEGORIES}.";
        }

        public static string CreateOrGetProjectMembership(string basePath, int projectId)
        {
            EnsureOwnerIsValid(projectId);

            return $"{basePath}/{RedmineKeys.PROJECTS}/{projectId}/{RedmineKeys.MEMBERSHIPS}.";
        }
        
        public static string Update<T>(string basePath)
        {

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basePath"></param>
        /// <param name="id">The identifier</param>
        /// <returns></returns>
        public static string Delete<T>(string basePath, int id)
        {
            var type = typeof(T);

            //TODO: replace 1 with type mapping
            return $"{basePath}/{1}/{id}.";
        }

        public static string Get<T>(string basePath, int id)
        {
            var type = typeof(T);

            //TODO: replace 1 with type mapping
            return $"{basePath}/{1}/{id}.";
        }

        public static string Upload(string basePath, int uploadId)
        {
            EnsureOwnerIsValid(uploadId, "upload");
            //TODO: replace 1 with type mapping
            return $"{basePath}/{1}/{uploadId}.";
        }

        public static string UploadFile(string basePath)
        {
            return $"{basePath}/{RedmineKeys.UPLOADS}.";
        }

        public static string CurrentUser(string basePath)
        {
            return $"{basePath}/{RedmineKeys.USERS}/{RedmineKeys.CURRENT}.";
        }

        public static string UpdateIssueAttachment(string basePath, int issueId)
        {
            EnsureOwnerIsValid(issueId, "issue");

            return $"{basePath}/{RedmineKeys.ATTACHMENTS}/{RedmineKeys.ISSUES}/{issueId}.";
        }

        public static string RemoveIssueWatcher(string basePath, int issueId, int watcherId)
        {
            EnsureOwnerIsValid(issueId, "issue");
            EnsureOwnerIsValid(watcherId, "watcher");

            return $"{basePath}/{RedmineKeys.ISSUES}/{issueId}/{RedmineKeys.WATCHERS}/{watcherId}.";
        }

        public static string AddIssueWatcher(string basePath, int issueId)
        {
            EnsureOwnerIsValid(issueId, "issue");

            return $"{basePath}/{RedmineKeys.ISSUES}/{issueId}/{RedmineKeys.WATCHERS}.";
        }

        /// <summary>
        /// Create, Update, Delete - has same url
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="projectId"></param>
        /// <param name="pageName"></param>
        /// <returns></returns>
        public static string Wiki(string basePath, int projectId, string pageName)
        {
            EnsureOwnerIsValid(projectId);
            EnsureIsNotNullOrWhitespace(pageName);

            return $"{basePath}/{RedmineKeys.PROJECTS}/{projectId}/{RedmineKeys.WIKI}/{pageName}.";
        }
        
        public static string RemoveGroupUser(string basePath, int groupId, int userId)
        {
            EnsureOwnerIsValid(groupId, "group");
            EnsureOwnerIsValid(userId, "user");

            return $"{basePath}/{RedmineKeys.GROUPS}/{groupId}/{RedmineKeys.USERS}/{userId}.";
        }

        public static string AddGroupUser(string basePath, int groupId)
        {
            EnsureOwnerIsValid(groupId, "group");

            return $"{basePath}/{RedmineKeys.GROUPS}/{groupId}/{RedmineKeys.USERS}.";
        }

        public static string GetWikiPage(string basePath, int projectId, string pageName, uint version = 0)
        {
            EnsureOwnerIsValid(projectId);
            EnsureIsNotNullOrWhitespace(pageName);

            var uri = version == 0
                ? $"{basePath}/{RedmineKeys.PROJECTS}/{projectId}/{RedmineKeys.WIKI}/{pageName}."
                : $"{basePath}/{RedmineKeys.PROJECTS}/{projectId}/{RedmineKeys.WIKI}/{pageName}/{version}.";
            return uri;
        }

        public static string GetWikis(string basePath, int projectId)
        {
            EnsureOwnerIsValid(projectId);

            return $"{basePath}/{RedmineKeys.PROJECTS}/{projectId}/{RedmineKeys.WIKI}/index.";
        }

        private static void EnsureOwnerIsValid(int id, string ownerType = "project")
        {
            if (id <= 0)
            {
                throw new ArgumentException($"The {ownerType} id must be greater than 0.");
            }
        }

        private static void EnsureIsNotNullOrWhitespace(string text, string errorMessage = "The wiki page is null or empty.")
        {
            if(text.IsNullOrWhiteSpace())
            {
                throw new ArgumentException(errorMessage);
            }
        }
    }
}