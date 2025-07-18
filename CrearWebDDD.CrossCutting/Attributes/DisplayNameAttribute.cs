using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrearWebDDD.CrossCutting.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class DisplayNameAttribute : Attribute
    {
        public readonly string DisplayName;

        public DisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ApiValueAttribute : Attribute
    {
        public readonly string ApiValue;

        public ApiValueAttribute(string apiValue)
        {
            ApiValue = apiValue;
        }
    }
}
