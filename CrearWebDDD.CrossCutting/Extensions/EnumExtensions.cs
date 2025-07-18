using CrearWebDDD.CrossCutting.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrearWebDDD.CrossCutting.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var attribute = GetFirstOrDefaultAttribute<DisplayNameAttribute>(enumValue);
            return attribute != null ? attribute.DisplayName : string.Empty;
        }
        public static int GetValue(this Enum enumValue)
        {
            var attribute = GetAttributes<ApiValueAttribute>(enumValue);
            var dato = attribute.Select(attribute => attribute.ApiValue).First();
            if (dato.All(char.IsNumber))
                return Convert.ToInt32(dato);
            return 0;
        }

        public static string GenerateQuery(this Enum enumValue)
        {
            var attributes = GetAttributes<ApiValueAttribute>(enumValue);
            IEnumerable<string> values = attributes.Select(attribute => attribute.ApiValue);
            return $"{string.Join(",", values)}";
        }

        #region Private Fields
        private static T GetFirstOrDefaultAttribute<T>(Enum enumValue) where T : Attribute
        {
            var attributes = GetAttributes<T>(enumValue);
            return attributes.FirstOrDefault() as T;
        }

        private static IEnumerable<T> GetAttributes<T>(Enum enumValue) where T : Attribute
        {
            var type = enumValue.GetType();
            var memberInfo = type.GetMember(enumValue.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Cast<T>();
        }
        #endregion
    }
}
