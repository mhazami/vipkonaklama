using Radyn.Framework.DbHelper;
using Radyn.Utility;
using Radyn.Web.Html;
using Radyn.Web.Manager;
using Radyn.Web.Mvc.Extentions;
using Radyn.Web.Mvc.UI.Message;
using Radyn.Web.Mvc.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Radyn.WebApp.AppCode.Filter;
using Radyn.WebApp.AppCode.Security;
using MessagebootstrapButtons = Radyn.Web.Mvc.UI.Messagebootstrap.MessagebootstrapButtons;
namespace Radyn.WebApp.AppCode.Base
{


    public abstract class BaseController<T> : BaseController where T : class
    {
      

        public virtual void ClearCustomeViewData()
        {
            var f = this.ViewData.Keys.Where(x => x.StartsWith("CustomeViewBage_")).ToList();
            foreach (var VARIABLE in f)
                this.ViewData.Remove(VARIABLE);
        }
       
        #region TryUpdateModel
        public bool RadynTryUpdateModel(T model)
        {
            return RadynTryUpdateModel<T>(model);
        }
        public bool RadynTryUpdateModel(T model, string cutomePrefixname)
        {
            return RadynTryUpdateModel<T>(model, cutomePrefixname);
        }
        public bool RadynTryUpdateModel(T model, FormCollection collection)
        {
            return RadynTryUpdateModel<T>(model, collection);

        }
        public bool RadynTryUpdateModel(T model, FormCollection collection, string cutomePrefixname)
        {
            return RadynTryUpdateModel<T>(model, collection, cutomePrefixname);
        }




        public bool RadynTryUpdateModel<M>(M model)
        {

            return BaseRadynTryUpdateModel(model, null);
        }
        public bool RadynTryUpdateModel<M>(M model, FormCollection collection) where M : class
        {

            return BaseRadynTryUpdateModel(model, collection, null);
        }
        public bool RadynTryUpdateModel<M>(M model, string cutomePrefixname)
        {

            return BaseRadynTryUpdateModel(model, null);
        }
        public bool RadynTryUpdateModel<M>(M model, FormCollection collection, string cutomePrefixname) where M : class
        {

            return BaseRadynTryUpdateModel(model, collection, null);
        }


        public bool BaseRadynTryUpdateModel<M>(M model, string cutomePrefixname)
        {
            if (model == null)
            {
                return false;
            }

            TryBindModelKey(model, cutomePrefixname);
            this.TryUpdateModelByRadyn(model);
            SetCurrentUICultureName(model);
            return true;
        }
        public bool BaseRadynTryUpdateModel<M>(M model, FormCollection collection, string cutomePrefixname) where M : class
        {
            if (model == null)
            {
                return false;
            }

            TryBindModelKey(model, collection, cutomePrefixname);
            this.TryUpdateModelByRadyn(model, collection);
            SetCurrentUICultureName(model, collection);
            return true;
        }
        private void SetCurrentUICultureName(T model)
        {
            SetCurrentUICultureName<T>(model);

        }

        private void SetCurrentUICultureName<M>(M model)
        {
            string firstOrDefault = Request.Form.AllKeys.FirstOrDefault(x => x.Equals("LanguageId"));
            if (string.IsNullOrEmpty(firstOrDefault))
            {
                return;
            }

            PropertyInfo propertyInfo = model.GetType().GetProperty("CurrentUICultureName");
            if (propertyInfo != null)
            {
                string value = Request.Form[firstOrDefault];
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                propertyInfo.SetValue(model, value);
            }



        }

        private void SetCurrentUICultureName<M>(M model, FormCollection collection)
        {
            if (string.IsNullOrEmpty(collection["LanguageId"]))
            {
                return;
            }

            PropertyInfo propertyInfo = model.GetType().GetProperty("CurrentUICultureName");
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(model, collection["LanguageId"]);
            }
        }
        public object[] GetModelKey(FormCollection formCollection)
        {
            return GetModelKey<T>(formCollection);


        }
        public object[] GetModelKey(FormCollection formCollection, string cutomePrefixname)
        {
            return GetModelKey<T>(formCollection, cutomePrefixname);


        }
        public object[] GetModelKey<M>(FormCollection formCollection)
        {
            return GetModelKey<M>(formCollection, null);
        }
        public object[] GetModelKey<M>(FormCollection formCollection, string cutomePrefixname)
        {
            Type type = typeof(M);
            List<PropertyInfo> objectKeyValues = type.GetTypeProperties().Where(x => x.GetPropertyAttributes().Key != null).ToList();
            List<object> objects = new List<object>();
            foreach (PropertyInfo objectKeyValue in objectKeyValues)
            {
                string name = BaseControllerUtility.GetModelKeyName(objectKeyValue, cutomePrefixname);
                string value = formCollection[name];
                PropertyInfo propertyInfo = type.GetProperty(objectKeyValue.Name);
                if (propertyInfo == null)
                {
                    continue;
                }

                objects.Add(value);
            }
            return objects.ToArray();
        }

      
        



