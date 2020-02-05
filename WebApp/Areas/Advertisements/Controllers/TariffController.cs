using System;
using System.Web.Mvc;
using Radyn.Advertisements;
using Radyn.Advertisements.DataStructure;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Advertisements.Controllers
{
    public class TariffController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            return View(AdvertisementsComponent.Instance.TariffFacade.GetAll());
        }

        [RadynAuthorize]
        public ActionResult Details(Guid id)
        {
            return View(AdvertisementsComponent.Instance.TariffFacade.Get(id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            ViewBag.TariffClass = new SelectList(AdvertisementsComponent.Instance.TariffClassFacade.GetAll(), "Id", "Title");
            return View();
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var tariff = new Tariff();
                this.RadynTryUpdateModel(tariff, collection);
                if (AdvertisementsComponent.Instance.TariffFacade.Insert(tariff))
                    return RedirectToAction("Index");
                return Content("Exception");
            }
            catch
            {
                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid id)
        {
            ViewBag.TariffClass = new SelectList(AdvertisementsComponent.Instance.TariffClassFacade.GetAll(), "Id", "Title");
            return View(AdvertisementsComponent.Instance.TariffFacade.Get(id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                var tariff = AdvertisementsComponent.Instance.TariffFacade.Get(id);
                this.RadynTryUpdateModel(tariff, collection);
                if (AdvertisementsComponent.Instance.TariffFacade.Update(tariff))
                    return RedirectToAction("Index");
                return Content("Message");
            }
            catch
            {
                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid id)
        {
            return View(AdvertisementsComponent.Instance.TariffFacade.Get(id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                AdvertisementsComponent.Instance.TariffFacade.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //public ActionResult CalculatePriceTariff(Guid tariffClassId,int? dayCount,int? clickCount,int? visitCount)
        //{
        //    AdvertisementsComponent.Instance.TariffFacade.Get(tariffClassId, dayCount, clickCount, visitCount);
        //    return JsonResult();
        //}
    }
}
