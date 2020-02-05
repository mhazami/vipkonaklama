using System.Web.Mvc;
using Radyn.Advertisements;
using Radyn.Advertisements.DataStructure;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Advertisements.Controllers
{
    public class AdvertisementSectionController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            return View(AdvertisementsComponent.Instance.AdvertisementSectionFacade.GetAll());
        }

        [RadynAuthorize]
        public ActionResult Details(int id)
        {
            return View(AdvertisementsComponent.Instance.AdvertisementSectionFacade.Get(id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var advertisementSection = new Section();
            try
            {
                
                this.RadynTryUpdateModel(advertisementSection, collection);
                if(AdvertisementsComponent.Instance.AdvertisementSectionFacade.Insert(advertisementSection))
                    return RedirectToAction("Index");
                return Content("Exception");
            }
            catch
            {
                return View(advertisementSection);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(int id)
        {
            return View(AdvertisementsComponent.Instance.AdvertisementSectionFacade.Get(id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

            var advertisementSection = AdvertisementsComponent.Instance.AdvertisementSectionFacade.Get(id);
            try
            {
               
                this.RadynTryUpdateModel(advertisementSection, collection);
                if(AdvertisementsComponent.Instance.AdvertisementSectionFacade.Update(advertisementSection))
                    return RedirectToAction("Index");
                return Content("Message");
            }
            catch
            {
                return View(advertisementSection);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(int id)
        {
            return View(AdvertisementsComponent.Instance.AdvertisementSectionFacade.Get(id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var advertisementSection = AdvertisementsComponent.Instance.AdvertisementSectionFacade.Get(id);

            try
            {
                AdvertisementsComponent.Instance.AdvertisementSectionFacade.Delete(id);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View(advertisementSection);
            }
        }
    }
}
