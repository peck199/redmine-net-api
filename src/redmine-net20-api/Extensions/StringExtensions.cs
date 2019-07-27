/*
   Copyright 2011 - 2019 Adrian Popescu

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
using System.Globalization;

namespace RedmineClient.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
#if NET20
            if (value == null)
            {
                return true;
            }

            for (var index = 0; index < value.Length; ++index)
            {
                if (!char.IsWhiteSpace(value[index]))
                {
                    return false;
                }
            }
            return true;
#else
            return string.IsNullOrWhiteSpace(value);
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maximumLength"></param>
        /// <returns></returns>
        public static string Truncate(this string text, int maximumLength)
        {
            if (!text.IsNullOrWhiteSpace())
            {
                if (text.Length > maximumLength)
                {
                    text = text.Substring(0, maximumLength);
                }
            }

            return text;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Uri FormatUri(this string pattern, params object[] args)
        {
            //Ensure.ArgumentNotNullOrEmptyString(pattern, nameof(pattern));

            return new Uri(string.Format(CultureInfo.InvariantCulture, pattern, args), UriKind.Relative);
        }
    }
}