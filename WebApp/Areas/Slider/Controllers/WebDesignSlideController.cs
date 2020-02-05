using System;
using System.Linq;
using System.Web.Mvc;
using Radyn.Slider;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.Areas.WebDesign.Tools;

namespace Radyn.WebApp.Areas.Slider.Controllers
{
    public class WebDesignSlideController : WebDesignBaseController
    {
       
        public ActionResult Index()
        {
            var slides = SliderComponent.Instance.WebDesignSliderFacade.Where(congessSlide => congessSlide.WebId == this.WebSite.Id).Select(gallery => gallery.WebSiteSlide);
            return View(slides);
        }
        public ActionResult Details(short id)
        {
            ViewBag.Id = id;
            return View();
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            ViewBag.Sliders = new SelectList(AppExtention.SlideList(), "Key", "Value");
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Create(FormCollection collection)
        {
            var slide = new Radyn.Slider.DataStructure.Slide();

            try
            {
                this.RadynTryUpdateModel(slide);
                var url = collection["Selected"];
                slide.CurrentUICultureName = collection["LanguageId"];
                if (SliderComponent.Instance.WebDesignSliderFacade.Insert(this.WebSite.Id, slide, url))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                ViewBag.Sliders = new SelectList(AppExtention.SlideList(), "Key", "Value");
                return View(slide);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(short id)
        {
            GetValue(id);
            return View();
        }
     
        [HttpPost]
        [RadynAuthorize]
        public ActionResult Edit(short id, FormCollection collection)
        {

            var slide = SliderComponent.Instance.SlideFacade.Get(id);
            try
            {
                this.RadynTryUpdateModel(slide, collection);
                var url = collection["Selected"];
                slide.CurrentUICultureName = collection["LanguageId"];
                if (SliderComponent.Instance.WebDesignSliderFacade.Update(this.WebSite.Id,slide, url))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    SessionParameters.CurrentWebSite = null;
                    return RedirectToAction("Index");

                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                ViewBag.Id = id;
                GetValue(id);
                return View(slide);
            }
        }
        private void GetValue(short id)
        {
            ViewBag.Id = id;
            var str = "";
            if (this.WebSite.Configuration.BigSlideId != null && this.WebSite.Configuration.BigSlideId == id)
                str = "1";
            if (this.WebSite.Configuration.AverageSlideId != null && this.WebSite.Configuration.AverageSlideId == id)
                str = "0";
            if (this.WebSite.Configuration.MiniSlideId != null && this.WebSite.Configuration.MiniSlideId == id)
                str = "-1";
            ViewBag.Sliders = new SelectList(AppExtention.SlideList(), "Key", "Value", str);
        }
        [RadynAuthorize]
        public ActionResult Delete(short id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Delete(short id, FormCollection collection)
        {
            var gallery = SliderComponent.Instance.SlideFacade.Get(id);
            try
            {
                if (SliderComponent.Instance.WebDesignSliderFacade.Delete(this.WebSite.Id, id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    SessionParameters.CurrentWebSite = null;
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                ViewBag.Id = id;
                return View(gallery);
            }
        }


    }
}
