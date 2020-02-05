using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.Areas.WebDesign.Tools;
using Radyn.WebDesign.Definition;

namespace Radyn.WebApp.Areas.Payment.Controllers
{
    public class WebDesignDiscountTypeController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = PaymentComponenets.Instance.WebDesignDiscountTypeFacade.GetByWebId(this.WebSite.Id);
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            ViewBag.Id = Guid.NewGuid();
            return View();
        }
        public ActionResult GenerateConfigDiscountSection()
        {
            ViewBag.Dicounttype =  PaymentComponenets.Instance.WebDesignDiscountTypeFacade.GetByWebId(this.WebSite.Id).Where(x=>x.Enabled);
            ViewBag.EnumsSource = EnumUtils.ConvertEnumToIEnumerableInLocalization<Enums.PaymentSection>().Select(
                   keyValuePair =>
                       new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Enums.PaymentSection>(),
                           keyValuePair.Value)).ToList();
            return PartialView("PartialViewConfigDiscounTypeSection", PaymentComponenets.Instance.DiscountTypeSectionFacade.GetByModelName(AppExtention.WebSiteMoudelName));
        }
        public ActionResult GenerateDiscountSection(Guid Id,bool Readonly=false)
        {
            ViewBag.Dicounttype = Id;
            ViewBag.EnumsSource = EnumUtils.ConvertEnumToIEnumerableInLocalization<Enums.PaymentSection>().Select(
                    keyValuePair =>
                        new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Enums.PaymentSection>(),
                            keyValuePair.Value)).ToList(); 
            ViewBag.Readonly = Readonly;
            return PartialView("PartialViewDiscounTypeSection", PaymentComponenets.Instance.DiscountTypeSectionFacade.GetByModelName(AppExtention.WebSiteMoudelName));
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var discountType = new DiscountType();
            try
            {
                this.RadynTryUpdateModel(discountType);

                var sectiontypes = Payment.Tools.AppExtentions.FillDiscountTypeSection(collection, AppExtention.WebSiteMoudelName);
                var discountTypeAutoCodes = Payment.Tools.AppExtentions.FillDiscountAutoCode(discountType, collection);
                discountType.CurrentUICultureName = collection["LanguageId"];
                if ( PaymentComponenets.Instance.WebDesignDiscountTypeFacade.Insert(this.WebSite.Id, discountType, sectiontypes, discountTypeAutoCodes))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return this.SubmitRedirect(collection, new { Id = discountType.Id });

                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Redirect("~/Payment/WebDesignDiscountType/Index");

            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id=Guid.NewGuid();
                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var discountType = PaymentComponenets.Instance.DiscountTypeFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(discountType);
                var sectiontypes = Payment.Tools.AppExtentions.FillDiscountTypeSection(collection, AppExtention.WebSiteMoudelName);
                var discountTypeAutoCodes = Payment.Tools.AppExtentions.FillDiscountAutoCode(discountType,collection);
                discountType.CurrentUICultureName = collection["LanguageId"];
                if (PaymentComponenets.Instance.DiscountTypeFacade.Update(discountType, AppExtention.WebSiteMoudelName, sectiontypes, discountTypeAutoCodes))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return this.SubmitRedirect(collection, new { Id = Id });

                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Redirect("~/Payment/WebDesignDiscountType/Index");

            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            
            try
            {
                if ( PaymentComponenets.Instance.WebDesignDiscountTypeFacade.Delete(this.WebSite.Id,Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return Redirect("~/Payment/WebDesignDiscountType/Index");

                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Redirect("~/Payment/WebDesignDiscountType/Index");

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
