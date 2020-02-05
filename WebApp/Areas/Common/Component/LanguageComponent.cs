using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.UI;
using Radyn.Common;
using Radyn.Common.Component;
using Radyn.Common.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Web.Mvc;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Common.Component
{
    public static class LanguageComponent
    {

 








        public enum ComponentMode
        {
            TextBox = 1,
            TextArea = 2,
            Label = 3,
        }
        public static void ImgeControlWithCulture<TModel>(this HtmlHelper helper, TModel obj, Expression<Func<TModel, string>> value, bool download = false, int width = 100, int height = 100)
        where TModel : class
        {
            var s = value.Body.ToString();
            var lastIndexOf = s.LastIndexOf(".");
            var name = s.Substring(lastIndexOf + 1, s.Length - lastIndexOf - 1);
            var stringWriter = new StringWriter();
            var html32TextWriter = new Html32TextWriter(stringWriter);


            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "LHK-" + name);
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "LHK-" + name);
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            html32TextWriter.RenderEndTag();
            


            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), name);
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), name);
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Align.ToString(), "absmiddle");
            var style = "";
            if (width > 0)
                style += "width:" + width + "px;";
            if (height > 0)
                style += "height:" + height + "px;";
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), style);
            var objectkeyskey = obj.GetCultureKeys();
            if (!objectkeyskey.Keys.Contains(name)) return;
            var imagevalue = "/RadynFileHandler/";
            var content =  LanguageContent(obj, name);
            if (content != null)
            {
                html32TextWriter.Write("<script>document.getElementById('LHK-" + name + "').value='" + content.Key + "'</script>");
                imagevalue += content.Value;

            }
            else
            {
                var val = obj.GetType().GetProperty(name).GetValue(obj, null);
                if (val != null)
                {
                    imagevalue += val;
                }
            }
            if (download) imagevalue += "?dl=true";
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Src.ToString(), imagevalue);
            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Img.ToString());
            html32TextWriter.RenderEndTag();
            var culture = content!=null?content.LanguageId:SessionParameters.Culture;
            GenerateDrp(html32TextWriter, culture,
                "$.get('@ApplicationSettings.MVcDnnPath/Common/LanguageContent/ChangeCulture',{key:document.getElementById('LHK-" + name +
                "').value,langid:document.getElementById('LanguageId').value,date: new Date().getTime()},function(data){if(data!=null){document.getElementById('" + name +
                "').src='/RadynFileHandler/'+data" + (download ? "?dl=true" : "") + ";}});");

            var resourceScript = helper.ViewContext.GenerateResourceScript();
            helper.ViewContext.Writer.Write(resourceScript + stringWriter);
        }


        public static void LanguageContentTextBoxFor<TModel>(this HtmlHelper helper, TModel obj, Expression<Func<TModel, string>> value, ComponentMode componentMode = ComponentMode.TextBox, int width = 250, int height = 100, bool showDrp = true,string drpName= "LanguageId", bool showemptyvalue = false)
         where TModel : class
        {
            var s = value.Body.ToString();
            var lastIndexOf = s.LastIndexOf(".");
            var name = s.Substring(lastIndexOf + 1, s.Length - lastIndexOf - 1);
            WriteControl(helper, obj, componentMode, width, height, showDrp, name, drpName, showemptyvalue);
        }


        public static void LanguageContentDisplayFor<TModel>(this HtmlHelper helper, TModel obj,
   Expression<Func<TModel, string>> value, int width = 100,
   int height = 25)
   where TModel : class
        {
            var s = value.Body.ToString();
            var lastIndexOf = s.LastIndexOf(".");
            var name = s.Substring(lastIndexOf + 1, s.Length - lastIndexOf - 1);
            WriteLabel(helper, obj, width, height, name);
        }


        public static void LanguageContentTextBox<TModel>(this HtmlHelper helper, TModel obj, string name, ComponentMode componentMode = ComponentMode.TextBox, int width = 250, int height = 100, bool showDrp = true)
      where TModel : class
        {
            WriteControl(helper, obj, componentMode, width, height, showDrp, name);
        }

        public static void LanguageDropDownList(this HtmlHelper helper, string onchangefunction = null, string Id = "LanguageId", IEnumerable<Language> customeList = null)
        {
            var stringWriter = new StringWriter();
            var html32TextWriter = new Html32TextWriter(stringWriter);


            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "table-row-cap");
            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "item");
            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            html32TextWriter.Write(Resources.Common.Language);
            html32TextWriter.RenderEndTag();
            html32TextWriter.RenderEndTag();

            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "table-row-item");
            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "item");
            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Div.ToString());

            GenerateDrp(html32TextWriter, SessionParameters.Culture, onchangefunction, Id, customeList);


            html32TextWriter.RenderEndTag();
            html32TextWriter.RenderEndTag();




            var resourceScript = helper.ViewContext.GenerateResourceScript();
            helper.ViewContext.Writer.Write(resourceScript + stringWriter);
        }



        private static void WriteControl<TModel>(HtmlHelper helper, TModel obj, ComponentMode componentMode, int width, int height, bool showDrp, string name,string DrpName= "LanguageId",bool showemptyvalue = false) where TModel : class
        {
            var stringWriter = new StringWriter();
            var html32TextWriter = new Html32TextWriter(stringWriter);
            LanguageContent content = null;
            content = LanguageContent(obj, name, showemptyvalue);
            if (!showDrp)
            {
                html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), DrpName);
                html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), DrpName);
                html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
                html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), SessionParameters.Culture);
                html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                html32TextWriter.RenderEndTag();
            }
            else
            {
                var culture = content != null ? content.LanguageId : SessionParameters.Culture;
                GenerateDrp(html32TextWriter, culture,
                    "$.get('/Common/LanguageContent/ChangeCulture',{key:document.getElementById('LHK-" + name +
                    "').value,langid:document.getElementById('LanguageId').value,date: new Date().getTime()},function(data){if(data!=null){document.getElementById('" +
                    name + "').value=data;}});",DrpName);
            }


            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "LHK-" + name);
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "LHK-" + name);
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            html32TextWriter.RenderEndTag();






            var str = "";
            if (content != null)
            {
                str = content.Value;
                html32TextWriter.Write("<script>document.getElementById('LHK-" + name + "').value='" + content.Key + "'</script>");
            }
            else
            {
                var val = obj.GetType().GetProperty(name).GetValue(obj, null);
                if (val != null)
                    str = val.ToString();
            }
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), str);
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), name);
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), name);
            switch (componentMode)
            {
                case ComponentMode.TextBox:

                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "text");
                    html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                    html32TextWriter.RenderEndTag();
                    break;
                case ComponentMode.TextArea:
                    var style = "";
                    if (width > 0)
                        style += "width:" + width + "px;";
                    if (height > 0 && componentMode != ComponentMode.TextBox)
                        style += "height:" + height + "px;";
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), style);
                    html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Textarea.ToString());
                    html32TextWriter.Write(str);
                    html32TextWriter.RenderEndTag();
                    break;


            }
            var resourceScript = helper.ViewContext.GenerateResourceScript();
            helper.ViewContext.Writer.Write(resourceScript + stringWriter);
        }

        private static LanguageContent LanguageContent<TModel>(TModel obj, string name, bool showemptyvalue = false) where TModel : class
        {
            var objectkeyskey = obj.GetCultureKeys();
            if (!objectkeyskey.Keys.Contains(name)) return null;
            var instanceLanguageContentFacade = CommonComponent.Instance.LanguageContentFacade;
            var content  = instanceLanguageContentFacade.Get(objectkeyskey[name], SessionParameters.Culture); ;
            if (!showemptyvalue || (content!=null&&content.Value != null && !string.IsNullOrEmpty(content.Value))) return content;
            var schema = obj.GetType().GetSchema().SchemaName.ToLower();
            var typename = obj.GetType().Name.ToLower();
            var keys = string.Join(",", obj.GetObjectKeyValue());
            content = instanceLanguageContentFacade.FirstOrDefault(
                x => x.Key.ToLower()
                         .Equals((string.Format("{0}.{1}.{2}.{3}", schema, typename, keys.ToLower(), name.ToLower()))) &&
                     x.Value != null && !string.IsNullOrEmpty(x.Value));
            return content;
        }

        private static void GenerateDrp(Html32TextWriter html32TextWriter, string selectedlang, string onchangefunction = null,  string Id = "LanguageId", IEnumerable<Language> customeList = null)
        {
            var listlang = customeList ?? CommonComponent.Instance.LanguageFacade.GetValidList().ToList();
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), Id);
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), Id);
            if (!string.IsNullOrEmpty(onchangefunction))
                html32TextWriter.AddAttribute("onchange", onchangefunction);
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "width:80px;height:25px;vertical-align: top;margin-left: 10px;");
            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Select.ToString());
            foreach (var language in listlang)
            {
                if (language.Id == selectedlang)
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Selected.ToString(), "selected");
                html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), language.Id);
                html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Option.ToString());
                html32TextWriter.Write(language.DisplayName);
                html32TextWriter.RenderEndTag();
            }
            html32TextWriter.RenderEndTag();
        }

        private static void WriteLabel<TModel>(HtmlHelper helper, TModel obj, int width,
            int height, string name)
             where TModel : class
        {
            var stringWriter = new StringWriter();
            var html32TextWriter = new Html32TextWriter(stringWriter);
            var objectkeyskey = obj.GetCultureKeys();
            if (!objectkeyskey.Keys.Contains(name)) return;
            var content  = LanguageContent(obj, name);


            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id, "lbl" + name);
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Value, content.Value);
            //html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width:" + width + "px;" + "height:" + height + "px;");
            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "item");


            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            html32TextWriter.InnerWriter.Write(content.Value);
            html32TextWriter.RenderEndTag();


            helper.ViewContext.Writer.Write(stringWriter);

        }
    }
}