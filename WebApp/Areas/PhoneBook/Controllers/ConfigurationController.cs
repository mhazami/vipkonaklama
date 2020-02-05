using System;
using System.Web.Mvc;
using Radyn.PhoneBook;
using Radyn.PhoneBook.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.Areas.PhoneBook.Security.Filter;


namespace Radyn.WebApp.Areas.PhoneBook.Controllers
{
    public class ConfigurationController : PhoneBookBaseController<Configuration>
    {
        
        public ActionResult GetModify(Guid? Id, string culture)
        {
            
            if (string.IsNullOrEmpty(culture))
                culture = SessionParameters.Culture;
            Configuration configuration;
            if (!Id.HasValue) configuration = new Configuration { WebSiteId = SessionParameters.CurrentWebSite.Id };
            else
            {
                configuration =PhoneBookComponenets.Instance.ConfigurationFacade.GetLanuageContent(culture, Id);
               
            }
            return PartialView("PVModify", configuration);
        }
        

        [RadynAuthorize]
        public ActionResult GetConfiguration()
        {

            var config = (SessionParameters.CurrentWebSite != null && SessionParameters.PhoneBookConfiguration!= null && SessionParameters.PhoneBookConfiguration.WebSiteId != Guid.Empty) ? SessionParameters.PhoneBookConfiguration : null;
            return config != null
                ? Redirect("~/PhoneBook/Configuration/Edit?PhoneBookId=" + config.WebSiteId)
                : Redirect("~/PhoneBook/Configuration/Create");

        }

        [RadynAuthorize]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var configuration = new Configuration();
            
            try
            {

                this.RadynTryUpdateModel(configuration);
                configuration.WebSiteId = SessionParameters.CurrentWebSite.Id;
                if (PhoneBookComponenets.Instance.ConfigurationFacade.Insert(configuration))
                {
                    SessionParameters.PhoneBookConfiguration = PhoneBookComponenets.Instance.ConfigurationFacade.Get(SessionParameters.CurrentWebSite.Id);
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return Redirect("~/PhoneBook/Configuration/GetConfiguration");

                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Redirect("~/PhoneBook/Configuration/GetConfiguration");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(configuration);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid PhoneBookId)
        {

            ViewBag.Id = PhoneBookId;
            return View();
        }


        [HttpPost]
        public ActionResult Edit(Guid PhoneBookId, FormCollection collection)
        {
            var configuration = PhoneBookComponenets.Instance.ConfigurationFacade.Get(SessionParameters.CurrentWebSite.Id);
            try
            {
                
                this.RadynTryUpdateModel(configuration);
                
                if (PhoneBookComponenets.Instance.ConfigurationFacade.Update(configuration))
                {
                    SessionParameters.PhoneBookConfiguration = PhoneBookComponenets.Instance.ConfigurationFacade.Get(SessionParameters.CurrentWebSite.Id);
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return Redirect("~/PhoneBook/Configuration/GetConfiguration");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Redirect("~/PhoneBook/Configuration/GetConfiguration");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = PhoneBookId;
                return View(configuration);
            }
        }


       

       
    }
}