        public void TryBindModelKey<M>(M Model, string cutomePrefixname = null)
        {
            if (Request == null || Request.Form == null)
            {
                return;
            }

            Type type = Model.GetType();
            List<PropertyInfo> objectKeyValues = type.GetTypeProperties().Where(x => x.GetPropertyAttributes().Key != null).ToList();
            foreach (PropertyInfo objectKeyValue in objectKeyValues)
            {
                string name = BaseControllerUtility.GetModelKeyName(objectKeyValue, cutomePrefixname);
                string value = Request.Form[name];
                if (string.IsNullOrEmpty(value)) continue;
                PropertyInfo propertyInfo = type.GetProperty(objectKeyValue.Name);
                if (propertyInfo == null)
                {
                    continue;
                }

                Model.SetValue(value, propertyInfo);
            }
        }
        public void TryBindModelKey<M>(M Model, FormCollection formCollection, string cutomePrefixname = null)
        {
            Type type = Model.GetType();
            List<PropertyInfo> objectKeyValues = type.GetTypeProperties().Where(x => x.GetPropertyAttributes().Key != null).ToList();
            foreach (PropertyInfo objectKeyValue in objectKeyValues)
            {
                string name = BaseControllerUtility.GetModelKeyName(objectKeyValue, cutomePrefixname);
                string value = formCollection[name];
                if (string.IsNullOrEmpty(value)) continue;
                PropertyInfo propertyInfo = type.GetProperty(objectKeyValue.Name);
                if (propertyInfo == null)
                {
                    continue;
                }

                Model.SetValue(value, propertyInfo);
            }
        }
        public virtual void PrepareViewBags(T Model, PageMode pageMode)
        {
            PrepareViewBags<T>(Model, pageMode);

        }
        public virtual void PrepareViewBagsWithPrefixName(T Model, PageMode pageMode, string cutomePrefixname)
        {
            PrepareViewBagsWithPrefixName<T>(Model, pageMode, cutomePrefixname);

        }
        public virtual void PrepareViewBags(T Model, PageMode pageMode, string culture)
        {
            PrepareViewBags<T>(Model, pageMode, culture);

        }
        public virtual void PrepareViewBags(T Model, PageMode pageMode, string culture, string cutomePrefixname)
        {
            PrepareViewBags<T>(Model, pageMode, culture, cutomePrefixname);

        }
        public virtual void PrepareViewBags(T Model, PageMode pageMode, ModifyBehavior modifyBehavior)
        {
            PrepareViewBags<T>(Model, pageMode, modifyBehavior);

        }
        public virtual void PrepareViewBags(T Model, PageMode pageMode, ModifyBehavior modifyBehavior, string cutomePrefixname)
        {
            PrepareViewBags<T>(Model, pageMode, modifyBehavior, cutomePrefixname);

        }
        public virtual void PrepareViewBags(T Model, PageMode pageMode, PageMode controllerStatus)
        {
            PrepareViewBags<T>(Model, pageMode, controllerStatus);

        }
        public virtual void PrepareViewBags(T Model, PageMode pageMode, PageMode controllerStatus, string cutomePrefixname)
        {
            PrepareViewBags<T>(Model, pageMode, controllerStatus, cutomePrefixname);

        }
        public virtual void PrepareViewBags(T Model, PageMode pageMode, ModifyBehavior modifyBehavior, PageMode controllerStatus)
        {
            PrepareViewBags<T>(Model, pageMode, modifyBehavior, controllerStatus);

        }
        public virtual void PrepareViewBags(T Model, PageMode pageMode, ModifyBehavior modifyBehavior, PageMode controllerStatus, string cutomePrefixname)
        {
            PrepareViewBags<T>(Model, pageMode, modifyBehavior, controllerStatus, cutomePrefixname);

        }



