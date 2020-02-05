using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.FormGenerator.ModelView;

namespace Radyn.FormGenerator.Tools
{
    public static class Extentions
    {
        public static string Serialize(this Object obj)
        {
            var stream1 = new MemoryStream();
            var ser = new DataContractJsonSerializer(obj.GetType());
            ser.WriteObject(stream1, obj);
            stream1.Position = 0;
            var sr = new StreamReader(stream1);
            return sr.ReadToEnd();
        }
        public static List<KeyValuePair<string, string>> GetExpressionList
        {
            get
            {
                var getExpressionList = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", "Email"),
                    new KeyValuePair<string, string>("?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$", "Password"),
                    new KeyValuePair<string, string>(@"^\d+(\.\d\d)?$", "Currency")
                };
                return getExpressionList;
            }
        }
        public static string Serialize(this Controls objects)
        {
            var str = string.Empty;
            foreach (var control in objects)
            {
                str += control.Serialize();
               str += "#";
            }
            return str;
        }
        public static string SerializeListItem(this List<ListItem> objects)
        {
            var str = string.Empty;
            var controls = new Controls();
            controls.AddRange(objects);
            return objects.Count == 0 ? str : controls.Serialize();
        }
        
        public static Controls DeSerialize(string structure)
        {

            var list = new List<Object>();
            var outlist = new Controls();
            if (string.IsNullOrEmpty(structure)) return outlist;
            var strings = structure.Split('#');
            foreach (var s1 in strings)
            {
                if (string.IsNullOrEmpty(s1)) continue;
                var byteArray = Encoding.UTF8.GetBytes(s1);
                var stream = new MemoryStream(byteArray);
                var serializer = new DataContractJsonSerializer(typeof(Control));
                var result = serializer.ReadObject(stream);
                var control = (Control)result;
                if (control.Type == null) continue;
                var type =Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == control.Type);
                if(type==null)continue;
                var serializer2 = new DataContractJsonSerializer(type);
                var memoryStream = new MemoryStream(byteArray);
                var readObject = serializer2.ReadObject(memoryStream);
                list.Add(readObject);
            }
            outlist.AddRange(list.OrderBy(x => ((Control) x).Order).ToList());
            return outlist;
        }

       
       
        public static List<ListItem> DeSerializeListItem(string structure)
        {
            var list = new List<ListItem>();
            if (string.IsNullOrEmpty(structure)) return list;
            var strings = structure.Split('#');
            foreach (var s1 in strings)
            {
                if (string.IsNullOrEmpty(s1)) continue;
                var byteArray = Encoding.UTF8.GetBytes(s1);
                var serializer2 = new DataContractJsonSerializer(typeof(ListItem));
                var memoryStream = new MemoryStream(byteArray);
                var readObject = serializer2.ReadObject(memoryStream);
                list.Add((ListItem) readObject);
            }
            return list.OrderBy(x => x.Order).ToList();
        }
        public const string Splite = "%&#%";
        public const string KeyValueSplite = "%#%";
        public static string GetFormData(Dictionary<string, Object> list)
        {
            var str = "";
            foreach (var controlvalueModel in list)
            {
                if (!string.IsNullOrEmpty(str)) str += Splite;
                str += controlvalueModel.Key + KeyValueSplite + controlvalueModel.Value;
            }
            return str;
        }
        public static Dictionary<string,object> GetControlData(string value)
        {
            var list = new Dictionary<string, object>();
            if (string.IsNullOrEmpty(value)) return list;
            var str = value.Split(new[] { Splite }, StringSplitOptions.None);
            foreach (var keyvalues in str)
            {
                var keyvalue = keyvalues.Split(new[] { KeyValueSplite }, StringSplitOptions.None);
                list.Add(keyvalue[0] , keyvalue[1]);
            }
            return list;
        }

       
    }
}
