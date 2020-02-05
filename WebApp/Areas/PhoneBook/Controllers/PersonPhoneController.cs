using System;
using System.Linq;
using System.Web.Mvc;
using Radyn.PhoneBook;
using Radyn.PhoneBook.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.Areas.PhoneBook.Security.Filter;

namespace Radyn.WebApp.Areas.PhoneBook.Controllers
{
    public class PersonPhoneController : PhoneBookBaseController<PersonPhone>
    {
        public ActionResult GetIndex(Guid personId, PageMode status)
        {
            this.SetPageMode(status, "GetIndex");
            var list =  PhoneBookComponenets.Instance.PersonPhoneFacade.OrderBy(x => x.TelNumber, x => x.PersonId == personId);
            if (status == PageMode.Details)
                return PartialView("PVPhoneList", list);
            this.DataSource = list;
            ViewBag.personId = personId;
            return PartialView("PVIndex");

        }
        public ActionResult GetPhoneList()
        {
            this.SetPageMode(PageMode.Edit, "GetIndex");
            var list = this.DataSource;
            if (list == null) return Content("false");
            return PartialView("PVPhoneList", list);

        }
        public ActionResult AddPhone(Guid? phoneId, Guid personId, PageMode status)
        {

            PersonPhone PersonPhone = null;
            switch (status)
            {

                case PageMode.Details:
                case PageMode.Delete:
                case PageMode.Edit:
                    var firstOrDefault = this.DataSource.FirstOrDefault(address => address.Id.Equals(phoneId));
                    if (firstOrDefault != null) PersonPhone = firstOrDefault;
                    break;
                case PageMode.Create:
                    PersonPhone = new PersonPhone() { Id = Guid.NewGuid(), PersonId = personId };
                    break;

            }
            ViewBag.PhoneType = new SelectList(PhoneBookComponenets.Instance.PhoneTypeFacade.SelectKeyValuePair(x => x.Id, x => x.Title), "Key", "Value");
            this.PrepareViewBags(PersonPhone, status);
            return PartialView("PVPhone", PersonPhone);


        }

        [HttpPost]
        public ActionResult AddPhone(FormCollection collection)
        {

            if (string.IsNullOrEmpty(collection["TelNumber"]))
            {
                ShowMessage("لطفا شماره تلفن را وارد کنید", Resources.Common.MessaageTitle,
                    messageIcon: MessageIcon.Error);
                return Content("false");
            }
            var pageMode = this.GetPageMode<PersonPhone>(collection);
            var modelKey = this.GetModelKey<PersonPhone>(collection);
            var personAddresses = this.DataSource;
            switch (pageMode)
            {

                case PageMode.Edit:
                    var firstOrDefault = personAddresses.FirstOrDefault(address => address.Id.Equals(modelKey[0].ToString().ToGuid()));
                    if (firstOrDefault != null)
                    {
                        personAddresses.Remove(firstOrDefault);
                        firstOrDefault = new PersonPhone();
                        this.RadynTryUpdateModel(firstOrDefault);
                        if (firstOrDefault.PhoneTypeId != 0 )
                            firstOrDefault.PhoneType = PhoneBookComponenets.Instance.PhoneTypeFacade.Get(firstOrDefault.PhoneTypeId);
                        personAddresses.Add(firstOrDefault);
                        return Content("true");
                    }
                    break;
                case PageMode.Create:
                    var personPhone = new PersonPhone();
                    this.RadynTryUpdateModel(personPhone);
                    if (personPhone.PhoneTypeId != 0)
                        personPhone.PhoneType = PhoneBookComponenets.Instance.PhoneTypeFacade.Get(personPhone.PhoneTypeId);
                    personAddresses.Add(personPhone);
                    return Content("true");
                    break;

            }
            return Content("false");



        }
        public ActionResult RemovePhone(Guid phoneId)
        {
            var firstOrDefault = this.DataSource.FirstOrDefault(personPhone => personPhone.Id.Equals(phoneId));
            return firstOrDefault != null ? Content(this.DataSource.Remove(firstOrDefault) ? "true" : "false") : Content("false");
        }

      
    }
}