using Radyn.ContentManager;
using Radyn.ContentManager.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;
using System;
using System.Web.Mvc;
using Radyn.WebApp.Areas.ContentManager.Tools;

namespace Radyn.WebApp.Areas.ContentManager.Controllers
{
    public class PagesController : WebDesignBaseController<Pages>
    {
        // GET: ContentManager/Pages
        public ActionResult Index()
        {
            var list = ContentManagerComponent.Instance.PagesFacade.OrderBy(x=>x.Title);
            return View(list);
        }

        public ActionResult Details(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        public ActionResult Delete(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var page = ContentManagerComponent.Instance.PagesFacade.Get(id);

            try
            {
                if (ContentManagerComponent.Instance.PagesFacade.Delete(page))
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
                return View(page);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var page = new Pages();

            try
            {
                this.RadynTryUpdateModel(page, collection);
                this.RadynTryUpdateModel(page.HtmlDesgin, collection);
                page.CurrentUICultureName = collection["LanguageId"];
                page.HtmlDesgin.CurrentUICultureName = collection["LanguageId"];
                if (ContentManagerComponent.Instance.PagesFacade.Insert(page, page.HtmlDesgin))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                        messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Edit", new { Id = page.Id });
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                    messageIcon: MessageIcon.Error);
                return View(page);
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(page);
            }
        }

        public ActionResult Edit(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int Id, FormCollection collection)
        {
            var page = ContentManagerComponent.Instance.PagesFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(page, collection);
                this.RadynTryUpdateModel(page.HtmlDesgin, collection);

                page.CurrentUICultureName = collection["LanguageId"];
                page.HtmlDesgin.CurrentUICultureName = collection["LanguageId"];
                if (ContentManagerComponent.Instance.PagesFacade.Update(page))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                        messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                    messageIcon: MessageIcon.Error);
                return View(page);
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(page);
            }
        }

        public ActionResult GetModify(int? Id, PageMode status, string culture)
        {
            if (string.IsNullOrEmpty(culture)) culture = SessionParameters.Culture;
            Pages pages = null;
            switch (status)
            {

                case PageMode.Edit:
                    pages = ContentManagerComponent.Instance.PagesFacade.GetLanuageContent(culture, Id);
                    break;
                case PageMode.Create:
                    pages = new Pages() { HtmlId = Guid.NewGuid(), HtmlDesgin = new HtmlDesgin() };
                    break;

            }

            ViewBag.DefaultHtmls =new SelectList(ContentManagerComponent.Instance.DefaultHtmlDesginFacade.SelectKeyValuePair(x => x.Body, x => x.Title,
                x => x.Enabled),"Key","Value");
              
            this.PrepareViewBags(pages, status);
            return PartialView("PVModify", pages);
        }
        public ActionResult ShowPage(int id, string title)
        {
            ViewBag.Title = title;
            var htmlDesign = ContentManagerComponent.Instance.PagesFacade.SelectFirstOrDefault(x => x.HtmlDesgin, x => x.Id == id);
            ViewBag.Html = this.GetHtml(htmlDesign, this.WebSite.Title, DefaultContrainerId: this.WebSite.Configuration.DefaultContainerID);
            return View();
        }
        public ActionResult GetDetails(int Id)
        {
            var page = ContentManagerComponent.Instance.PagesFacade.Get(Id);
            return PartialView("PVDetails", page);
        }



    }
}