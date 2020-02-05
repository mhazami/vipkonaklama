using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.FileManager;
using Radyn.FileManager.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebDesign;

namespace Radyn.WebApp.Areas.FileManager.Controllers
{

    public class RadynEditorController : WebDesignBaseController
    {

        #region editor

        public ActionResult InsertLink(string editorId, Guid folderId)
        {
            try
            {
                var guid = folderId;
                if (folderId == Guid.Empty)
                {
                    if (System.Web.HttpContext.Current == null ||
                           string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Url.Authority))
                        throw new Exception(Resources.Common.ErrorInInsert);
                    var id =  WebDesignComponent.Instance.WebSiteFacade.GetWebSiteFolderId(System.Web.HttpContext.Current.Request.Url.Authority);
                    if (id == Guid.Empty)
                    {
                        ViewBag.ErrorMessage = "لطفا وب سایت مورد نظر را ثبت نمایید";
                        return PartialView("PVInsertLink");
                    }
                    guid = id;
                }
                ViewBag.folderId = guid;
                ViewBag.editorId = editorId;
                return PartialView("PVInsertLink");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.An_error_has_occurred + exception.Message, Resources.Common.MessaageTitle,
                    messageIcon: MessageIcon.Error);
                return Content("false");
            }
        }


        public ActionResult LookUpEditorFolder(bool returnimage = false)
        {
            ViewBag.returnimage = returnimage;
            return PartialView("LookUpEditorFolder");
        }

        public ActionResult EditorFolder(bool returnimage = false)
        {
            try
            {
                var guid = Guid.Empty;
                if (System.Web.HttpContext.Current == null ||
                       string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Url.Authority))
                    throw new Exception(Resources.Common.ErrorInInsert);
                var id = WebDesignComponent.Instance.WebSiteFacade.GetWebSiteFolderId(System.Web.HttpContext.Current.Request.Url.Authority);
                if (id == Guid.Empty)
                {
                    ViewBag.ErrorMessage = "لطفا وب سایت مورد نظر را ثبت نمایید";
                    return PartialView("PVEditorFolder");
                }
                guid = id;
                ViewBag.folderId = guid;
                ViewBag.returnimage = returnimage;
                return PartialView("PVEditorFolder");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");
            }
        }

        public ActionResult EditorFolderTree(Guid folderId)
        {

            var list = FileManagerComponent.Instance.FolderFacade.GetWithChilds(folderId);
            return PartialView("PVFolders", list);


        }


        #endregion


        public ActionResult GetFiles(Guid Id)
        {

            var list = FileManagerComponent.Instance.FileFacade.Where(file => file.FolderId == Id);
            ViewBag.FolderId = Id;
            return PartialView("PVFiles", list);
        }


        [HttpPost]

        public ActionResult ModifyFolder(FormCollection formCollection)
        {

            try
            {
                var id = formCollection["FolderId"];
                var state = formCollection["FolderState"];
                var parentorOwn = string.IsNullOrEmpty(id) ? null : FileManagerComponent.Instance.FolderFacade.Get(id);
                switch (state)
                {

                    case "Create":
                        {

                            var folder = new Folder();
                            this.RadynTryUpdateModel(folder, formCollection);
                            if (parentorOwn != null) folder.ParentFolderId = parentorOwn.Id;
                            folder.IsExternal = false;
                            if (FileManagerComponent.Instance.FolderFacade.Insert(folder))
                            {
                                ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,

                                    messageIcon: MessageIcon.Succeed);
                                return Content("true");
                            }
                            ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,

                                messageIcon: MessageIcon.Error);
                            return Content("false");
                        }
                    case "Edit":
                        {

                            if (parentorOwn == null)
                            {
                                ShowMessage(Resources.ContentManager.Not_Exist_To_Edit, Resources.Common.MessaageTitle,

                                    messageIcon: MessageIcon.Error);
                                return Content("false");
                            }
                            this.RadynTryUpdateModel(parentorOwn, formCollection);

                            if (FileManagerComponent.Instance.FolderFacade.Update(parentorOwn))
                            {
                                ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,

                                    messageIcon: MessageIcon.Succeed);
                                return Content("true");
                            }
                            ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle,
                               messageIcon: MessageIcon.Error);
                            return Content("false");
                        }
                    case "Delete":
                        {
                            if (parentorOwn == null)
                            {
                                ShowMessage(Resources.ContentManager.Not_Exist_To_delete, Resources.Common.MessaageTitle,

                                    messageIcon: MessageIcon.Error);
                                return Content("false");
                            }
                            var child =
                                FileManagerComponent.Instance.FolderFacade.Any(
                                    folder => folder.ParentFolderId == parentorOwn.Id);
                            if (child)
                            {
                                ShowMessage(Resources.ContentManager.This_Item_Not_able_to_Delete_Becase_Have_ChildNode,
                                    Resources.Common.MessaageTitle,
                                    messageIcon: MessageIcon.Error);
                                return Content("false");
                            }
                            if (FileManagerComponent.Instance.FolderFacade.Delete(parentorOwn.Id))
                            {
                                ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,

                                    messageIcon: MessageIcon.Succeed);
                                return Content("true");
                            }
                            ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,

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

        public ActionResult GetFolder(string state, Guid id)
        {
            var menu = new Folder();
            switch (state)
            {

                case "Create":
                    var menu1 = FileManagerComponent.Instance.FolderFacade.Get(id);
                    menu = new Folder();
                    if (menu1 != null) menu1.ParentFolderId = menu1.Id;
                    break;
                case "Edit":
                    menu = FileManagerComponent.Instance.FolderFacade.Get(id);
                    break;
                case "Delete":
                    menu = FileManagerComponent.Instance.FolderFacade.Get(id);
                    break;
            }

            ViewBag.id = id;
            ViewBag.state = state;
            if (state == "Create" || state == "Edit")
                return PartialView("LookUPModify", menu);
            return PartialView("LookUPDetails", menu);
        }

        public ActionResult LookUPFileModify(Guid Id, Guid? fileId = null)
        {
            var folder = FileManagerComponent.Instance.FolderFacade.Get(Id);
            ViewBag.FolderName = folder != null ? folder.Title : "";
            ViewBag.FolderId = Id;
            return PartialView("LookUPFileModify", fileId.HasValue ? FileManagerComponent.Instance.FileFacade.Get(fileId) : new File());
        }
        [HttpPost]
        public ActionResult FileModify(FormCollection collection)
        {
            try
            {
                List<HttpPostedFileBase> File = null;
                if (RadynSession["File"] != null)
                {
                    File = (List<HttpPostedFileBase>)RadynSession["File"];
                    RadynSession.Remove("File");
                }
                var Id = string.IsNullOrEmpty(collection["FolderId"]) ? (Guid?)null : collection["FolderId"].ToGuid();
                if (collection["FileId"].ToGuid() == Guid.Empty)
                {
                    if (File == null)
                    {
                        ShowMessage(Resources.FileManager.PleaseUploadFile, Resources.Common.MessaageTitle,
                       messageIcon: MessageIcon.Error);
                        return Content("false");
                    }

                    if (FileManagerComponent.Instance.FileFacade.Insert(File, new File() { FolderId = Id, FileName = collection["FileName"] }))
                    {
                        ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,

                            messageIcon: MessageIcon.Succeed);
                        return Content("true");
                    }
                    ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                        messageIcon: MessageIcon.Error);
                    return Content("false");
                }
                if (File != null)
                {
                    if (FileManagerComponent.Instance.FileFacade.Update(File.FirstOrDefault(), collection["FileId"].ToGuid(), new File() { FileName = collection["FileName"], FolderId = Id }))
                    {
                        ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,

                            messageIcon: MessageIcon.Succeed);
                        return Content("true");
                    }
                    ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                   messageIcon: MessageIcon.Error);
                    return Content("false");
                }
                if (FileManagerComponent.Instance.FileFacade.Update(collection["FileId"].ToGuid(), new File() { FileName = collection["FileName"], FolderId = Id }))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,

                        messageIcon: MessageIcon.Succeed);
                    return Content("true");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                    messageIcon: MessageIcon.Error);
                return Content("false");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
                return Content("false");
            }
        }

        public ActionResult DeleteFile(Guid Id)
        {

            try
            {
                if (FileManagerComponent.Instance.FileFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,
                        messageIcon: MessageIcon.Succeed);
                    return Content("true");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,
                    messageIcon: MessageIcon.Error);
                return Content("false");

            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
                return Content("false");
            }
        }






    }
}
