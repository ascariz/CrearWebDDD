using CrearWebDDD.CrossCutting.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CrearWebDDD.CrossCutting.Extensions
{
    public static class ClassExtensions
    {
        public static List<Property> GetPropertiesValues<T>(this T source)
        {
            PropertyInfo[] properties = source.GetType().GetProperties();
            var t = source.GetType();
            List<Property> result = new List<Property>();
            var datos = source.GetType().GetCustomAttributes();

            foreach (PropertyInfo prop in properties)
            {
                var attrName = prop.Name;
                var CustomAttributes = prop.CustomAttributes.ToList();
                foreach (var item in CustomAttributes)
                {
                    var name = item.AttributeType.Name;

                }

                var attrType = prop.PropertyType.FullName; // 100
                var attrValue = prop.GetValue(source, null); // "Hello!"

                Property property = new Property()
                {
                    ClassName = t.Name,
                    Name = attrName,
                    Type = attrType,
                    Value = attrValue
                };

                var attrRequired = prop.GetCustomAttributes(typeof(RequiredAttribute), true).Cast<RequiredAttribute>().FirstOrDefault();

                if (attrRequired != null)
                {
                    property.IsRequired = true;
                }

                var attr = prop.GetCustomAttributes(typeof(StringLengthAttribute), true).Cast<StringLengthAttribute>().FirstOrDefault();
                if (attr != null)
                {
                    CustomAttributes customAttributes = new CustomAttributes();
                    customAttributes.Name = "StringLengthAttribute";
                    customAttributes.MaxValue = attr.MaximumLength;
                    customAttributes.MinValue = attr.MinimumLength;
                    property.CustomAttributes = customAttributes;
                }

                result.Add(property);


            }


            //properties.ToList().ForEach(prop =>
            //{
            //    var attrName = prop.Name;
            //    var CustomAttributes = prop.CustomAttributes.ToList();
            //    foreach (var item in CustomAttributes)
            //    {
            //        var dd = item.NamedArguments;
            //    }

            //    var attrType = prop.PropertyType.FullName; // 100
            //    var attrValue = prop.GetValue(source, null); // "Hello!"

            //    var attr = prop.GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>().FirstOrDefault();
            //    if (attr != null)
            //    {
            //        attrName = attr.Name;
            //    }
            //    result.Add(new Property() { ClassName = t.Name, Name = attrName, Type = attrType, Value = attrValue });
            //});
            return result;
        }


        public static List<Property> GetPropertiesValues<T>(this List<T> source)
        {
            List<Property> result = new List<Property>();
            source.ToList().ForEach(item =>
            {
                result.AddRange(item.GetPropertiesValues());
            }
            );
            return result;
        }
    }
}
