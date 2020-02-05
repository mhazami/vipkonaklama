using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.ContentManager;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.AppCode.Filter;
using Radyn.WebApp.Areas.WebDesign.Tools;
using Radyn.WebDesign;
using Radyn.WebDesign.DataStructure;
using Radyn.WebDesign.Definition;


namespace Radyn.WebApp.Areas.WebDesign.Controllers
{
    public class ConfigurationController : WebDesignBaseController
    {
       
        [RadynAuthorize]
        [WebDesignHost]
        public ActionResult GetConfiguration()
        {

            var config = (WebSite != null && WebSite.Configuration != null && WebSite.Configuration.Id != Guid.Empty) ? WebSite.Configuration : null;
            return config != null
                ? Redirect("~/WebDesign/Configuration/Edit?Id=" + config.Id)
                : Redirect("~/WebDesign/Configuration/Create");


          
        }
        public ActionResult GetModify(Guid? Id)
        {
            ViewBag.InformTypes = new SelectList(
                EnumUtils.ConvertEnumToIEnumerableInLocalization<Enums.UserInformType>().Select(
                    keyValuePair =>
                        new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Enums.UserInformType>(),
                            keyValuePair.Value)), "Key", "Value");
            
            ViewBag.Contents = new SelectList(ContentManagerComponent.Instance.WebDesignContentFacade.GetByWebSiteId(this.WebSite.Id, true), "Id", "Title");
            ViewBag.Container =new SelectList(ContentManagerComponent.Instance.WebDesignContainerFacade.SelectKeyValuePair(c => c.WebSiteContainer.Id,c => c.WebSiteContainer.Title, c => c.WebId == this.WebSite.Id), "Key", "Value");
            ViewBag.Html =new SelectList(ContentManagerComponent.Instance.WebDesignHtmlFacade.SelectKeyValuePair(c => c.HtmlDesgin.Id,c => c.HtmlDesgin.Title, c => c.WebId ==this.WebSite.Id), "Key", "Value");
            ViewBag.MenuHtmls = new SelectList(ContentManagerComponent.Instance.WebDesignMenuHtmlFacade.SelectKeyValuePair(x => x.MenuHtmlId, x => x.WebSiteMenuHtml.Title, x => x.WebSiteId == this.WebSite.Id), "Key", "Value");
            return PartialView("PVModify", Id.HasValue ? WebDesignComponent.Instance.ConfigurationFacade.Get(Id) : new Configuration());
        }

        [RadynAuthorize]
        [WebDesignHost]
        public ActionResult Create()
        {

            if (this.WebSite == null)
                return RedirectToAction("Index", "WebSite", new { area = "WebDesign" });
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var configuration = new Configuration();

            try
            {
                this.RadynTryUpdateModel(configuration);
                HttpPostedFileBase FavIcon = null;
                if (RadynSession["UpFileFavIcon"] != null)
                {
                    FavIcon = (HttpPostedFileBase)RadynSession["UpFileFavIcon"];
                    
                }
                if (this.WebSite == null)
                    return RedirectToAction("Index", "WebSite", new { area = "WebDesign" });
                configuration.Id = this.WebSite.ConfigurationId;
                configuration.PaymentType = AppExtention.FillPaymentTypes(collection);
                if (WebDesignComponent.Instance.ConfigurationFacade.Insert(configuration, FavIcon))
                {
                    RadynSession.Remove("UpFileFavIcon");
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    SessionParameters.CurrentWebSite = null;
                    return RedirectToAction("GetConfiguration");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return RedirectToAction("GetConfiguration");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);

                return View(configuration);
            }
        }

        [RadynAuthorize]
        [WebDesignHost]
        public ActionResult Edit(Guid Id)
        {


            if (this.WebSite == null)
                return RedirectToAction("Index", "WebSite", new { area = "WebDesign" });
            ViewBag.Id = this.WebSite.Id;
            return View();
        }


        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var configuration = WebDesignComponent.Instance.ConfigurationFacade.Get(Id);
            try
            {
                
                this.RadynTryUpdateModel(configuration);
                HttpPostedFileBase FavIcon = null;
                if (RadynSession["UpFileFavIcon"] != null)
                {
                    FavIcon = (HttpPostedFileBase)RadynSession["UpFileFavIcon"];
                    
                }
                configuration.PaymentType = AppExtention.FillPaymentTypes(collection);
                if (WebDesignComponent.Instance.ConfigurationFacade.Update(configuration, FavIcon))
                {
                    RadynSession.Remove("UpFileFavIcon");
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    SessionParameters.CurrentWebSite = null;
                    return RedirectToAction("GetConfiguration");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("GetConfiguration");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                ViewBag.Id = Id;
                return View(configuration);
            }
        }





    }
}