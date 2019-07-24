using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using RedmineClient.Internals;
using RedmineClient.Types;

namespace RedmineClient.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    internal static partial class JsonExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        public static void WriteIdIfNotNull(this JsonWriter jsonWriter, string tag, IdentifiableName value)
        {
            if (value != null)
            {
                jsonWriter.WritePropertyName(tag);
                jsonWriter.WriteValue(value.Id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="ident"></param>
        /// <param name="emptyValue"></param>
        public static void WriteIdOrEmpty(this JsonWriter jsonWriter, string tag, IdentifiableName ident, string emptyValue = null)
        {
            if (ident != null)
            {
                jsonWriter.WriteProperty(tag, ident.Id);
            }
            else
            {
                jsonWriter.WriteProperty(tag, emptyValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="val"></param>
        /// <param name="dateFormat"></param>
        public static void WriteDateOrEmpty(this JsonWriter jsonWriter, string tag, DateTime? val, string dateFormat = "yyyy-MM-dd")
        {
            if (!val.HasValue || val.Value.Equals(default(DateTime)))
            {
                jsonWriter.WriteProperty(tag, string.Empty);
            }
            else
            {
                jsonWriter.WriteProperty(tag, string.Format(NumberFormatInfo.InvariantInfo, "{0}", val.Value.ToString(dateFormat, CultureInfo.InvariantCulture)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="val"></param>
        public static void WriteValueOrEmpty<T>(this JsonWriter jsonWriter, string tag, T? val) where T : struct
        {
            if (!val.HasValue || EqualityComparer<T>.Default.Equals(val.Value, default(T)))
            {
                jsonWriter.WriteProperty(tag, string.Empty);
            }
            else
            {
                jsonWriter.WriteProperty(tag, val.Value.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="val"></param>
        public static void WriteValueOrDefault<T>(this JsonWriter jsonWriter, string tag, T? val) where T : struct
        {
            if (!val.HasValue || EqualityComparer<T>.Default.Equals(val.Value, default(T)))
            {
                jsonWriter.WriteProperty(tag, default(T).ToString());
            }
            else
            {
                jsonWriter.WriteProperty(tag, val.Value.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        public static void WriteProperty(this JsonWriter jsonWriter, string tag, object value)
        {
            jsonWriter.WritePropertyName(tag);
            jsonWriter.WriteValue(value);
        }
        
        public static void WriteIfNotNull<TIn>(this JsonWriter jsonWriter, string tag, TIn value) where TIn: class
        {
            if (value == null)
            {
                return;
            }

            jsonWriter.WritePropertyName(tag);
            jsonWriter.WriteValue(value);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        public static void WriteIfNotDefaultOrNull<TIn>(this JsonWriter jsonWriter, string tag, TIn value)
        {
            if (EqualityComparer<TIn>.Default.Equals(value, default(TIn)))
            {
                return;
            }

            jsonWriter.WritePropertyName(tag);
            
            if (value.GetType() == typeof(bool))
            {
                jsonWriter.WriteValue( value.ToString().ToLowerInvariant());
                return;
            }
            
            jsonWriter.WriteValue(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="collection"></param>
        public static void WriteRepeatableElement(this JsonWriter jsonWriter, string tag, IEnumerable<IValue> collection)
        {
            if (collection == null)
            {
                return;
            }

            jsonWriter.WritePropertyName(tag);
            jsonWriter.WriteStartArray();

            string value = string.Empty;

            foreach (var identifiableName in collection)
            {
                value += "," + identifiableName.Value;
            }

            value = value.Remove(0, 1);
            jsonWriter.WriteValue(value);

            jsonWriter.WriteEndArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="collection"></param>
        public static void WriteArray<T>(this JsonWriter jsonWriter, string tag, ICollection<T> collection) where T : IJsonSerializable
        {
            if (collection == null)
            {
                return;
            }

            jsonWriter.WritePropertyName(tag);
            jsonWriter.WriteStartArray();

            foreach (var item in collection)
            {
                item.WriteJson(jsonWriter);
            }

            jsonWriter.WriteEndArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="collection"></param>
        public static void WriteListAsProperty(this JsonWriter jsonWriter, string tag, ICollection collection)
        {
            if (collection == null)
            {
                return;
            }

            foreach (var item in collection)
            {
                jsonWriter.WriteProperty(tag, item);
            }
        }
    }
}