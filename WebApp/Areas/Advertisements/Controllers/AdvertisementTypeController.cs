using System.Web.Mvc;
using Radyn.Advertisements;
using Radyn.Advertisements.DataStructure;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Advertisements.Controllers
{
    public class AdvertisementTypeController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            return View(AdvertisementsComponent.Instance.AdvertisementTypeFacade.GetAll());
        }

        [RadynAuthorize]
        public ActionResult Details(int id)
        {
            return View(AdvertisementsComponent.Instance.AdvertisementTypeFacade.Get(id));
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
            try
            {
                var advertisementType = new AdvertisementType();
                this.RadynTryUpdateModel(advertisementType, collection);
                if (AdvertisementsComponent.Instance.AdvertisementTypeFacade.Insert(advertisementType))
                    return RedirectToAction("Index");
                return Content("Massage");
             }
            catch
            {
                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(int id)
        {
            return View(AdvertisementsComponent.Instance.AdvertisementTypeFacade.Get(id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var advertisementType = new AdvertisementType();
                this.RadynTryUpdateModel(advertisementType, collection);
                if (AdvertisementsComponent.Instance.AdvertisementTypeFacade.Update(advertisementType)) 
                        return RedirectToAction("Index");
                return Content("Message");
            }
            catch
            {
                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(int id)
        {
            return View(AdvertisementsComponent.Instance.AdvertisementTypeFacade.Get(id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                AdvertisementsComponent.Instance.AdvertisementTypeFacade.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
