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

namespace RedmineClient
{
    internal sealed class RedmineSerializerFactory
    {
        private readonly IRedmineSerializer serializer;

        private RedmineSerializerFactory(RedmineSerializerType serializerType)
        {
            if(serializerType == RedmineSerializerType.Xml)
            {
                serializer = new RedmineXmlSerializer();
            }
            else
            {
                serializer = new RedmineJsonSerializer();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializerType"></param>
        /// <returns></returns>
        public static IRedmineSerializer Create(RedmineSerializerType serializerType)
        {
            var serializerFactory = new RedmineSerializerFactory(serializerType);
            return serializerFactory.serializer;
        }
    }
}