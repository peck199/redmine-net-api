using Newtonsoft.Json;

namespace RedmineClient
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJsonSerializable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        void WriteJson(JsonWriter writer);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        void ReadJson(JsonReader reader);
    }
}