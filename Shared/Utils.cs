using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class Utils
    {
        public static string GetPropertyValues(object value)
        {
            
            if (value == null) return string.Empty;
            var str = string.Empty;
            foreach (var property in value?.GetType().GetProperties())
            {
                str += $"{property.GetValue(value)};";
            }
            return str;
        }

        public static string GetPropertiesNames(object obj)
        {
            if (obj == null) return string.Empty;
            var str = string.Empty;
            foreach (var item in obj?.GetType()?.GetProperties())
            {
                str += $"{item.Name};";
            }

            return str;
        }
    }
}
