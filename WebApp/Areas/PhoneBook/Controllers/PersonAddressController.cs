using Radyn.PhoneBook;
using Radyn.PhoneBook.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using System;
using System.Linq;
using System.Web.Mvc;
using Radyn.WebApp.Areas.PhoneBook.Security.Filter;

namespace Radyn.WebApp.Areas.PhoneBook.Controllers
{
    public class PersonAddressController : PhoneBookBaseController<PersonAddress>
    {
        public ActionResult GetIndex(Guid personId,PageMode status)
        {
            this.SetPageMode(status, "GetIndex");
            var list = PhoneBookComponenets.Instance.PersonAddressFacade.OrderBy(x => x.Address, x => x.PersonId == personId);
            if (status == PageMode.Details)
                return PartialView("PVAddressList", list);
            this.DataSource = list;
            ViewBag.personId = personId;
            return PartialView("PVIndex");

        }
        public ActionResult GetAddressList()
        {
            this.SetPageMode(PageMode.Edit, "GetIndex");
            var list = this.DataSource;
            if (list == null) return Content("false");
            return PartialView("PVAddressList", list);

        }
      
        public ActionResult AddAddress(Guid? addressId, Guid personId, PageMode status)
        {
            PersonAddress personAddress = null;
            switch (status)
            {

                case PageMode.Details:
                case PageMode.Delete:
                case PageMode.Edit:
                    var firstOrDefault = this.DataSource.FirstOrDefault(address => address.Id.Equals(addressId));
                    if (firstOrDefault != null) personAddress = firstOrDefault;
                    break;
                case PageMode.Create:
                     personAddress = new PersonAddress(){Id = Guid.NewGuid(),PersonId = personId};
                    break;

            }
            ViewBag.AddressType = new SelectList(PhoneBookComponenets.Instance.AddressTypeFacade.GetAll(), "Id", "Title");
            this.PrepareViewBags(personAddress, status);
            return PartialView("PVAddress", personAddress);
        }
        [HttpPost]
        public ActionResult AddAddress(FormCollection collection)
        {
            if (string.IsNullOrEmpty(collection["Address"]))
            {
                ShowMessage("لطفا آدرس را وارد کنید", Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");
            }
            var pageMode = this.GetPageMode<PersonAddress>(collection);
            var modelKey = this.GetModelKey<PersonAddress>(collection);
            var personAddresses = this.DataSource;
            switch (pageMode)
            {

                case PageMode.Edit:
                    var firstOrDefault = personAddresses.FirstOrDefault(address => address.Id.Equals(modelKey[0].ToString().ToGuid()));
                    if (firstOrDefault != null)
                    {
                        personAddresses.Remove(firstOrDefault);
                        firstOrDefault = new PersonAddress();
                        this.RadynTryUpdateModel(firstOrDefault);
                        if (firstOrDefault.IsDefault &&
                            this.DataSource.Any(x => x.IsDefault && x.Id != firstOrDefault.Id))
                        {
                              ShowMessage("آدرس پیش فرض قبلا انتخاب شده است", Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");
                        }
                        if (firstOrDefault.AddressTypeId != 0)
                            firstOrDefault.AddressType = PhoneBookComponenets.Instance.AddressTypeFacade.Get(firstOrDefault.AddressTypeId);
                        personAddresses.Add(firstOrDefault);
                        return Content("true");
                    }
                    break;
                case PageMode.Create:
                    var personAddress = new PersonAddress();
                    this.RadynTryUpdateModel(personAddress);
                    if (personAddress.IsDefault &&
                        this.DataSource.Any(x => x.IsDefault && x.Id != personAddress.Id))
                    {
                        ShowMessage("آدرس پیش فرض قبلا انتخاب شده است", Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                        return Content("false");
                    }
                    if (personAddress.AddressTypeId != 0)
                        personAddress.AddressType = PhoneBookComponenets.Instance.AddressTypeFacade.Get(personAddress.AddressTypeId);
                    personAddresses.Add(personAddress);
                    return Content("true");
                    break;

            }
            return Content("false");

        }
        public ActionResult RemoveAddress(Guid addressId)
        {
            var firstOrDefault = this.DataSource.FirstOrDefault(address => address.Id.Equals(addressId));
            return firstOrDefault != null ? Content(this.DataSource.Remove(firstOrDefault) ? "true" : "false") : Content("false");
        }
      

      
     



     
     
        



    }
}