        public virtual void PrepareViewBags<M>(M Model, PageMode pageMode, ModifyBehavior modifyBehavior, PageMode controllerStatus)
        {
            PrepareViewBags<M>(Model, pageMode, modifyBehavior, controllerStatus, null);
        }
        public virtual void PrepareViewBags<M>(M Model, PageMode pageMode, ModifyBehavior modifyBehavior, PageMode controllerStatus, string cutomePrefixname)
        {
            SetModelKey<M>(Model, cutomePrefixname);
            SetPageMode<M>(pageMode, cutomePrefixname);
            SetModifyBehaviorStaus<M>(modifyBehavior, cutomePrefixname);
            SetControllerStatus<M>(controllerStatus, cutomePrefixname);

        }
        public virtual void PrepareViewBags<M>(M Model, PageMode pageMode)
        {
            PrepareViewBagsWithPrefixName<M>(Model, pageMode, null);



        }
        public virtual void PrepareViewBagsWithPrefixName<M>(M Model, PageMode pageMode, string cutomePrefixname)
        {
            SetModelKey<M>(Model, cutomePrefixname);
            SetPageMode<M>(pageMode, cutomePrefixname);



        }
        public virtual void PrepareViewBags<M>(M Model, PageMode pageMode, string culture)
        {

            PrepareViewBags<M>(Model, pageMode, culture, null);

        }
        public virtual void PrepareViewBags<M>(M Model, PageMode pageMode, string culture, string cutomePrefixname)
        {
            SetModelKey<M>(Model, cutomePrefixname);
            SetPageMode<M>(pageMode, cutomePrefixname);
            SetCulture<M>(culture, cutomePrefixname);



        }
        public virtual void PrepareViewBags<M>(M Model, PageMode pageMode, PageMode controllerStatus)
        {
            PrepareViewBags<M>(Model, pageMode, controllerStatus, null);


        }
        public virtual void PrepareViewBags<M>(M Model, PageMode pageMode, PageMode controllerStatus, string cutomePrefixname)
        {
            SetModelKey<M>(Model, cutomePrefixname);
            SetPageMode<M>(pageMode, cutomePrefixname);
            SetControllerStatus<M>(controllerStatus, cutomePrefixname);


        }
        public virtual void PrepareViewBags<M>(M Model, PageMode pageMode, ModifyBehavior modifyBehavior)
        {
            PrepareViewBags<M>(Model, pageMode, modifyBehavior, null);


        }
        public virtual void PrepareViewBags<M>(M Model, PageMode pageMode, ModifyBehavior modifyBehavior, string cutomePrefixname)
        {
            SetModelKey<M>(Model, cutomePrefixname);
            SetPageMode<M>(pageMode, cutomePrefixname);
            SetModifyBehaviorStaus<M>(modifyBehavior, cutomePrefixname);


        }
        public void SetCulture(string culture, string cutomePrefixname = null)
        {
            SetCulture<T>(culture, cutomePrefixname);



        }
        public void SetCulture<M>(string culture, string cutomePrefixname = null)
        {
            var pageStausKeyName = BaseControllerUtility.GetCultureKeyName<M>(cutomePrefixname);
            if (!ViewData.ContainsKey(pageStausKeyName)) ViewData.Add(pageStausKeyName, culture);
            else ViewData[pageStausKeyName] = culture;

        }

        public void SetPageMode(PageMode pageMode, string cutomePrefixname = null)
        {
            SetPageMode<T>(pageMode, cutomePrefixname);

        }
        public void SetPageMode<M>(PageMode pageMode, string cutomePrefixname = null)
        {

            var pageStausKeyName = BaseControllerUtility.GetPagModeKeyName<M>(cutomePrefixname);
            if (!ViewData.ContainsKey(pageStausKeyName)) ViewData.Add(pageStausKeyName, pageMode);
            else ViewData[pageStausKeyName] = pageMode;

        }

