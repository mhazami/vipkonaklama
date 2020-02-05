using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Radyn.Slider;
using Radyn.Slider.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Slider.Controllers
{
    public class SlideItemController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(short slideId, string externalurl = "")
        {
            ViewBag.SlideId = slideId;
            ViewBag.externalurl = externalurl;
            var list = SliderComponent.Instance.SlideItemFacade.Where(x => x.SlideId == slideId);
            var model = SliderComponent.Instance.SlideFacade.Get(slideId);
            ViewBag.Slide = model;
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(short Id)
        {
            ViewBag.externalurl = Request.QueryString["externalurl"];
            ViewBag.Id = Id;
            return View();
        }
        public ActionResult GetDetail(short Id)
        {
            var slideItem = SliderComponent.Instance.SlideItemFacade.Get(Id);
            if (slideItem == null) return Content("false");
            return PartialView("PVDetails", slideItem);
        }
        public ActionResult GetModify(short? Id, string culture)
        {
            if (string.IsNullOrEmpty(culture)) culture = SessionParameters.Culture;
            if (!Id.HasValue) return PartialView("PVModify", new SlideItem() { Enabled = true });
            var languageContent = SliderComponent.Instance.SlideItemFacade.GetLanuageContent(culture, Id);
            return PartialView("PVModify", languageContent);
        }
        [RadynAuthorize]
        public ActionResult Create()
        {
            ViewBag.externalurl = Request.QueryString["externalurl"];
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var slideItem = new SlideItem();
            try
            {
                this.RadynTryUpdateModel(slideItem);
                HttpPostedFileBase image = null;
                if (RadynSession["ImageId"] != null)
                {
                    image = (HttpPostedFileBase)RadynSession["ImageId"];
                    RadynSession.Remove("ImageId");
                }
                slideItem.SlideId = Request.QueryString["slideId"].ToShort();
                slideItem.CurrentUICultureName = collection["LanguageId"];

                if (string.IsNullOrEmpty(slideItem.Title))
                {
                    ShowMessage(Resources.Slider.PleaseEnterTitle, Resources.Common.MessaageTitle,
                        messageIcon: MessageIcon.Error);
                    return RedirectToAction("Create", new { SlideId = slideItem.SlideId, externalurl = Request.QueryString["externalurl"] });
                }

                if (SliderComponent.Instance.SlideItemFacade.Insert(slideItem, image))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                        messageIcon: MessageIcon.Succeed);
                    var aftersubmitaction = new Dictionary<FormSubmitAction, string>
                    {
                        {
                            FormSubmitAction.Save,
                            "/Slider/SlideItem/Index?SlideId=" + slideItem.SlideId + "&externalurl=" +
                            Request.QueryString["externalurl"]
                        },
                        {
                            FormSubmitAction.SaveAndNew,
                            "/Slider/SlideItem/Create?externalurl=" + Request.QueryString["externalurl"]
                        },
                        {
                            FormSubmitAction.SaveAndNewLanguage,
                          "/Slider/SlideItem/Edit?Id="+slideItem.Id+"&externalurl=" + Request.QueryString["externalurl"]
                        }
                    };
                    return this.SubmitRedirect(collection, aftersubmitaction, true);

                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                    messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { SlideId = slideItem.SlideId, externalurl = Request.QueryString["externalurl"] });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.externalurl = Request.QueryString["externalurl"];
                return View(slideItem);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Int16 Id)
        {

            ViewBag.externalurl = Request.QueryString["externalurl"];
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Int16 Id, FormCollection collection)
        {
            var slideItem = SliderComponent.Instance.SlideItemFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(slideItem);
                HttpPostedFileBase image = null;
                if (RadynSession["ImageId"] != null)
                {
                    image = (HttpPostedFileBase)RadynSession["ImageId"];
                    RadynSession.Remove("ImageId");
                }
                slideItem.CurrentUICultureName = collection["LanguageId"];


                if (string.IsNullOrEmpty(slideItem.Title))
                {
                    ShowMessage(Resources.Slider.PleaseEnterTitle, Resources.Common.MessaageTitle,
                        messageIcon: MessageIcon.Error);
                    return RedirectToAction("Edit", new { Id = Id, SlideId = slideItem.SlideId, externalurl = Request.QueryString["externalurl"] });
                }
                if (SliderComponent.Instance.SlideItemFacade.Update(slideItem, image))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                        messageIcon: MessageIcon.Succeed);
                    var aftersubmitaction = new Dictionary<FormSubmitAction, string>
                    {
                        {
                            FormSubmitAction.Save,
                            "/Slider/SlideItem/Index?SlideId=" + slideItem.SlideId + "&externalurl=" +
                            Request.QueryString["externalurl"]
                        },
                        {
                            FormSubmitAction.SaveAndNew,
                            "/Slider/SlideItem/Create?externalurl=" + Request.QueryString["externalurl"]
                        },
                        {
                            FormSubmitAction.SaveAndNewLanguage,
                            "/Slider/SlideItem/Edit?Id="+Id+"&externalurl=" + Request.QueryString["externalurl"]
                        }
                    };
                    return this.SubmitRedirect(collection, aftersubmitaction, true);
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { SlideId = slideItem.SlideId, externalurl = Request.QueryString["externalurl"] });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.externalurl = Request.QueryString["externalurl"];
                ViewBag.Id = Id;
                return View(slideItem);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Int16 Id)
        {
            ViewBag.externalurl = Request.QueryString["externalurl"];
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Int16 Id, FormCollection collection)
        {
            var slideItem = SliderComponent.Instance.SlideItemFacade.Get(Id);
            try
            {
                if (SliderComponent.Instance.SlideItemFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,
                        messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { slideItem.SlideId, externalurl = Request.QueryString["externalurl"] });
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,
                    messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { slideItem.SlideId, externalurl = Request.QueryString["externalurl"] });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.externalurl = Request.QueryString["externalurl"];
                ViewBag.Id = Id;
                return View(slideItem);
            }
        }

        public ActionResult RemoveImage(HttpPostedFileBase fileBase)
        {
            RadynSession.Remove("");
            return Content("");
        }


    }
}