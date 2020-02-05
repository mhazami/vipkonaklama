using System;
using System.Web.Mvc;
using Radyn.PhoneBook;
using Radyn.PhoneBook.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.Areas.PhoneBook.Security.Filter;

namespace Radyn.WebApp.Areas.PhoneBook.Controllers
{
    public class OfficeController : PhoneBookBaseController<Office>
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = PhoneBookComponenets.Instance.OfficeFacade.GetAll();
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Int16 Id)
        {
            return View(PhoneBookComponenets.Instance.OfficeFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            ViewBag.OfficeList = new SelectList(PhoneBookComponenets.Instance.OfficeFacade.OrderBy(x => x.Title), "Id", "Title");
            return View(new Office());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var Office = new Office();
            try
            {
                this.RadynTryUpdateModel(Office);
                if (PhoneBookComponenets.Instance.OfficeFacade.Insert(Office))
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
                ViewBag.OfficeList = new SelectList(PhoneBookComponenets.Instance.OfficeFacade.OrderBy(x => x.Title), "Id", "Title");
                return View(Office);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Int16 Id)
        {
            ViewBag.OfficeList = new SelectList(PhoneBookComponenets.Instance.OfficeFacade.OrderBy(x => x.Title), "Id", "Title");
            return View(PhoneBookComponenets.Instance.OfficeFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Edit(Int16 Id, FormCollection collection)
        {
            var Office = PhoneBookComponenets.Instance.OfficeFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(Office);
                if (PhoneBookComponenets.Instance.OfficeFacade.Update(Office))
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
                ViewBag.OfficeList = new SelectList(PhoneBookComponenets.Instance.OfficeFacade.OrderBy(x => x.Title), "Id", "Title");
                return View(Office);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Int16 Id)
        {
            return View(PhoneBookComponenets.Instance.OfficeFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(Int16 Id, FormCollection collection)
        {
            var Office = PhoneBookComponenets.Instance.OfficeFacade.Get(Id);
            try
            {
                if (PhoneBookComponenets.Instance.OfficeFacade.Delete(Id))
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
                return View(Office);
            }
        }
    }
}