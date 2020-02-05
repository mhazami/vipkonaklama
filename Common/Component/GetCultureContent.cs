using System.Collections.Generic;
using System.Reflection;
using Radyn.Framework;
using System.Linq;

namespace Radyn.Common.Component
{
    public static class GetCultureContent
    {
        
       
      
      
        public static Dictionary<string, string> GetCultureKeys<T>(this T obj) where T : class
        {
            if (obj == null) return null;
            var dictionary = new Dictionary<string, string>();
            var key = "";
            var schema = "dbo";
            var attributes = obj.GetType().GetCustomAttributes(typeof(Schema), false);
            if (attributes.Length > 0)
                schema = ((Schema)attributes[0]).SchemaName;
            if (schema != null)
                key += schema + ".";

            key += obj.GetType().Name + ".";
            var list = new List<PropertyInfo>();
            foreach (var propertyInfo in obj.GetType().GetProperties())
            {
                var keyvalue = propertyInfo.GetCustomAttributes(typeof(Key), false);
                if (keyvalue.Length > 0)
                {
                    var value = propertyInfo.GetValue(obj, null);
                    key += value + ".";
                    continue;
                }
                if (propertyInfo.GetCustomAttributes(typeof(MultiLanguage), false).Any())
                    list.Add(propertyInfo);

            }
            foreach (var attribute in list)
            {
                dictionary.Add(attribute.Name, key + attribute.Name);
            }
            return dictionary;
        }
    }
}
