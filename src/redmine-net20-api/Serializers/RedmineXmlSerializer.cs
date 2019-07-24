/*
   Copyright 2011 - 2019 Adrian Popescu.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using RedmineClient.Exceptions;
using RedmineClient.Extensions;

namespace RedmineClient
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RedmineXmlSerializer : IRedmineSerializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="RedmineException"></exception>
        public T Deserialize<T>(string response) where T : new()
        {
            if (response.IsNullOrWhiteSpace())
            {
                throw new RedmineException("Could not deserialize empty string.");
            }

            try
            {
                return FromXML<T>(response);
            }
            catch (Exception ex)
            {
                throw new RedmineException("Deserialization error.", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="RedmineException"></exception>
        public PaginatedResult<T> DeserializeList<T>(string response) where T : class
        {
            try
            {
                if (response.IsNullOrWhiteSpace())
                {
                    throw new RedmineException("Could not deserialize empty response.");
                }

                return XmlDeserializeList<T>(response);
            }
            catch (Exception ex)
            {
                throw new RedmineException("Deserialization error.", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public RedmineSerializerType Type { get; } = RedmineSerializerType.Xml;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="RedmineException"></exception>
        public int Count<T>(string response) where T : new()
        {
            try
            {
                if (response.IsNullOrWhiteSpace())
                {
                    throw new RedmineException("Could not deserialize empty response.");
                }

                using (TextReader stringReader = new StringReader(response))
                {
                    using (var xmlReader = XmlReader.Create(stringReader))
                    {
                        xmlReader.Read();
                        xmlReader.Read();

                        var totalItems = xmlReader.ReadAttributeAsInt(RedmineKeys.TOTAL_COUNT);

                        return totalItems;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RedmineException("Deserialization error.", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RedmineException"></exception>
        public string Serialize<T>(T obj) where T : class
        {
            if (obj == default(T))
            {
                throw new ArgumentNullException(nameof(Serialize));
            }

            try
            {
                return ToXML(obj);
            }
            catch (Exception ex)
            {
                throw new RedmineException("Serialization error.", ex);
            }
        }

        /// <summary>
        ///     XMLs the deserialize list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        private static PaginatedResult<T> XmlDeserializeList<T>(string response) where T : class
        {
            using (TextReader stringReader = new StringReader(response))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings()
                {
                    IgnoreComments = true,
                    IgnoreProcessingInstructions = true,
                    IgnoreWhitespace = true,
                }))
                {
                    xmlReader.Read();

                    if (xmlReader.NodeType == XmlNodeType.XmlDeclaration)
                    {
                        xmlReader.Read();
                    }

                    while (xmlReader.NodeType != XmlNodeType.Element)
                    {
                        xmlReader.Read();
                    }

                    var totalItems = xmlReader.ReadAttributeAsInt(RedmineKeys.TOTAL_COUNT);
                    var offset = xmlReader.ReadAttributeAsInt(RedmineKeys.OFFSET);
                    var result = xmlReader.ReadElementContentAsCollection<T>();
                    return new PaginatedResult<T>(result, totalItems, offset);
                }
            }
        }

        /// <summary>
        ///     Serializes the specified System.Object and writes the XML document to a string.
        /// </summary>
        /// <typeparam name="T">The type of objects to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>
        ///     The System.String that contains the XML document.
        /// </returns>
        /// <exception cref="InvalidOperationException"></exception>
        // ReSharper disable once InconsistentNaming
        private static string ToXML<T>(T obj) where T : class
        {
            var xws = new XmlWriterSettings { OmitXmlDeclaration = true };
            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, xws))
                {
                    var sr = new XmlSerializer(typeof(T));
                    sr.Serialize(xmlWriter, obj);
                    return stringWriter.ToString();
                }
            }
        }

        /// <summary>
        ///     Deserializes the XML document contained by the specific System.String.
        /// </summary>
        /// <typeparam name="T">The type of objects to deserialize.</typeparam>
        /// <param name="xml">The System.String that contains the XML document to deserialize.</param>
        /// <returns>
        ///     The T object being deserialized.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        ///     An error occurred during deserialization. The original exception is available
        ///     using the System.Exception.InnerException property.
        /// </exception>
        // ReSharper disable once InconsistentNaming
        private static T FromXML<T>(string xml) where T : new()
        {
            using (var text = new StringReader(xml))
            {
                var sr = new XmlSerializer(typeof(T));
                var obj = sr.Deserialize(text);
                if (obj is T)
                {
                    return (T)obj;
                }

                return default(T);
            }
        }
    }
}