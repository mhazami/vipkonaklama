using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using Radyn.WebApp.AppCode.Base;

using Radyn.Utility;
using Radyn.Web.Mvc.Utility;
using Radyn.WebApp.AppCode.Filter;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.AppCode
{
    public static class Componenets
    {

        public static PageMode GetPageMode(this HtmlHelper htmlHelper)
        {

            return GetPageMode(htmlHelper, null);


        }
        public static PageMode GetPageMode<M>(this HtmlHelper htmlHelper)
        {
            return GetPageMode(htmlHelper, typeof(M), null);

        }
        public static PageMode GetPageMode(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            var type = GetViewModel(htmlHelper);
            return GetPageMode(htmlHelper, type, cutomePrefixname);

        }

        public static PageMode GetPageMode<M>(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            return GetPageMode(htmlHelper, typeof(M), cutomePrefixname);

        }
        public static PageMode GetPageMode(this HtmlHelper htmlHelper, Type type, string cutomePrefixname)
        {
            var keyName = BaseControllerUtility.GetPagModeKeyName(type, cutomePrefixname);
            if (htmlHelper.ViewData == null || htmlHelper.ViewData[keyName] == null)
                return PageMode.None;
            return htmlHelper.ViewData[keyName].ToString().ToEnum<PageMode>();

        }


        public static string GetPageModeClientId(this HtmlHelper htmlHelper)
        {

            return GetPageModeClientId(htmlHelper, null);


        }
        public static string GetPageModeClientId<M>(this HtmlHelper htmlHelper)
        {
            return GetPageModeClientId(htmlHelper, typeof(M), null);

        }
        public static string GetPageModeClientId(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            var type = GetViewModel(htmlHelper);
            return GetPageModeClientId(htmlHelper, type, cutomePrefixname);

        }

        public static string GetPageModeClientId<M>(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            return GetPageModeClientId(htmlHelper, typeof(M), cutomePrefixname);

        }
        public static string GetPageModeClientId(this HtmlHelper htmlHelper, Type type, string cutomePrefixname)
        {
            return BaseControllerUtility.GetPagModeKeyName(type, cutomePrefixname);
           

        }




        public static bool IsAuthenticated(this HtmlHelper htmlHelper)
        {
            return Autherized.IsAuthenticated();

        }
        public static PageMode GetControllerStatus(this HtmlHelper htmlHelper)
        {
            return GetControllerStatus(htmlHelper, null);

        }
        public static PageMode GetControllerStatus<M>(this HtmlHelper htmlHelper)
        {
            return GetControllerStatus(htmlHelper, typeof(M), null);
            
        }
        public static PageMode GetControllerStatus(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            var type = GetViewModel(htmlHelper);
            return GetControllerStatus(htmlHelper, type, cutomePrefixname);
        }

      
        public static PageMode GetControllerStatus<M>(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            return GetControllerStatus(htmlHelper, typeof(M), cutomePrefixname);



        }
        public static PageMode GetControllerStatus(this HtmlHelper htmlHelper, Type type, string cutomePrefixname)
        {
            var keyName = BaseControllerUtility.GetControllerStatusKeyName(type, cutomePrefixname);
            if (htmlHelper.ViewData == null || htmlHelper.ViewData[keyName] == null)
                return PageMode.Edit;
            return htmlHelper.ViewData[keyName].ToString().ToEnum<PageMode>();



        }


        public static string GetControllerStatusClientId(this HtmlHelper htmlHelper)
        {
            return GetControllerStatusClientId(htmlHelper, null);

        }
        public static string GetControllerStatusClientId<M>(this HtmlHelper htmlHelper)
        {
            return GetControllerStatusClientId(htmlHelper, typeof(M), null);

        }
        public static string GetControllerStatusClientId(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            var type = GetViewModel(htmlHelper);
            return GetControllerStatusClientId(htmlHelper, type, cutomePrefixname);
        }


        public static string GetControllerStatusClientId<M>(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            return GetControllerStatusClientId(htmlHelper, typeof(M), cutomePrefixname);



        }
        public static string GetControllerStatusClientId(this HtmlHelper htmlHelper, Type type, string cutomePrefixname)
        {
           return BaseControllerUtility.GetControllerStatusKeyName(type, cutomePrefixname);
          


        }


        public static ModifyBehavior GetModifyBehavior(this HtmlHelper htmlHelper)
        {

            return GetModifyBehavior(htmlHelper, null);


        }
        public static ModifyBehavior GetModifyBehavior<M>(this HtmlHelper htmlHelper)
        {
            return GetModifyBehavior(htmlHelper, typeof(M), null);

        }
        public static ModifyBehavior GetModifyBehavior(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            var type = GetViewModel(htmlHelper);
            return GetModifyBehavior(htmlHelper, type, cutomePrefixname);

        }

        public static ModifyBehavior GetModifyBehavior<M>(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            return GetModifyBehavior(htmlHelper, typeof(M), cutomePrefixname);

        }
        public static ModifyBehavior GetModifyBehavior(this HtmlHelper htmlHelper, Type type, string cutomePrefixname)
        {
            var keyName = BaseControllerUtility.GetModifyBehaviorStausKeyName(type, cutomePrefixname);
            if (htmlHelper.ViewData == null || htmlHelper.ViewData[keyName] == null)
                return ModifyBehavior.SelfModify;
            return htmlHelper.ViewData[keyName].ToString().ToEnum<ModifyBehavior>();

        }


        public static string GetModifyBehaviorClientId(this HtmlHelper htmlHelper)
        {

            return GetModifyBehaviorClientId(htmlHelper, null);


        }
        public static string GetModifyBehaviorClientId<M>(this HtmlHelper htmlHelper)
        {
            return GetModifyBehaviorClientId(htmlHelper, typeof(M), null);

        }
        public static string GetModifyBehaviorClientId(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            var type = GetViewModel(htmlHelper);
            return GetModifyBehaviorClientId(htmlHelper, type, cutomePrefixname);

        }

        public static string GetModifyBehaviorClientId<M>(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            return GetModifyBehaviorClientId(htmlHelper, typeof(M), cutomePrefixname);

        }
        public static string GetModifyBehaviorClientId(this HtmlHelper htmlHelper, Type type, string cutomePrefixname)
        {
            return BaseControllerUtility.GetModifyBehaviorStausKeyName(type, cutomePrefixname);


        }


        public static string GetPageCulture(this HtmlHelper htmlHelper)
        {

            return GetPageCulture(htmlHelper, null);

        }

        public static string GetPageCulture(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            var type = GetViewModel(htmlHelper);
            return GetPageCulture(htmlHelper, type, cutomePrefixname);

        }
        public static string GetPageCulture<M>(this HtmlHelper htmlHelper)
        {
            return GetPageCulture<M>(htmlHelper, null);

        }
        public static string GetPageCulture<M>(this HtmlHelper htmlHelper, string cutomePrefixname)
        {

            return GetPageCulture(htmlHelper, typeof(M), cutomePrefixname);

        }

        public static string GetPageCulture(this HtmlHelper htmlHelper, Type type, string cutomePrefixname)
        {
            var keyName = BaseControllerUtility.GetCultureKeyName(type, cutomePrefixname);
            if (htmlHelper.ViewData == null || htmlHelper.ViewData[keyName] == null)
                return SessionParameters.Culture;
            return htmlHelper.ViewData[keyName].ToString();



        }



        public static string GetPageCultureClientId(this HtmlHelper htmlHelper)
        {

            return GetPageCultureClientId(htmlHelper, null);

        }

        public static string GetPageCultureClientId(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            var type = GetViewModel(htmlHelper);
            return GetPageCultureClientId(htmlHelper, type, cutomePrefixname);

        }
        public static string GetPageCultureClientId<M>(this HtmlHelper htmlHelper)
        {
            return GetPageCultureClientId<M>(htmlHelper, null);

        }
        public static string GetPageCultureClientId<M>(this HtmlHelper htmlHelper, string cutomePrefixname)
        {

            return GetPageCultureClientId(htmlHelper, typeof(M), cutomePrefixname);

        }

        public static string GetPageCultureClientId(this HtmlHelper htmlHelper, Type type, string cutomePrefixname)
        {
            return BaseControllerUtility.GetCultureKeyName(type, cutomePrefixname);
           



        }


        public static bool UpdateFormData(this HtmlHelper htmlHelper)
        {

            return UpdateFormData(htmlHelper, null);

        }
        public static bool UpdateFormData(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            var type = GetViewModel(htmlHelper);
            return UpdateFormData(htmlHelper, type, cutomePrefixname);

        }
        public static bool UpdateFormData<M>(this HtmlHelper htmlHelper)
        {
            return UpdateFormData<M>(htmlHelper, null);

        }
        public static bool UpdateFormData<M>(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            return UpdateFormData(htmlHelper, typeof(M), cutomePrefixname);




        }
        public static bool UpdateFormData(this HtmlHelper htmlHelper, Type type, string cutomePrefixname)
        {
            var keyName = BaseControllerUtility.GetUpdateFormDataKeyName(type, cutomePrefixname);
            if (htmlHelper.ViewData == null || htmlHelper.ViewData[keyName] == null)
                return false;
            return htmlHelper.ViewData[keyName].ToString().ToBool();



        }

        public static string UpdateFormDataClientId(this HtmlHelper htmlHelper)
        {

            return UpdateFormDataClientId(htmlHelper, null);

        }
        public static string UpdateFormDataClientId(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            var type = GetViewModel(htmlHelper);
            return UpdateFormDataClientId(htmlHelper, type, cutomePrefixname);

        }
        public static string UpdateFormDataClientId<M>(this HtmlHelper htmlHelper)
        {
            return UpdateFormDataClientId<M>(htmlHelper, null);

        }
        public static string UpdateFormDataClientId<M>(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            return UpdateFormDataClientId(htmlHelper, typeof(M), cutomePrefixname);




        }
        public static string UpdateFormDataClientId(this HtmlHelper htmlHelper, Type type, string cutomePrefixname)
        {
            return BaseControllerUtility.GetUpdateFormDataKeyName(type, cutomePrefixname);
         



        }



        public static void PrepareModel(this HtmlHelper htmlHelper)
        {
            var type = GetViewModel(htmlHelper);
            PrepareModel(htmlHelper, type, null);
        }
        public static void PrepareModel<M>(this HtmlHelper htmlHelper)
        {

            PrepareModel<M>(htmlHelper, null);
        }
        public static void PrepareModel(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            var type = GetViewModel(htmlHelper);
            PrepareModel(htmlHelper, type, cutomePrefixname);
        }
        public static void PrepareModel<M>(this HtmlHelper htmlHelper, string cutomePrefixname)
        {
            PrepareModel(htmlHelper, typeof(M), cutomePrefixname);
        }
        public static void PrepareModel(this HtmlHelper htmlHelper, Type type, string cutomePrefixname)
        {
            try
            {
                if (htmlHelper.ViewContext == null || htmlHelper.ViewContext.ViewData == null) return;

                var stringWriter = new StringWriter();
                var html32TextWriter = new Html32TextWriter(stringWriter);
                var keyName = BaseControllerUtility.GetModelKeyName(type, cutomePrefixname);
                var pagModeKeyName = BaseControllerUtility.GetPagModeKeyName(type, cutomePrefixname);
                var modifyBehaviorStausKeyName = BaseControllerUtility.GetModifyBehaviorStausKeyName(type, cutomePrefixname);
                var cultureKeyName = BaseControllerUtility.GetCultureKeyName(type, cutomePrefixname);
                var updateFormDataKeyName = BaseControllerUtility.GetUpdateFormDataKeyName(type, cutomePrefixname);
                var controllerStatusKeyName = BaseControllerUtility.GetControllerStatusKeyName(type, cutomePrefixname);
                if (htmlHelper.ViewContext.ViewData.ContainsKey(keyName))
                {

                    var split = Radyn.Utility.Serialize.JsonDeserialize<List<KeyValuePair<string, object>>>(htmlHelper.ViewContext.ViewData[keyName].ToString());
                    foreach (var pair in split)
                    {
                        html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), pair.Key);
                        html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), pair.Key);
                        html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
                        html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), pair.Value != null ? pair.Value.ToString() : null);
                        html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                        html32TextWriter.RenderEndTag();
                    }

                }
                if (htmlHelper.ViewContext.ViewData.ContainsKey(pagModeKeyName))
                {

                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), pagModeKeyName);
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), pagModeKeyName);
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), htmlHelper.ViewContext.ViewData[pagModeKeyName].ToString());
                    html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                    html32TextWriter.RenderEndTag();
                }
                if (htmlHelper.ViewContext.ViewData.ContainsKey(modifyBehaviorStausKeyName))
                {


                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), modifyBehaviorStausKeyName);
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), modifyBehaviorStausKeyName);
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), htmlHelper.ViewContext.ViewData[modifyBehaviorStausKeyName].ToString());
                    html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                    html32TextWriter.RenderEndTag();

                }

                if (htmlHelper.ViewContext.ViewData.ContainsKey(cultureKeyName))
                {


                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), cultureKeyName);
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), cultureKeyName);
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), htmlHelper.ViewContext.ViewData[cultureKeyName].ToString());
                    html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                    html32TextWriter.RenderEndTag();

                }
                if (htmlHelper.ViewContext.ViewData.ContainsKey(updateFormDataKeyName))
                {

                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), updateFormDataKeyName);
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), updateFormDataKeyName);
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), htmlHelper.ViewContext.ViewData[updateFormDataKeyName].ToString());
                    html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                    html32TextWriter.RenderEndTag();
                }

                if (htmlHelper.ViewContext.ViewData.ContainsKey(controllerStatusKeyName))
                {

                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), controllerStatusKeyName);
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), controllerStatusKeyName);
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), htmlHelper.ViewContext.ViewData[controllerStatusKeyName].ToString());
                    html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                    html32TextWriter.RenderEndTag();

                }
                
                htmlHelper.ViewContext.Writer.Write(stringWriter);



            }
            catch
            {


            }
        }
        public static Type GetViewModel(HtmlHelper htmlHelper)
        {
            var viewDataModel = htmlHelper.GetViewModel();
            if (viewDataModel != null) return viewDataModel.GetType();
            var type1 = htmlHelper.ViewContext.Controller.GetType();
            if (type1.IsGenericType)
                return type1.GetGenericArguments()[0];
            var type = type1.BaseType;
            if (type != null && type.IsGenericType)
                return type.GetGenericArguments()[0];
            return typeof(object);

        }


        public static void WriteReturnValuesHideId(this HtmlHelper htmlHelper)
        {
            try
            {
                if (htmlHelper.ViewContext == null || htmlHelper.ViewContext.ViewBag == null) return;
                var stringWriter = new StringWriter();
                var html32TextWriter = new Html32TextWriter(stringWriter);
                if (htmlHelper.ViewContext.ViewBag.fki != null)
                {

                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "fki");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "fki");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), htmlHelper.ViewContext.ViewBag.fki);
                    html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                    html32TextWriter.RenderEndTag();

                }
                if (htmlHelper.ViewContext.ViewBag.fkd != null)
                {

                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "fkd");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "fkd");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), htmlHelper.ViewContext.ViewBag.fkd);
                    html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                    html32TextWriter.RenderEndTag();

                }
                if (htmlHelper.ViewContext.ViewBag.fkb != null)
                {

                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "fkb");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "fkb");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), htmlHelper.ViewContext.ViewBag.fkb);
                    html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                    html32TextWriter.RenderEndTag();

                }

                htmlHelper.ViewContext.Writer.Write(stringWriter);

            }
            catch
            {


            }
        }
        public static void WriteFormUrl(this HtmlHelper htmlHelper)
        {
            try
            {
                if (htmlHelper.ViewContext == null || htmlHelper.ViewContext.ViewBag == null) return;
                var stringWriter = new StringWriter();
                var html32TextWriter = new Html32TextWriter(stringWriter);
                if (htmlHelper.ViewContext.ViewBag.Viewurl != null)
                {


                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "RadynFormUrl");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "RadynFormUrl");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), htmlHelper.ViewContext.ViewBag.Viewurl.ToString());
                    html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                    html32TextWriter.RenderEndTag();

                }

                htmlHelper.ViewContext.Writer.Write(stringWriter);
            }
            catch
            {


            }
        }


        public static void WriteFormId(this HtmlHelper htmlHelper)
        {
            try
            {
                if (htmlHelper.ViewContext == null || htmlHelper.ViewContext.ViewBag == null) return;
                var stringWriter = new StringWriter();
                var html32TextWriter = new Html32TextWriter(stringWriter);
                if (htmlHelper.ViewContext.ViewBag.FormId != null)
                {

                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "RadynFormId");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "RadynFormId");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
                    html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), htmlHelper.ViewContext.ViewBag.FormId);
                    html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                    html32TextWriter.RenderEndTag();

                }

                htmlHelper.ViewContext.Writer.Write(stringWriter);
            }
            catch
            {


            }
        }


      

    }
}