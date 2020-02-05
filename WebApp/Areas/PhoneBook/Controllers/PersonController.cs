using Radyn.Common;
using Radyn.EnterpriseNode.DataStructure;
using Radyn.EnterpriseNode.Tools;
using Radyn.PhoneBook;
using Radyn.PhoneBook.DataStructure;
using Radyn.Web.Html;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.Areas.PhoneBook.Security.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.PhoneBook.Controllers
{
    public class PersonController : PhoneBookBaseController<Person>
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            RadynSession.Remove("PersonAddressDataSource");
            RadynSession.Remove("PersonPhoneDataSource");
            var list = PhoneBookComponenets.Instance.PersonFacade.GetAll();
            return View(list);
        }
        public ActionResult GetIndex(string txtsearch)
        {

            var result = PhoneBookComponenets.Instance.PersonFacade.SearchPerson(txtsearch);
            return PartialView("PVIndex", result);
        }

        public ActionResult GetModify(Guid Id, PageMode status)
        {

            Person person = null;
            switch (status)
            {

                case PageMode.Edit:
                    person = PhoneBookComponenets.Instance.PersonFacade.Get(Id);
                    break;
                case PageMode.Create:
                    var id = Guid.NewGuid();
                    person = new Person()
                    {
                        Id = id,
                        Enabled = true,
                        EnterpriseNode = new Radyn.EnterpriseNode.DataStructure.EnterpriseNode()
                        {
                            RealEnterpriseNode = new RealEnterpriseNode(),
                            LegalEnterpriseNode = new LegalEnterpriseNode()
                        }
                    };
                    break;

            }

            this.PrepareViewBags(person, status);
            return PartialView("PVModify", person);
        }

        public override void PrepareViewBags(Person Model, PageMode pageMode)
        {
            base.PrepareViewBags(Model, pageMode);
            ViewBag.ProvinceList = new SelectList(CommonComponent.Instance.ProvinceFacade.OrderBy(x => x.Title), "Id", "Title");
            ViewBag.OfficeList = new SelectList(PhoneBookComponenets.Instance.OfficeFacade.OrderBy(x => x.Title), "Id", "Title");
            ViewBag.EnterpriseNodeType = new SelectList(Radyn.Utility.EnumUtils.ConvertEnumToIEnumerableInLocalization<Enums.EnterpriseNodeType>(), "Key", "Value");
        }

        public ActionResult GetDetails(Guid Id, PageMode status)
        {

            var person = PhoneBookComponenets.Instance.PersonFacade.Get(Id);
            this.PrepareViewBags(person, status);
            return PartialView("PVDetails", person);
        }



        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [RadynAuthorize]
        public ActionResult Create()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var person = new Person() { EnterpriseNode = new Radyn.EnterpriseNode.DataStructure.EnterpriseNode() };
            try
            {
                this.RadynTryUpdateModel(person, collection);
                this.RadynTryUpdateModel(person.EnterpriseNode, collection);
                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }
                switch (person.EnterpriseNode.EnterpriseNodeTypeId)
                {
                    case Enums.EnterpriseNodeType.RealEnterPriseNode:
                        person.EnterpriseNode.RealEnterpriseNode = new RealEnterpriseNode();
                        this.RadynTryUpdateModel(person.EnterpriseNode.RealEnterpriseNode, collection);

                        break;
                    case Enums.EnterpriseNodeType.LegalEnterPriseNode:
                        person.EnterpriseNode.LegalEnterpriseNode = new LegalEnterpriseNode();
                        this.RadynTryUpdateModel(person.EnterpriseNode.LegalEnterpriseNode, collection);
                        break;
                }
                var addresslist = (List<PersonAddress>)RadynSession["PersonAddressDataSource"];
                var personphone = (List<PersonPhone>)RadynSession["PersonPhoneDataSource"];

                if (PhoneBookComponenets.Instance.PersonFacade.Insert(person, addresslist, personphone, file))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, " window.location='/PhoneBook/Person/Index'; " }, messageIcon: MessageIcon.Succeed);
                    RadynSession.Remove("PersonAddressDataSource");
                    RadynSession.Remove("PersonPhoneDataSource");
                    return Content("true");
                }

                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Content("false");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Content("false");
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            var id = GetModelKey(collection);
            var person = PhoneBookComponenets.Instance.PersonFacade.Get(id);
            try
            {
                this.RadynTryUpdateModel(person, collection);
                this.RadynTryUpdateModel(person.EnterpriseNode, collection);
                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }
                switch (person.EnterpriseNode.EnterpriseNodeTypeId)
                {
                    case Enums.EnterpriseNodeType.RealEnterPriseNode:
                        this.RadynTryUpdateModel(person.EnterpriseNode.RealEnterpriseNode, collection);
                        break;
                    case Enums.EnterpriseNodeType.LegalEnterPriseNode:
                        this.RadynTryUpdateModel(person.EnterpriseNode.LegalEnterpriseNode, collection);
                        break;
                }
                var addresslist = (List<PersonAddress>)RadynSession["PersonAddressDataSource"];
                var personphone = (List<PersonPhone>)RadynSession["PersonPhoneDataSource"];
                if (PhoneBookComponenets.Instance.PersonFacade.Update(person, addresslist, personphone,
                                                                    file))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, " window.location='/PhoneBook/Person/Index'; " }, messageIcon: MessageIcon.Succeed);
                    RadynSession.Remove("PersonAddressDataSource");
                    RadynSession.Remove("PersonPhoneDataSource");
                    return Content("true");
                }


                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("fasle");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("fasle");
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var person = PhoneBookComponenets.Instance.PersonFacade.Get(Id);
            try
            {
                if (PhoneBookComponenets.Instance.PersonFacade.Delete(Id))
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
                return View(person);
            }
        }

        [HttpPost]
        public ActionResult Validate(FormCollection collection)
        {
            var messageStack = new List<string>();
            var id = GetModelKey(collection);
            var pageMode = GetPageMode(collection);
            Person person = null;
            switch (pageMode)
            {

                case PageMode.Edit:
                    person = PhoneBookComponenets.Instance.PersonFacade.Get(id);
                    this.RadynTryUpdateModel(person, collection);
                    this.RadynTryUpdateModel(person.EnterpriseNode, collection);
                    break;
                case PageMode.Create:
                    person = new Person() { EnterpriseNode = new Radyn.EnterpriseNode.DataStructure.EnterpriseNode() };
                    this.RadynTryUpdateModel(person, collection);
                    this.RadynTryUpdateModel(person.EnterpriseNode, collection);
                    break;

            }

            if (person == null) return null;
            var enterpriseNodeEnterpriseNodeTypeId = (Enums.EnterpriseNodeType)person.EnterpriseNode.EnterpriseNodeTypeId;
            switch (enterpriseNodeEnterpriseNodeTypeId)
            {
                case Enums.EnterpriseNodeType.RealEnterPriseNode:
                    person.EnterpriseNode.RealEnterpriseNode = new RealEnterpriseNode();
                    this.RadynTryUpdateModel(person.EnterpriseNode.RealEnterpriseNode, collection);
                    if (string.IsNullOrEmpty(person.EnterpriseNode.RealEnterpriseNode.FirstName))
                        messageStack.Add(Resources.PhoneBook.Please_Enter_FName);
                    if (string.IsNullOrEmpty(person.EnterpriseNode.RealEnterpriseNode.LastName))
                        messageStack.Add(Resources.PhoneBook.Please_Enter_Lname);
                    if (string.IsNullOrEmpty(person.PersoneliCode))
                        messageStack.Add(Resources.PhoneBook.Please_Enter_PersoneliCode);
                    break;
                case Enums.EnterpriseNodeType.LegalEnterPriseNode:
                    person.EnterpriseNode.LegalEnterpriseNode = new LegalEnterpriseNode();
                    this.RadynTryUpdateModel(person.EnterpriseNode.LegalEnterpriseNode, collection);
                    break;

            }
            
            if (string.IsNullOrEmpty(person.EnterpriseNode.Tel))
                messageStack.Add(Resources.PhoneBook.Please_Enter_Tel);
            if (string.IsNullOrEmpty(person.EnterpriseNode.Email))
                messageStack.Add(Resources.PhoneBook.Please_Enter_Email);
            if (person.OfficeId == null)
                messageStack.Add(Resources.PhoneBook.Please_Enter_Office);
            if (person.DepartmentId == null)
                messageStack.Add(Resources.PhoneBook.Please_Enter_Departmen);
            if (RadynSession["Image"] != null)
            {
                var file = (HttpPostedFileBase)RadynSession["Image"];
                if (file.ContentLength > (30 * 1024))
                    messageStack.Add(Resources.PhoneBook.Image_Size_Not_Alowed);
            }
            var addresslist = (List<PersonAddress>)RadynSession["PersonAddressDataSource"];
            if (string.IsNullOrEmpty(person.JopTitle))
                messageStack.Add(Resources.PhoneBook.PleaseEnterJobTitle);
            if (!addresslist.Any())
                messageStack.Add(Resources.PhoneBook.PleaseEnterMinimomOfAddress);
            if (!addresslist.Any(x => x.IsDefault))
                messageStack.Add("لطفا آدرس پیش فرض را انتخاب نمایید");
            var messageBody = messageStack.Aggregate("", (current, item) => current + Tag.Li(item));
            if (messageBody != "")
            {
                ShowMessage(messageBody, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                return Content("false");
            }
            return Content("true");

        }


        


    }
}