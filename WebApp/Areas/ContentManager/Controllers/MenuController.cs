using System;
using System.Web;
using System.Web.Mvc;
using Radyn.ContentManager;
using Radyn.ContentManager.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;


namespace Radyn.WebApp.Areas.ContentManager.Controllers
{

    public class MenuController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ContentManagerComponent.Instance.MenuFacade.Where(menu => menu.IsExternal == false);
            return View(list);
        }
        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(ContentManagerComponent.Instance.MenuFacade.Get(Id));
        }
        [RadynAuthorize]
        public ActionResult Create()
        {

            return View(new Menu());
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
               menu.CurrentUICultureName = collection["LanguageId"];
                if (ContentManagerComponent.Instance.MenuFacade.Insert(menu, file))
                {


                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Succeed);
                    return Redirect("~/ContentManager/Menu/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Redirect("~/ContentManager/Menu/Index");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
                return View(menu);
            }
        }



        [RadynAuthorize]
        public ActionResult Content()
        {
            return View();
        }
        [RadynAuthorize]
        public ActionResult LookUpMenu()
        {

            return View();
        }


      
      
        public ActionResult ModifyMenu(FormCollection formCollection)
        {

            try
            {
                var id = formCollection["menuid"];
                var state = formCollection["menuState"];
                var parentorOwn =string.IsNullOrEmpty(id)?null: ContentManagerComponent.Instance.MenuFacade.Get(Guid.Parse(id));
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
                            if (ContentManagerComponent.Instance.MenuFacade.Insert(menu, file))
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
                            if (ContentManagerComponent.Instance.MenuFacade.Delete(parentorOwn.Id))
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
             ShowExceptionMessage(exception);
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
        public ActionResult GetDetails(Guid Id)
        {
            var details = ContentManagerComponent.Instance.MenuFacade.Get(Id);
            return PartialView("PVDetails", details);
        }




        public ActionResult GetModify(Guid? Id, Guid? parentId, string menuurl, string culture = "")
        {
            ViewBag.Id = Id;
            ViewBag.MenuUrl = menuurl;

            ViewBag.Culture = culture;
            var modify = Id.HasValue
                ? ContentManagerComponent.Instance.MenuFacade.Get(Id)
                : new Menu() { Enabled = true, ParentId = parentId };

           
            return PartialView("PVModify", modify);
        }

       
      
        public ActionResult GetMenuTree(Guid? id)
        {
            var model = ContentManagerComponent.Instance.MenuFacade.MenuTree(id);
            return PartialView("PartialViewMenuTree", model);
        }
        [RadynAuthorize]
        public ActionResult Edit(Guid id)
        {
            ViewBag.Id = id;
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
                menu.CurrentUICultureName = collection["LanguageId"];
                if (ContentManagerComponent.Instance.MenuFacade.Update(menu, file))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return Redirect("~/ContentManager/Menu/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Redirect("~/ContentManager/Menu/Index");
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
                    return Redirect("~/ContentManager/Menu/Index");
                }
                if (ContentManagerComponent.Instance.MenuFacade.Delete(id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return Redirect("~/ContentManager/Menu/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Redirect("~/ContentManager/Menu/Index");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
                ViewBag.Id = id;
                return View(menu);
            }
        }
       
    }
}