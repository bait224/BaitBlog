using System;

namespace FA.JustBlog.Core.Common
{
    public static class ExtendType
    {
        /// <summary>
        /// Get default value of type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object GetDefaultValue(this Type t)
        {
            return (t.IsValueType && Nullable.GetUnderlyingType(t) == null) ? Activator.CreateInstance(t) : null;
        }
    }
}
