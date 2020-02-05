using System.Web.Mvc;
using Radyn.Advertisements;
using Radyn.Advertisements.DataStructure;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Advertisements.Controllers
{
    public class AdvertisementSectionPositionController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(int sectionId)
        {
            return View(AdvertisementsComponent.Instance.AdvertisementSectionPositionFacade.Where(x=>x.SectionId.Equals(sectionId)));
            
        }

        [RadynAuthorize]
        public ActionResult Details(int id)
        {
            return View(AdvertisementsComponent.Instance.AdvertisementSectionPositionFacade.Get(id));
        }

        [RadynAuthorize]
        public ActionResult Create(int sectionId)
        {
            return View();
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Create(int sectionId,FormCollection collection)
        {
            try
            {
                var advertisementSectionPosition = new SectionPosition();
                advertisementSectionPosition.SectionId = sectionId;
                this.RadynTryUpdateModel(advertisementSectionPosition, collection);
                if (AdvertisementsComponent.Instance.AdvertisementSectionPositionFacade.Insert(advertisementSectionPosition))
                    return RedirectToAction("Index", new { sectionId= sectionId });
                    return Content("Message");
            }
            catch
            {
                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(int id, int sectionId)
        {
            
            return View(AdvertisementsComponent.Instance.AdvertisementSectionPositionFacade.Get(id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Edit(int id,int sectionId, FormCollection collection)
        {
            try
            {
                var advertisementSectionPosition =AdvertisementsComponent.Instance.AdvertisementSectionPositionFacade.Get(id);
                this.RadynTryUpdateModel(advertisementSectionPosition, collection);
                if (AdvertisementsComponent.Instance.AdvertisementSectionPositionFacade.Update(advertisementSectionPosition))
                    return RedirectToAction("Index", new { sectionId = sectionId });
                return Content("Message");
            }
            catch
            {
                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(int id, int sectionId)
        {
            return View(AdvertisementsComponent.Instance.AdvertisementSectionPositionFacade.Get(id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Delete(int id,int sectionId, FormCollection collection)
        {
            try
            {
                AdvertisementsComponent.Instance.AdvertisementSectionPositionFacade.Delete(id);
                return RedirectToAction("Index", new { sectionId = sectionId });
            }
            catch
            {
                return View();
            }
        }
    }
}
