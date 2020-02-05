using System;
using System.Web.Mvc;
using Radyn.EnterpriseNode;
using Radyn.EnterpriseNode.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.EnterpriseNode.Controllers
{
    public class PrefixTitleController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = EnterpriseNodeComponent.Instance.PrefixTitleFacade.GetAll();
            if (list.Count == 0) return this.Redirect("~/EnterpriseNode/PrefixTitle/Create");
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(byte Id)
        {
            return View(EnterpriseNodeComponent.Instance.PrefixTitleFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View(new PrefixTitle());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var prefixTitle = new PrefixTitle();
            try
            {
                this.RadynTryUpdateModel(prefixTitle);
                prefixTitle.CurrentUICultureName = collection["LanguageId"];
                if (EnterpriseNodeComponent.Instance.PrefixTitleFacade.Insert(prefixTitle))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return (!string.IsNullOrEmpty(Request.QueryString["AddNew"])) ? this.Redirect("~/EnterpriseNode/PrefixTitle/Create") : this.Redirect("~/EnterpriseNode/PrefixTitle/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/EnterpriseNode/PrefixTitle/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(prefixTitle);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(byte Id)
        {
            return View(EnterpriseNodeComponent.Instance.PrefixTitleFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Edit(byte Id, FormCollection collection)
        {
            var prefixTitle = EnterpriseNodeComponent.Instance.PrefixTitleFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(prefixTitle);
                prefixTitle.CurrentUICultureName = collection["LanguageId"];
                if (EnterpriseNodeComponent.Instance.PrefixTitleFacade.Update(prefixTitle))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/EnterpriseNode/PrefixTitle/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/EnterpriseNode/PrefixTitle/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(prefixTitle)
                    ;
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(byte Id)
        {
            return View(EnterpriseNodeComponent.Instance.PrefixTitleFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(byte Id, FormCollection collection)
        {
            var prefixTitle = EnterpriseNodeComponent.Instance.PrefixTitleFacade.Get(Id);
            try
            {
                if (EnterpriseNodeComponent.Instance.PrefixTitleFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/EnterpriseNode/PrefixTitle/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/EnterpriseNode/PrefixTitle/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(prefixTitle);
            }
        }
    }
}