        public void SetModifyBehaviorStaus(ModifyBehavior behavior, string cutomePrefixname = null)
        {
            SetModifyBehaviorStaus<T>(behavior, cutomePrefixname);

        }
        public void SetModifyBehaviorStaus<M>(ModifyBehavior behavior, string cutomePrefixname = null)
        {
            var pageStausKeyName = BaseControllerUtility.GetModifyBehaviorStausKeyName<M>(cutomePrefixname);
            if (!ViewData.ContainsKey(pageStausKeyName))
                ViewData.Add(pageStausKeyName, behavior);
            else ViewData[pageStausKeyName] = behavior;

        }

        public void SetControllerStatus(PageMode pageMode, string cutomePrefixname = null)
        {
            SetControllerStatus<T>(pageMode, cutomePrefixname);

        }
        public void SetControllerStatus<M>(PageMode pageMode, string cutomePrefixname = null)
        {
            var pageStausKeyName = BaseControllerUtility.GetControllerStatusKeyName<M>(cutomePrefixname);
            if (!ViewData.ContainsKey(pageStausKeyName)) ViewData.Add(pageStausKeyName, pageMode);
            else ViewData[pageStausKeyName] = pageMode;
        }
        public void SetUpdateFormData(bool UpdateFormData, string cutomePrefixname = null)
        {
            SetUpdateFormData<T>(UpdateFormData, cutomePrefixname);

        }
        public void SetUpdateFormData<M>(bool UpdateFormData, string cutomePrefixname = null)
        {
            var pageStausKeyName = BaseControllerUtility.GetUpdateFormDataKeyName<M>(cutomePrefixname);
            if (!ViewData.ContainsKey(pageStausKeyName)) ViewData.Add(pageStausKeyName, UpdateFormData);
            else ViewData[pageStausKeyName] = UpdateFormData;
        }
        public string GetCulture()
        {
            return GetCulture<T>();

        }
        public string GetCulture(string cutomePrefixname)
        {
            return GetCulture<T>(cutomePrefixname);

        }
        public string GetCulture<M>(string cutomePrefixname=null)
        {
            var pageStausKeyName = BaseControllerUtility.GetUpdateFormDataKeyName<M>(cutomePrefixname);
            return ViewData == null || !ViewData.ContainsKey(pageStausKeyName)
                ? SessionParameters.Culture
                : ViewData[pageStausKeyName].ToString();
        }
        public PageMode GetPageMode()
        {
            return GetPageMode<T>();

        }
        public PageMode GetPageMode(string cutomePrefixname)
        {
            return GetPageMode<T>(cutomePrefixname);

        }
        public PageMode GetPageMode<M>(string cutomePrefixname = null)
        {
            var pageStausKeyName = BaseControllerUtility.GetPagModeKeyName<M>(cutomePrefixname);
            return ViewData == null || !ViewData.ContainsKey(pageStausKeyName)
                ? PageMode.None
                : ViewData[pageStausKeyName].ToString().ToEnum<PageMode>();
          
        }
        public ModifyBehavior GetModifyBehaviorMode()
        {
            return GetModifyBehaviorMode<T>();
        }
        public ModifyBehavior GetModifyBehaviorMode(string cutomePrefixname)
        {
            return GetModifyBehaviorMode<T>(cutomePrefixname);
        }
        public ModifyBehavior GetModifyBehaviorMode<M>(string cutomePrefixname = null)
        {

            var pageStausKeyName = BaseControllerUtility.GetModifyBehaviorStausKeyName<M>(cutomePrefixname);
            return ViewData == null || !ViewData.ContainsKey(pageStausKeyName)
                ? ModifyBehavior.SelfModify
                : ViewData[pageStausKeyName].ToString().ToEnum<ModifyBehavior>();

        
        }
        public PageMode GetControllerStatus()
        {
            return GetControllerStatus<T>();
        }
        public PageMode GetControllerStatus(string cutomePrefixname)
        {
            return GetControllerStatus<T>(cutomePrefixname);
        }
        public PageMode GetControllerStatus<M>(string cutomePrefixname = null)
        {
            var pageStausKeyName = BaseControllerUtility.GetControllerStatusKeyName<M>(cutomePrefixname);
            return ViewData == null || !ViewData.ContainsKey(pageStausKeyName)
                ? PageMode.Edit
                : ViewData[pageStausKeyName].ToString().ToEnum<PageMode>();
           
        }


