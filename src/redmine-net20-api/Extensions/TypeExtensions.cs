using System;

namespace RedmineClient.Extensions
{
    /// <summary>
    /// /
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool Implements<TInterface>(this Type type) where TInterface : class
        {
            var interfaceType = typeof(TInterface);

            if (!interfaceType.IsInterface)
            {
                throw new InvalidOperationException("Only interfaces can be implemented.");
            }

            return (interfaceType.IsAssignableFrom(type));
        }
    }
}