using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.ContentManager;
using Radyn.ContentManager.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.ContentManager.Controllers
{
    public class WebDesignMenuController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list =
                ContentManagerComponent.Instance.WebDesignMenuFacade.Select(x => x.WebSiteMenu,
                    menu => menu.WebId == this.WebSite.Id);
            return View(list);
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {

            var predicateBuilder = new PredicateBuilder<Radyn.ContentManager.DataStructure.WebDesignMenu>();
            predicateBuilder.And(x => x.WebId == this.WebSite.Id);


            if (!string.IsNullOrEmpty(collection["title"]))
            {
                predicateBuilder.And(x => x.WebSiteMenu.Text.Contains(collection["title"]));
            }
            var list = ContentManagerComponent.Instance.WebDesignMenuFacade.Select(x => x.WebSiteMenu, predicateBuilder.GetExpression());
            return View(list);
        }


        [RadynAuthorize]
        public ActionResult Details(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }
        [RadynAuthorize]
        public ActionResult Create()
        {

            return View();
        }



        public ActionResult SearchStoreMenu(string title)
        {


            var list =
                ContentManagerComponent.Instance.WebDesignMenuFacade.Select(x => x.WebSiteMenu,
                    menu => menu.WebId == this.WebSite.Id);
            if (!string.IsNullOrEmpty(title))
            {
                var lst = list.Where(item => item.Text.Contains(title)).ToList();
                return PartialView("PVSearchIndex", lst);
            }

            return PartialView("PVSearchIndex", list);
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var menu = new Menu();

            try
            {
                this.RadynTryUpdateModel(menu);
                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }
                menu.CurrentUICultureName = collection["LanguageId"]; ;
                if (ContentManagerComponent.Instance.WebDesignMenuFacade.Insert(this.WebSite.Id, menu, file))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return this.SubmitRedirect(collection, new { Id = menu.Id });


                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Redirect("~/ContentManager/WebDesignMenu/Index");

            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.StoreId = this.WebSite.Id;
                return View(menu);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid id, string culture)
        {
            ViewBag.Id = id;
            ViewBag.Culture = culture;
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            var menu = ContentManagerComponent.Instance.MenuFacade.Get(id);

            try
            {
                this.RadynTryUpdateModel(menu);

                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }


                menu.CurrentUICultureName = collection["LanguageId"]; ;
                if (ContentManagerComponent.Instance.MenuFacade.Update(menu, file))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return this.SubmitRedirect(collection, new { Id = menu.Id });

                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Redirect("~/ContentManager/WebDesignMenu/Index");

            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = id;
                return View(menu);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            var menu = ContentManagerComponent.Instance.MenuFacade.Get(id);

            try
            {
                if (ContentManagerComponent.Instance.MenuFacade.Any(x => x.ParentId == menu.Id))
                {
                    ShowMessage(Resources.ContentManager.This_Item_Not_able_to_Delete_Becase_Have_ChildNode,
                                Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Error);
                    return Redirect("~/ContentManager/WebDesignMenu/Index");

                }
                if (ContentManagerComponent.Instance.WebDesignMenuFacade.Delete(this.WebSite.Id, id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return Redirect("~/ContentManager/WebDesignMenu/Index");

                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Redirect("~/ContentManager/WebDesignMenu/Index");

            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = id;
                return View(menu);
            }
        }
        [RadynAuthorize]
        public ActionResult ModifyMenu(FormCollection formCollection)
        {
            try
            {
                var id = formCollection["menuid"];
                var state = formCollection["menuState"];
                var parentorOwn = string.IsNullOrEmpty(id) ? null : ContentManagerComponent.Instance.MenuFacade.Get(Guid.Parse(id));
                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }
                switch (state)
                {

                    case "Create":
                        {

                            var menu = new Menu();
                            this.RadynTryUpdateModel(menu, formCollection);
                            menu.CurrentUICultureName = formCollection["LanguageId"];
                            if (ContentManagerComponent.Instance.WebDesignMenuFacade.Insert(this.WebSite.Id, menu, file))
                            {

                                return Content("true");
                            }
                            ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" },
                                        messageIcon: MessageIcon.Error);
                            return Content("false");
                        }
                    case "Edit":
                        {
                            if (parentorOwn == null)
                            {
                                ShowMessage(Resources.ContentManager.Not_Exist_To_Edit, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" },
                                            messageIcon: MessageIcon.Error);
                                return Content("false");
                            }
                            this.RadynTryUpdateModel(parentorOwn, formCollection);
                            parentorOwn.CurrentUICultureName = formCollection["LanguageId"];
                            if (ContentManagerComponent.Instance.MenuFacade.Update(parentorOwn, file))
                            {

                                return Content("true");
                            }
                            ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" }, messageIcon: MessageIcon.Error);
                            return Content("false");
                        }
                    case "Delete":
                        {
                            if (parentorOwn == null)
                            {
                                ShowMessage(Resources.ContentManager.Not_Exist_To_delete, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" },
                                            messageIcon: MessageIcon.Error);
                                return Content("false");
                            }
                            if (ContentManagerComponent.Instance.MenuFacade.Any(menu => menu.ParentId == parentorOwn.Id))
                            {
                                ShowMessage(Resources.ContentManager.This_Item_Not_able_to_Delete_Becase_Have_ChildNode,
                                            Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" },
                                            messageIcon: MessageIcon.Error);
                                return Content("false");
                            }
                            if (ContentManagerComponent.Instance.WebDesignMenuFacade.Delete(this.WebSite.Id, parentorOwn.Id))
                            {

                                return Content("true");
                            }
                            ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" },
                                        messageIcon: MessageIcon.Error);
                            return Content("false");
                        }
                }
                return Content("true");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception, new[] { Resources.Common.Ok, "Close_thisModal();" });
                return Content("false");
            }
        }
        [RadynAuthorize]
        public ActionResult PartialViewModifyMenu(string state, Guid id)
        {
            ViewBag.state = state;
            switch (state)
            {
                case "Create":
                    var menu1 = ContentManagerComponent.Instance.MenuFacade.Get(id);
                    ViewBag.ParentId = menu1 != null ? menu1.Id : (Guid?)null;
                    return PartialView("PVModifyMenu");
                case "Edit":
                    ViewBag.Id = id;
                    return PartialView("PVModifyMenu");
                case "Delete":
                    ViewBag.Id = id;
                    return PartialView("PVDetailsMenu");
            }
            return Content("");
        }

        [RadynAuthorize]
        public ActionResult LookUpMenu(string culture)
        {
            ViewBag.Culture = culture;
            return View();
        }

        public ActionResult GetMenuTree(string culture)
        {

            var model = ContentManagerComponent.Instance.WebDesignMenuFacade.MenuTree(this.WebSite.Id, culture);
            return PartialView("PartialViewMenuTree", model);
        }

    }
}
