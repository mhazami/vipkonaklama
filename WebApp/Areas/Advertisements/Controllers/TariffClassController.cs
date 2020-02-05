using System;
using System.Linq;
using System.Web.Mvc;
using Radyn.Advertisements;
using Radyn.Advertisements.DataStructure;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Advertisements.Controllers
{
    public class TariffClassController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(int SectionPositionId)
        {
            return View(AdvertisementsComponent.Instance.TariffClassFacade.GetAll().Where(x => x.SectionPositionId.Equals(SectionPositionId)));
        }

        [RadynAuthorize]
        public ActionResult Details(Guid id)
        {
            return View(AdvertisementsComponent.Instance.TariffClassFacade.Get(id));
        }

        [RadynAuthorize]
        public ActionResult Create(int SectionPositionId)
        {
            return View();
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Create(int SectionPositionId, FormCollection collection)
        {
            try
            {
                var tariffClass = new TariffClass();
                tariffClass.SectionPositionId = SectionPositionId;
                this.RadynTryUpdateModel(tariffClass, collection);
                if (AdvertisementsComponent.Instance.TariffClassFacade.Insert(tariffClass))
                    return RedirectToAction("Index", new { SectionPositionId = SectionPositionId });
                return Content("Message");
            }
            catch
            {
                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid id, int SectionPositionId)
        {

            return View(AdvertisementsComponent.Instance.TariffClassFacade.Get(id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Edit(Guid id, int SectionPositionId, FormCollection collection)
        {
            try
            {
                var tariffClass = new TariffClass();
                tariffClass.SectionPositionId = SectionPositionId;
                this.RadynTryUpdateModel(tariffClass, collection);
                if (AdvertisementsComponent.Instance.TariffClassFacade.Update(tariffClass))
                    return RedirectToAction("Index", new { SectionPositionId = SectionPositionId });
                return Content("Message");
            }
            catch
            {
                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid id, int SectionPositionId)
        {
            return View(AdvertisementsComponent.Instance.TariffClassFacade.Get(id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Delete(Guid id, int SectionPositionId, FormCollection collection)
        {
            try
            {
                AdvertisementsComponent.Instance.TariffClassFacade.Delete(id);
                return RedirectToAction("Index", new { SectionPositionId = SectionPositionId });
            }
            catch
            {
                return View();
            }
        }
    }
}
