using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Common
{
    public static class ConvertDefaultValue
    {
        public static T ConvertValue<T>(object obj, T defaultValue)
        {
            T result = (obj == null || obj == DBNull.Value) ? defaultValue : (T)obj;
            return result;
        }
    }
}
