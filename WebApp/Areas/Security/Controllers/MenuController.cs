using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Radyn.Security;
using Radyn.Security.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class MenuController : WebDesignBaseController
    {
        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult Index(Guid? selectedId)
        {
            var list = new List<Menu>();
            if (selectedId.HasValue)
                list.Add(SecurityComponent.Instance.MenuFacade.Get(selectedId));
            return View(list);
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            var list = SecurityComponent.Instance.MenuFacade.Search(collection["txtSearch"]);
            ViewBag.txtSearch = collection["txtSearch"];
            return View(list);
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult Details(Guid ID)
        {
            return View(SecurityComponent.Instance.MenuFacade.Get(ID));
        }
        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult Create()
        {
            this.FillViewBags();
            return View(new Menu());
        }
        [RadynAuthorize(AccessLevel = UserType.Host)]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var menu = new Menu();
            try
            {

                this.RadynTryUpdateModel(menu, collection);
                HttpPostedFileBase fileBase = null;
                if (RadynSession["MenuIcon"] != null)
                {
                    fileBase = (HttpPostedFileBase)RadynSession["MenuIcon"];
                    RadynSession.Remove("MenuIcon");
                }
                if (SecurityComponent.Instance.MenuFacade.Insert(menu, fileBase))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { selectedId = menu.Id });
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { selectedId = menu.Id });
            }
            catch (Exception exception)
            {
                this.FillViewBags();
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(menu);
            }
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult Edit(Guid ID)
        {
            this.FillViewBags();
            return View(SecurityComponent.Instance.MenuFacade.Get(ID));
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        [HttpPost]
        public ActionResult Edit(Guid ID, FormCollection collection)
        {
            var menu = SecurityComponent.Instance.MenuFacade.Get(ID);
            try
            {
                this.RadynTryUpdateModel(menu, collection);

                HttpPostedFileBase fileBase = null;
                if (RadynSession["MenuIcon"] != null)
                {
                    fileBase = (HttpPostedFileBase)RadynSession["MenuIcon"];
                    RadynSession.Remove("MenuIcon");
                }
                if (SecurityComponent.Instance.MenuFacade.Update(menu, fileBase))
                {

                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { selectedId = ID });

                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { selectedId = ID });
            }
            catch (Exception exception)
            {
                this.FillViewBags();
                ShowExceptionMessage(exception);
                return View(menu);
            }
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult Delete(Guid ID)
        {
            return View(SecurityComponent.Instance.MenuFacade.Get(ID));
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        [HttpPost]
        public ActionResult Delete(Guid ID, FormCollection collection)
        {
            var menu = SecurityComponent.Instance.MenuFacade.Get(ID);
            try
            {
                if (SecurityComponent.Instance.MenuFacade.Delete(ID))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(menu);
            }
        }
        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult LookUPSearch()
        {

            return View();
        }
        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult Search(string value)
        {
            var list = SecurityComponent.Instance.MenuFacade.Search(value);
            return PartialView("PVSearchMenuResult", list);
        }

        private void FillViewBags()
        {
            ViewBag.MenuGroup = new SelectList(SecurityComponent.Instance.MenuGroupFacade.GetAll(), "Id", "Name");
        }

      
    }
}
