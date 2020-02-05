using System;
using System.Web.Mvc;
using Radyn.Slider;
using Radyn.Slider.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Slider.Controllers
{
    public class SlideController : WebDesignBaseController
    {


        public ActionResult GetDetails(short id)
        {
            var obj = SliderComponent.Instance.SlideFacade.Get(id);


            return PartialView("PVDetails", obj);
        }
        public ActionResult ShowSliderItems(short Id, string effecttype, int timeout = 4)
        {
            var list = SliderComponent.Instance.SlideFacade.GetSlideWithSliders(Id);
            ViewBag.effecttype = effecttype;
            ViewBag.Id = "SlideShow";
            ViewBag.Timeout = timeout;
            return PartialView("PartialViewSlideShow", list);
        }

        public ActionResult GetModify(short? Id)
        {
            ViewBag.effecttypeEnums =
                new SelectList(EnumUtils.ConvertEnumToIEnumerable<Radyn.Slider.Definition.Enums.SliderCycleFxType>(), "Key", "Value");

            return PartialView("PVModify", Id.HasValue ? SliderComponent.Instance.SlideFacade.Get(Id) : new Slide());
        }


        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = SliderComponent.Instance.SlideFacade.Where(gallery => gallery.IsExternal == false);
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Int16 Id)
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
            var slide = new Slide();
            try
            {
                this.RadynTryUpdateModel(slide);
                slide.EffectType = collection["EffectType"];
                slide.CurrentUICultureName = collection["LanguageId"];
                if (SliderComponent.Instance.SlideFacade.Insert(slide))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                        messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                    messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(slide);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Int16 Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Int16 Id, FormCollection collection)
        {
            var slide = SliderComponent.Instance.SlideFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(slide);
                slide.CurrentUICultureName = collection["LanguageId"];
                if (SliderComponent.Instance.SlideFacade.Update(slide))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                        messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(slide);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Int16 Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Int16 Id, FormCollection collection)
        {

            try
            {
                if (SliderComponent.Instance.SlideFacade.Delete(Id))
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
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View();
            }
        }
    }
}