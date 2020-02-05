using Radyn.ContentManager;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Tools;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Filter;
using Radyn.WebApp.AppCode.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.ContentManager.Controllers
{
    public class ContentController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ContentManagerComponent.Instance.ContentFacade.Where(x => x.IsExternal == false);
            return View(list);
        }
        public ActionResult GetDetails(int Id)
        {
            var obj = ContentManagerComponent.Instance.ContentFacade.Get(Id);
            return PartialView("PVDetails", obj);
        }
        public ActionResult GetModify(int? Id, string menuurl)
        {

            
            ViewBag.url = menuurl;
            var obj = Id.HasValue ? ContentManagerComponent.Instance.ContentFacade.Get(Id) : new Content() { Enabled = true, HasContainer = true };
            return PartialView("PVModify", obj);
        }
        [RadynAuthorize]
        public ActionResult Details(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [RadynAuthorize]
        [SourceCodeFile("ImageBrowser Controller", "~/Controllers/ImageBrowserController.cs")]
        public ActionResult Create()
        {
            FillTempData();
            return View();
        }

        private void FillTempData()
        {
            TempData["Containers"] = new SelectList(ContentManagerComponent.Instance.ContainerFacade
                .SelectKeyValuePair(x => x.Id, x => x.Title, x => x.IsExternal == false).Select(
                    keyValuePair =>
                        new KeyValuePair<Guid, string>(keyValuePair.Key.ToGuid(), keyValuePair.Value)), "Key", "Value");

        }

        [RadynAuthorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            var content = new Content();

            try
            {
                var contentcontent = new ContentContent();
                this.RadynTryUpdateModel(content, collection);
                this.RadynTryUpdateModel(contentcontent, collection);
                if (ContentManagerComponent.Instance.ContentFacade.Insert(content, contentcontent))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                TempData["Containers"] = new SelectList(ContentManagerComponent.Instance.ContainerFacade.SelectKeyValuePair(x => x.Id, x => x.Title, x => x.IsExternal == false), "Key", "Value");


                return View(content);
            }
        }
        [RadynAuthorize]
        [SourceCodeFile("ImageBrowser Controller", "~/Controllers/ImageBrowserController.cs")]
        public ActionResult Edit(int Id)
        {
            ViewBag.Id = Id;
            FillTempData();
            return View();
        }
        public ActionResult View(int id)
        {
            var content = ContentManagerComponent.Instance.ContentFacade.Get(id);
            if (content == null) return View();
            ViewBag.Title = content.GetContent(SessionParameters.Culture).Title;
            ViewBag.Html = ContentManagerComponent.Instance.ContentFacade.GetHtml(id, SessionParameters.Culture);
            ViewBag.Script = ContentManagerComponent.Instance.ContentFacade.GetScript(id);
            //ViewBag.UserScript = ContentManagerComponent.Instance.ContentFacade.GetScript(id);
            return View();
        }

        [RadynAuthorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int Id, FormCollection collection)
        {
            var content = ContentManagerComponent.Instance.ContentFacade.Get(Id);
            try
            {
                var contentcontent = ContentManagerComponent.Instance.ContentContentFacade.Get(Id, collection["LanguageId"]) ??
             new ContentContent();
                this.RadynTryUpdateModel(content, collection);
                this.RadynTryUpdateModel(contentcontent, collection);
                contentcontent.CurrentUICultureName = collection["LanguageId"];
                if (ContentManagerComponent.Instance.ContentFacade.Update(content, contentcontent))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                TempData["Containers"] = new SelectList(ContentManagerComponent.Instance.ContainerFacade.SelectKeyValuePair(x => x.Id, x => x.Title, x => x.IsExternal == false), "Key", "Value");
                ViewBag.Id = Id;
                return View(content);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var content = ContentManagerComponent.Instance.ContentFacade.Get(id);

            try
            {
                if (ContentManagerComponent.Instance.ContentFacade.Delete(id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = id;
                return View(content);
            }
        }

        public ActionResult Search(string qry)
        {
            var query = qry.Replace(" ", "+");
            ViewBag.qry = query;
            TempData["caption"] = "نتیجه جستجو : " + qry.Trancate(60);
            return this.View(ContentManagerComponent.Instance.ContentFacade.Search(query));
        }

        public ActionResult ViewPartial(int id)
        {
            ViewBag.Html = ContentManagerComponent.Instance.ContentFacade.GetHtml(id, SessionParameters.Culture);
            return PartialView("ViewPartial");
        }
    }
}