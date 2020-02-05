using System;
using System.Web;
using System.Web.Mvc;
using Radyn.FAQ;
using Radyn.FAQ.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.FAQ.Controllers
{
    public class FAQController : WebDesignBaseController
    {

        public ActionResult GetDetail(Guid Id)
        {
            return PartialView("PVDetails", FAQComponent.Instance.FAQFacade.Get(Id));
        }
        public ActionResult GetModify(Guid? Id)
        {
            return PartialView("PVModify", Id.HasValue ? FAQComponent.Instance.FAQFacade.Get(Id) : new Radyn.FAQ.DataStructure.FAQ());
        }
       
        
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = FAQComponent.Instance.FAQFacade.Where(x=>x.IsExternal==false);
            return View(list);
        }
        [RadynAuthorize]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            var value = collection["txtvalue"];
            var list = FAQComponent.Instance.FAQFacade.Search(value);
            ViewBag.value = value;
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

        [RadynAuthorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            var faq = new Radyn.FAQ.DataStructure.FAQ();
           
            try
            {
                var faqContent = new FAQContent();
                this.RadynTryUpdateModel(faq, collection);
                this.RadynTryUpdateModel(faqContent, collection);
                HttpPostedFileBase image = null;
                if (RadynSession["Image"] != null)
                {
                    image = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }

                if (Radyn.FAQ.FAQComponent.Instance.FAQFacade.Insert(faq,faqContent, image))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");

                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        [RadynAuthorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            var faq = FAQComponent.Instance.FAQFacade.Get(id);
            
            try
            {
                var content = FAQComponent.Instance.FaqContentFacade.Get(id, collection["LanguageId"]) ??
                 new FAQContent();
                this.RadynTryUpdateModel(content, collection);
                this.RadynTryUpdateModel(faq, collection);
                HttpPostedFileBase image = null;
                if (RadynSession["Image"] != null)
                {
                    image = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }

                if (Radyn.FAQ.FAQComponent.Instance.FAQFacade.Update(faq, content, image))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");

                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = id;
                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
           
            try
            {
               
                if (FAQComponent.Instance.FAQFacade.Delete(id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                ViewBag.Id = id;
                return View();
            }
        }

        

        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult FAQSearchableList(string Date)
        {
            return PartialView("FAQSearchableList", FAQComponent.Instance.FAQFacade.Where(x=>x.IsExternal==false));
        }
        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult FAQList()
        {
            return View();
        }
    }
}
