using System;
using Newtonsoft.Json;
using RedmineClient.Extensions;

namespace RedmineClient.Internals
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class JsonObject : IDisposable
    {
        private readonly bool hasRoot;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="root"></param>
        public JsonObject(JsonWriter writer, string root = null)
        {
            Writer = writer;
            Writer.WriteStartObject();
            if (!root.IsNullOrWhiteSpace())
            {
                hasRoot = true;
                Writer.WritePropertyName(root);
                Writer.WriteStartObject();
            }
        }

        private JsonWriter Writer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Writer.WriteEndObject();
            if (hasRoot)
            {
                Writer.WriteEndObject();
            }
        }
    }
}