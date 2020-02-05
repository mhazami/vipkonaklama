using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.Common.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult Filter(Type type)
        {
            var propertyInfos = type.GetProperties().Where(
                propertyInfo =>
                {
                    var customAttributes = propertyInfo.GetCustomAttributes(typeof(Framework.Filter), true);
                    return customAttributes.Length > 0 && customAttributes is Framework.Filter[];
                }).ToList();

//            var uiControls = new List<UIControl>();
//            foreach (var property in propertyInfos)
//            {
//                var filterAttr = property.GetCustomAttributes(typeof(Framework.Filter), false);
//                Framework.Filter filter = null;
//                if (filterAttr.Length > 0)
//                    filter = (Framework.Filter)filterAttr[0];
//                var uiControl = new UIControl { Name = property.Name, Type = this.GetControlType(property) };
//                var layoutAttr = property.GetCustomAttributes(typeof(Framework.Layout), false);
//                if (layoutAttr.Length > 0)
//                    uiControl.Caption = ((Framework.Layout)layoutAttr[0]).Caption;
//                if (!string.IsNullOrEmpty(uiControl.Type))
//                    uiControls.Add(uiControl);
//            }
//            return PartialView("PartialFilter", uiControls);
            return null;
        }

        private string GetControlType(PropertyInfo property)
        {
//            var layoutAttr = property.GetCustomAttributes(typeof(Framework.Assosiation), false);
//            if (layoutAttr.Length > 0)
//                return ControlType.DropDownList.ToString();
//
//            var fullName = property.PropertyType.FullName.ToLower();
//            if (fullName.Contains("string"))
//                return ControlType.TextBox.ToString();
//            if (fullName.Contains("string"))
//                return ControlType.TextBox.ToString();
//            if (fullName.Contains("bool"))
//                return ControlType.CheckBox.ToString();
            return string.Empty;
        }
    }
}
