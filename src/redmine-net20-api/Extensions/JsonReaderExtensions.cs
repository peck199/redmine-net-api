using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using RedmineClient.Exceptions;
using RedmineClient.Internals.Serialization;

namespace RedmineClient.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    internal static partial class JsonExtensions
    {
        private const string INCLUDE_DATE_TIME_FORMAT = "yyyy'-'MM'-'dd HH':'mm':'ss UTC";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static int ReadAsInt(this JsonReader reader)
        {
            return reader.ReadAsInt32().GetValueOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static bool ReadAsBool(this JsonReader reader)
        {
            return reader.ReadAsBoolean().GetValueOrDefault();
        }

        /// <summary>
        /// Reads the element content as nullable date time.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public static DateTime? ReadAsNullableDateTime(this JsonReader reader)
        {
            var content = reader.ReadAsString();

            if (content.IsNullOrWhiteSpace() || !DateTime.TryParse(content, out var result))
            {
                if (!DateTime.TryParseExact(content, INCLUDE_DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return null;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IEnumerable<T> ReadAsEnumerable<T>(this JsonReader reader) where T : class
        {
            if (!typeof(T).Implements<IJsonSerializable>())
            {
                throw new RedmineException($"Entity of type '{typeof(T)}' should implement IJsonSerializable.");
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndArray)
                {
                    break;
                }

                if (reader.TokenType == JsonToken.StartArray)
                {
                    continue;
                }

                var entity = Activator.CreateInstance<T>();

                ((IJsonSerializable)entity).ReadJson(reader);

                var des = entity;

                yield return des;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<T> ReadAsCollection<T>(this JsonReader reader) where T : class
        {
            if (!typeof(T).Implements<IJsonSerializable>())
            {
                throw new RedmineException($"Entity of type '{typeof(T)}' should implement IJsonSerializable.");
            }

            List<T> col = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndArray)
                {
                    break;
                }

                if (reader.TokenType == JsonToken.StartArray)
                {
                    continue;
                }

                var entity = Activator.CreateInstance<T>();

                ((IJsonSerializable)entity).ReadJson(reader);

                var des = entity;

                if (col == null)
                {
                    col = new List<T>();
                }

                col.Add(des);
            }

            return col;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<T> ReadAsArray<T>(this JsonReader reader) where T : class
        {
            if (!typeof(T).Implements<IJsonSerializable>())
            {
                throw new RedmineException($"Entity of type '{typeof(T)}' should implement IJsonSerializable.");
            }

            List<T> col = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndArray)
                {
                    break;
                }

                if (reader.TokenType == JsonToken.PropertyName)
                {
                    break;
                }

                if (reader.TokenType == JsonToken.StartArray)
                {
                    continue;
                }

                var entity = Activator.CreateInstance<T>();

                ((IJsonSerializable)entity).ReadJson(reader);

                var des = entity;

                if (col == null)
                {
                    col = new List<T>();
                }

                col.Add(des);
            }

            return col;
        }
    }
}