using Radyn.ContentManager;
using Radyn.ContentManager.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.ContentManager.Controllers
{
    public class PartialsController : WebDesignBaseController
    {
        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult Index(Guid? selectedId)
        {
            var list = new List<Partials>();
            if (selectedId.HasValue)
                list.Add(ContentManagerComponent.Instance.PartialsFacade.Get(selectedId));
            else
                list = ContentManagerComponent.Instance.PartialsFacade.GetAll();
            return View(list);
        }
        [RadynAuthorize(AccessLevel = UserType.Host)]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            var predicateBuilder = new PredicateBuilder<Partials>();
            if (!string.IsNullOrEmpty(collection["txtSearch"]))
            {
                var txt = collection["txtSearch"];
                predicateBuilder.And(x => x.Title.Contains(txt) || x.Url.Contains(txt));

            }
            var list = ContentManagerComponent.Instance.PartialsFacade.OrderBy(x => x.Title, predicateBuilder.GetExpression());
            ViewBag.txtSearch = collection["txtSearch"];
            return View(list);
        }

        public ActionResult GetDetail(Guid Id)
        {
            var culture = SessionParameters.Culture;
            return PartialView("PVDetails", ContentManagerComponent.Instance.PartialsFacade.GetLanuageContent(culture, Id));
        }
        public ActionResult GetModify(Guid? Id, string culture)
        {
            if (string.IsNullOrEmpty(culture)) culture = SessionParameters.Culture;
            ViewBag.Operations =
                new SelectList(SessionParameters.UserOperation, "Id", "Title");
            ViewBag.Containers = new SelectList(ContentManagerComponent.Instance.ContainerFacade.GetAll(), "Id", "Title");
            return PartialView("PVModify", Id.HasValue ? ContentManagerComponent.Instance.PartialsFacade.GetLanuageContent(culture, Id) : new Partials());
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult Details(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult Create()
        {

            return View(new Partials());
        }
        [RadynAuthorize(AccessLevel = UserType.Host)]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var partials = new Partials();
            try
            {
                this.RadynTryUpdateModel(partials);
                partials.CurrentUICultureName = collection["LanguageId"];
                HttpPostedFileBase file = null;
                if (RadynSession["ImagePartials"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["ImagePartials"];
                    RadynSession.Remove("ImagePartials");
                }
                if (ContentManagerComponent.Instance.PartialsFacade.Insert(partials, file))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return Redirect("~/ContentManager/Partials/Index?selectedId=" + partials.Id);
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Redirect("~/ContentManager/Partials/Index?selectedId=" + partials.Id);
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(partials);
            }
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult Edit(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var partials = ContentManagerComponent.Instance.PartialsFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(partials);
                partials.CurrentUICultureName = collection["LanguageId"];
                HttpPostedFileBase file = null;
                if (RadynSession["ImagePartials"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["ImagePartials"];
                    RadynSession.Remove("ImagePartials");
                }
                if (ContentManagerComponent.Instance.PartialsFacade.Update(partials, file))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return Redirect("~/ContentManager/Partials/Index?selectedId=" + partials.Id);

                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Redirect("~/ContentManager/Partials/Index?selectedId=" + partials.Id);
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(partials);
            }
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult Delete(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var partials = ContentManagerComponent.Instance.PartialsFacade.Get(Id);
            try
            {
                if (ContentManagerComponent.Instance.PartialsFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return Redirect("~/ContentManager/Partials/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Redirect("~/ContentManager/Partials/Index");

            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(partials);
            }
        }


    }
}