        public bool GetUpdateFormData()
        {
            return GetUpdateFormData<T>();
        }
        public bool GetUpdateFormData(string cutomePrefixname)
        {
            return GetUpdateFormData<T>(cutomePrefixname);
        }
        public bool GetUpdateFormData<M>(string cutomePrefixname = null)
        {
            var pageStausKeyName = BaseControllerUtility.GetUpdateFormDataKeyName<M>(cutomePrefixname);
            return ViewData != null && ViewData.ContainsKey(pageStausKeyName) && ViewData[pageStausKeyName].ToString().ToBool();
           
        }


        public PageMode GetPageMode(FormCollection formCollection, string cutomePrefixname = null)
        {
            return GetPageMode<T>(formCollection, cutomePrefixname);

        }
        public PageMode GetPageMode<M>(FormCollection formCollection, string cutomePrefixname = null)
        {
            GetModifyBehaviorMode<M>(formCollection, cutomePrefixname);
            GetUpdateFormData<M>(formCollection, cutomePrefixname);
            if (string.IsNullOrEmpty(formCollection[BaseControllerUtility.GetPagModeKeyName<M>(cutomePrefixname)])) return PageMode.None;
            SetPageMode<M>(formCollection[BaseControllerUtility.GetPagModeKeyName<M>(cutomePrefixname)].ToEnum<PageMode>());
            return GetPageMode<M>();

        }
        public ModifyBehavior GetModifyBehaviorMode(FormCollection formCollection, string cutomePrefixname = null)
        {
            return GetModifyBehaviorMode<T>(formCollection, cutomePrefixname);
        }
        public ModifyBehavior GetModifyBehaviorMode<M>(FormCollection formCollection, string cutomePrefixname = null)
        {
            if (string.IsNullOrEmpty(formCollection[BaseControllerUtility.GetModifyBehaviorStausKeyName<M>(cutomePrefixname)])) return ModifyBehavior.SelfModify;
            SetModifyBehaviorStaus<M>(formCollection[BaseControllerUtility.GetModifyBehaviorStausKeyName<M>(cutomePrefixname)].ToEnum<ModifyBehavior>());
            return GetModifyBehaviorMode<M>();
        }
        public PageMode GetControllerStatus(FormCollection formCollection, string cutomePrefixname = null)
        {
            return GetControllerStatus<T>(formCollection, cutomePrefixname);
        }
        public PageMode GetControllerStatus<M>(FormCollection formCollection, string cutomePrefixname = null)
        {
            if (string.IsNullOrEmpty(formCollection[BaseControllerUtility.GetControllerStatusKeyName<M>(cutomePrefixname)])) return PageMode.Edit;
            SetControllerStatus<M>(formCollection[BaseControllerUtility.GetControllerStatusKeyName<M>(cutomePrefixname)].ToEnum<PageMode>());
            return GetControllerStatus<M>();
        }
        public bool GetUpdateFormData(FormCollection formCollection, string cutomePrefixname = null)
        {
            return GetUpdateFormData<T>(formCollection, cutomePrefixname);
        }
        public bool GetUpdateFormData<M>(FormCollection formCollection, string cutomePrefixname = null)
        {
            if (string.IsNullOrEmpty(formCollection[BaseControllerUtility.GetUpdateFormDataKeyName<M>(cutomePrefixname)])) return false;
            SetUpdateFormData<M>(formCollection[BaseControllerUtility.GetUpdateFormDataKeyName<M>(cutomePrefixname)].ToBool());
            return GetUpdateFormData<M>();

        }
        public virtual void SetModelKey(T Model)
        {
            SetModelKey<T>(Model);

        }
        public virtual void SetModelKey(T Model, string cutomePrefixname)
        {
            SetModelKey<T>(Model, cutomePrefixname);

        }
        public virtual void SetModelKey<M>(M Model, string cutomePrefixname = null)
        {
            if (Model == null)
            {
                return;
            }

            List<PropertyInfo> objectKeyValues = Model.GetType().GetTypeProperties().Where(x => x.GetPropertyAttributes().Key != null).ToList();
            List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
            foreach (PropertyInfo objectKeyValue in objectKeyValues)
            {
                string name = BaseControllerUtility.GetModelKeyName(objectKeyValue, cutomePrefixname);
                object value = objectKeyValue.GetValue(Model);
                list.Add(new KeyValuePair<string, object>(name, value));
            }

            var modelKeyName = BaseControllerUtility.GetModelKeyName<M>(cutomePrefixname);
            if (!ViewData.ContainsKey(modelKeyName)) ViewData.Add(modelKeyName, list.JsonSerializer());
            else ViewData[modelKeyName] = list.JsonSerializer();

           
         

        }
        #endregion

