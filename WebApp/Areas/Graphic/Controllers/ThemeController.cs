using System;
using System.Web.Mvc;
using Radyn.Graphic;
using Radyn.Graphic.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;


namespace Radyn.WebApp.Areas.Graphic.Controllers
{
    public class ThemeController : WebDesignBaseController
    {
       
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = GraphicComponent.Instance.ThemeFacade.GetAll();
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(GraphicComponent.Instance.ThemeFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View(new Theme { Enabled = true });
        }
        [RadynAuthorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var homa = new Theme();
            try
            {

                this.RadynTryUpdateModel(homa);
                homa.CurrentUICultureName = collection["LanguageId"];
                if (GraphicComponent.Instance.ThemeFacade.Insert(homa))
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
                return View(homa);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            return View(GraphicComponent.Instance.ThemeFacade.Get(Id));
        }
        [RadynAuthorize]
        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var webSite = GraphicComponent.Instance.ThemeFacade.Get(Id);
            try
            {
                
                this.RadynTryUpdateModel(webSite);
               
                webSite.CurrentUICultureName = collection["LanguageId"];
                if (GraphicComponent.Instance.ThemeFacade.Update(webSite))
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
                return View(webSite);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            return View(GraphicComponent.Instance.ThemeFacade.Get(Id));
        }
        [RadynAuthorize]
        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var webSite = GraphicComponent.Instance.ThemeFacade.Get(Id);
            try
            {
                if (GraphicComponent.Instance.ThemeFacade.Delete(Id))
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
                return View(webSite);
            }
        }

    }
}