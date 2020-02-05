using System;
using System.Web.Mvc;
using Radyn.PhoneBook;
using Radyn.WebApp.Areas.PhoneBook.Security.Filter;

namespace Radyn.WebApp.Areas.PhoneBook.Controllers
{
    public class UIPanelController : PhoneBookBaseController
    {
        // GET: PhoneBook/UserPanel
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult Details(Guid Id)
        {
            var person = PhoneBookComponenets.Instance.PersonFacade.Get(Id);
            return View(person);
        }
        [HttpPost]
        public ActionResult GetIndex(string txtsearch)
        {
            try
            {

                if(string.IsNullOrEmpty(txtsearch))return  PartialView("PVIndex", null);
                ViewBag.PhoneType = PhoneBookComponenets.Instance.PhoneTypeFacade.Where(x => x.ShowSearchResult);
                var result = PhoneBookComponenets.Instance.PersonFacade.SearchPerson(txtsearch);
                return PartialView("PVIndex", result);
            }
            catch (Exception ex)
            {

                ShowExceptionMessage(ex);
                return null;
            }
        }

       
    }
}