        public List<T> DataSource
        {
            get
            {
                string name = typeof(T).Name;
                if (RadynSession[name + "DataSource"] == null)
                {
                    RadynSession[name + "DataSource"] = new List<T>();
                }

                return RadynSession[name + "DataSource"] as List<T>;
            }
            set
            {
                string name = GetType().Name.Replace("Controller", "");
                RadynSession[name + "DataSource"] = value;

            }
        }

        public string Culture
        {
            get => GetCulture();
            set => SetCulture(value);
        }

        public PageMode PageMode
        {
            get => GetPageMode();
            set => SetPageMode(value);
        }

        public ModifyBehavior MB
        {
            get => GetModifyBehaviorMode();
            set => SetModifyBehaviorStaus(value);
        }

        public bool UpdateFormData
        {
            get => GetUpdateFormData();
            set => SetUpdateFormData(value);
        }


        public PageMode ControllerStatus
        {
            get => GetControllerStatus();
            set => SetControllerStatus(value);
        }





    }

    public class BaseController : Controller
    {
        public bool IsAuthenticated
        {
            get
            {
                return User != null ? User.Identity.IsAuthenticated : Autherized.IsAuthenticated();
            }
        }
        public SessionManager RadynSession
        {
            get { return Web.HttpContext.Current.Session; }
        }
        public void ShowExceptionMessage(Exception ex, string[] buttonses = null)
        {

            var error = ex.Message;
            if (ex.InnerException != null && Radyn.WebApp.AppCode.Security.Settings.ShowMessageInnerException)
            {
                var list = new List<string>
                {
                    "InnerException : "+ ex.InnerException.Message,
                    "StackTrace : "+ ex.StackTrace
                };
                var messageBody = list.Aggregate("", (current, str) => current + Tag.Li(str));
                error += Environment.NewLine + messageBody;
            }

            this.ShowMessage(error, Resources.Common.MessaageTitle, buttonses, messageIcon: MessageIcon.Error);

        }


        public void ShowMessage(string message,
            string title = "", string[] buttonses = null,
            DisplayMode displayMode = DisplayMode.Modal,
            MessageIcon messageIcon = MessageIcon.Information,
            string details = "")
        {
            MessagebootstrapButtons[] newbuttonses = null;
            if (buttonses != null)
            {
                try
                {
                    var newbuttonseslist = new List<MessagebootstrapButtons>();
                    var index = buttonses.Length;
                    for (int i = 0; i < buttonses.Length; i++)
                    {
                        if (index <= 0)
                            break;
                        MessagebootstrapButtons newbuttons = null;
                        if (i / 2 == 0)
                            newbuttons = new MessagebootstrapButtons();
                        if (newbuttons != null)
                        {
                            newbuttons.Text = buttonses[buttonses.Length - index];
                            newbuttons.ClickEvent = buttonses[buttonses.Length - (index - 1)];
                            newbuttonseslist.Add(newbuttons);
                            index -= 2;

                        }

                    }
                    newbuttonses = newbuttonseslist.ToArray();
                }
                catch
                {


                }
            }
            this.Messagebootstrap(message, title, newbuttonses, displayMode, messageIcon);
        }

        public RedirectResult SubmitRedirect(FormCollection collection, object objectparamets = null)
        {

            var formSubmitAction = string.IsNullOrEmpty(collection["FormSubmitMode"])
                ? FormSubmitAction.Save
                : collection["FormSubmitMode"].ToEnum<FormSubmitAction>();
            var action = "";
            switch (formSubmitAction)
            {
                case FormSubmitAction.Save:
                    action = "Index";
                    break;
                case FormSubmitAction.SaveAndNew:
                    action = "Create";
                    break;
                case FormSubmitAction.SaveAndNewLanguage:
                    var parametrs = "";
                    if (objectparamets != null)
                    {
                        var propertyInfos = objectparamets.GetType().GetProperties();

                        foreach (var propertyInfo in propertyInfos)
                        {
                            if (!string.IsNullOrEmpty(parametrs)) parametrs += "&";
                            parametrs += propertyInfo.Name + "=" + propertyInfo.GetValue(objectparamets, null);
                        }
                    }

                    action = "Edit?" + parametrs;
                    break;

            }
            return Redirect("~/" + this.RouteData.DataTokens["area"] + "/" + this.RouteData.Values["controller"] + "/" + action);
        }
        public RedirectResult SubmitRedirect(FormCollection collection, Dictionary<FormSubmitAction, string> aftersubmitaction, bool urliscomplete = false)
        {

            var formSubmitAction = string.IsNullOrEmpty(collection["FormSubmitMode"])
                 ? FormSubmitAction.Save
                 : collection["FormSubmitMode"].ToEnum<FormSubmitAction>();
            var action = aftersubmitaction[formSubmitAction];
            return Redirect("~" + (urliscomplete ? action : ("/" + this.RouteData.DataTokens["area"] + "/" + this.RouteData.Values["controller"] + action)));
        }


        public string CallBackRedirect(FormCollection collection, object objectparamets = null)
        {

            var formSubmitAction = string.IsNullOrEmpty(collection["FormSubmitMode"])
                 ? FormSubmitAction.Save
                 : collection["FormSubmitMode"].ToEnum<FormSubmitAction>();
            var action = "";
            switch (formSubmitAction)
            {
                case FormSubmitAction.Save:
                    action = "Index";
                    break;
                case FormSubmitAction.SaveAndNew:
                    action = "Create";
                    break;
                case FormSubmitAction.SaveAndNewLanguage:
                    var parametrs = "";
                    if (objectparamets != null)
                    {
                        var propertyInfos = objectparamets.GetType().GetProperties();

                        foreach (var propertyInfo in propertyInfos)
                        {
                            if (!string.IsNullOrEmpty(parametrs)) parametrs += "&";
                            parametrs += propertyInfo.Name + "=" + propertyInfo.GetValue(objectparamets, null);
                        }
                    }
                    action = "Edit?" + parametrs;
                    break;

            }
            return Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + "/" + this.RouteData.DataTokens["area"] + "/" + this.RouteData.Values["controller"] + "/" + action;
        }
        public string CallBackRedirect(FormCollection collection, Dictionary<FormSubmitAction, string> aftersubmitaction, bool urliscomplete = false)
        {

            var formSubmitAction = string.IsNullOrEmpty(collection["FormSubmitMode"])
                  ? FormSubmitAction.Save
                  : collection["FormSubmitMode"].ToEnum<FormSubmitAction>();
            var action = aftersubmitaction[formSubmitAction];
            return Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + (urliscomplete ? action : ("/" + this.RouteData.DataTokens["area"] + "/" + this.RouteData.Values["controller"] + action));
        }

        #region TryUpdateModel
        public bool RadynTryUpdateModel<TModel>(TModel model)
        {
            return this.TryUpdateModelByRadyn(model);
        }

        public bool RadynTryUpdateModel<TModel>(TModel model, FormCollection collection)
        {
            return this.TryUpdateModelByRadyn(model, collection);
        }

        #endregion

        #region RadynRedirectToAction

        public RedirectResult RadynRedirect(string url)
        {
            return this.RedirectByRadyn(url);
        }
        public RedirectResult RadynRedirectToAction(string actionName, string controllerName)
        {
            return this.RedirectToActionByRadyn(actionName, controllerName);
        }
        public RedirectResult RadynRedirectToAction(string actionName, string controllerName, object routeValue)
        {
            return this.RedirectToActionByRadyn(actionName, controllerName, routeValue);
        }
        public RedirectResult RadynRedirectToAction(string actionName, string controllerName, string area)
        {
            return this.RedirectToActionByRadyn(actionName, controllerName, area);
        }
        public RedirectResult RadynRedirectToAction(string actionName, string controllerName, string area, object routeValue)
        {
            return this.RedirectToActionByRadyn(actionName, controllerName, area, routeValue);
        }

        #endregion

        public string BaseUrl()
        {
            return Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/');
        }


    }

    public enum FormSubmitAction
    {
        [Framework.Description("Save", Type = typeof(Resources.Common))]
        Save,
        [Framework.Description("SaveAndNew", Type = typeof(Resources.Common))]
        SaveAndNew,
        [Framework.Description("SaveAndNewLanguage", Type = typeof(Resources.Common))]
        SaveAndNewLanguage
    }




}