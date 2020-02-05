using System;
using System.Web.Mvc;
using Radyn.PhoneBook;
using Radyn.PhoneBook.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.Areas.PhoneBook.Security.Filter;

namespace Radyn.WebApp.Areas.PhoneBook.Controllers
{
    public class PhoneTypeController : PhoneBookBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = PhoneBookComponenets.Instance.PhoneTypeFacade.GetAll();
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Int16 Id)
        {
            return View(PhoneBookComponenets.Instance.PhoneTypeFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View(new PhoneType());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var phoneType = new PhoneType();
            try
            {
                this.RadynTryUpdateModel(phoneType);
                if (PhoneBookComponenets.Instance.PhoneTypeFacade.Insert(phoneType))
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
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return View(phoneType);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Int16 Id)
        {
            return View(PhoneBookComponenets.Instance.PhoneTypeFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Edit(Int16 Id, FormCollection collection)
        {
            var phoneType = PhoneBookComponenets.Instance.PhoneTypeFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(phoneType);
                if (PhoneBookComponenets.Instance.PhoneTypeFacade.Update(phoneType))
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
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return View(phoneType);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Int16 Id)
        {
            return View(PhoneBookComponenets.Instance.PhoneTypeFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(Int16 Id, FormCollection collection)
        {
            var phoneType = PhoneBookComponenets.Instance.PhoneTypeFacade.Get(Id);
            try
            {
                if (PhoneBookComponenets.Instance.PhoneTypeFacade.Delete(Id))
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
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return View(phoneType);
            }
        }
    }
}