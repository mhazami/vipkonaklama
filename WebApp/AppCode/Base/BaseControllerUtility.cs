using System;
using System.Linq;
using System.Reflection;

namespace Radyn.WebApp.AppCode.Base
{
    public class BaseControllerUtility
    {
        public static string GetModelKeyName(PropertyInfo objectKeyValue, string cutomePrefixname = null)
        {
            string DeclaringTypeame = string.Empty;
            if (objectKeyValue.DeclaringType != null)
            {
                DeclaringTypeame = GetTypeName(objectKeyValue.DeclaringType);
            }

            return DeclaringTypeame + (string.IsNullOrEmpty(cutomePrefixname) ? "" : "_" + cutomePrefixname) + "_" + objectKeyValue.Name;

        }
        public static string GetModelKeyName<M>(string cutomePrefixname = null)
        {
            return GetModelKeyName(GetValidType(typeof(M)), cutomePrefixname);
          
        }
        public static string GetPagModeKeyName<M>(string cutomePrefixname = null)
        {
            return GetPagModeKeyName(GetValidType(typeof(M)), cutomePrefixname);
        }
        public static string GetCultureKeyName<M>(string cutomePrefixname = null)
        {
            return GetCultureKeyName(GetValidType(typeof(M)), cutomePrefixname);
        }
        public static string GetUpdateFormDataKeyName<M>(string cutomePrefixname = null)
        {
            return GetUpdateFormDataKeyName(GetValidType(typeof(M)), cutomePrefixname);
        }
        public static string GetModifyBehaviorStausKeyName<M>(string cutomePrefixname = null)
        {
            return GetModifyBehaviorStausKeyName(GetValidType(typeof(M)), cutomePrefixname);
        }
        public static string GetControllerStatusKeyName<M>(string cutomePrefixname = null)
        {
            return GetControllerStatusKeyName(GetValidType(typeof(M)), cutomePrefixname);
        }
        private static Type GetValidType(Type type)
        {
            if (type.FullName.Contains("System.Collections.Generic.List"))
                return type.GetGenericArguments().Single();
            return type;
        }
        public static string GetTypeName(Type type)
        {
            return GetValidType(type).FullName.Replace(".", "_");
        }
        public static string GetModelKeyName(Type type, string cutomePrefixname = null)
        {
            return "CustomeViewBage_"+ GetTypeName(type) + (string.IsNullOrEmpty(cutomePrefixname) ? "" : "_" + cutomePrefixname) + "_ModelKey";
        }
      
        public static string GetPagModeKeyName(Type type, string cutomePrefixname = null)
        {
            return "CustomeViewBage_"+ GetTypeName(type) + (string.IsNullOrEmpty(cutomePrefixname) ? "" : "_" + cutomePrefixname) + "_PageModeStatus";
        }
        public static string GetCultureKeyName(Type type, string cutomePrefixname = null)
        {
            return "CustomeViewBage_" + GetTypeName(type) + (string.IsNullOrEmpty(cutomePrefixname) ? "" : "_" + cutomePrefixname) + "_Culture";
        }
        public static string GetUpdateFormDataKeyName(Type type, string cutomePrefixname = null)
        {
            return "CustomeViewBage_" + GetTypeName(type) + (string.IsNullOrEmpty(cutomePrefixname) ? "" : "_" + cutomePrefixname) + "_UpdateFormData";
        }
        public static string GetModifyBehaviorStausKeyName(Type type, string cutomePrefixname = null)
        {
            return "CustomeViewBage_" + GetTypeName(type) + (string.IsNullOrEmpty(cutomePrefixname) ? "" : "_" + cutomePrefixname) + "_ModifyBehaviorStaus";
        }
        public static string GetControllerStatusKeyName(Type type, string cutomePrefixname = null)
        {
            return "CustomeViewBage_" + GetTypeName(type) + (string.IsNullOrEmpty(cutomePrefixname) ? "" : "_" + cutomePrefixname) + "_ControllerStatus";
        }

       

      
    }
}