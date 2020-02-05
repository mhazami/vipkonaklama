using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Tools;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.Areas.Payment.Tools;
using Stimulsoft.Report;

namespace Radyn.WebApp.Areas.Payment.Controllers
{
    public class DiscountTypeController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = PaymentComponenets.Instance.DiscountTypeFacade.Where(x => x.IsExternal == false);
              
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
            return View();
        }



        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var discountType = new DiscountType();
            try
            {
                this.RadynTryUpdateModel(discountType);
              
                var list = AppExtentions.FillDiscountAutoCode(discountType, collection);
              discountType.CurrentUICultureName = collection["LanguageId"];
                if (PaymentComponenets.Instance.DiscountTypeFacade.Insert(discountType, list))
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
                return View(discountType);
            }
        }
        public ActionResult GetDetails(Guid Id)
        {
            var obj = PaymentComponenets.Instance.DiscountTypeFacade.Get(Id);
            return PartialView("PVDetails", obj);
        }
        public ActionResult GetModify(Guid? Id, string culture)
        {
            if (string.IsNullOrEmpty(culture)) culture = SessionParameters.Culture;
            ViewBag.Currency =
                new SelectList(
                    EnumUtils.ConvertEnumToIEnumerableInLocalization<Radyn.Common.Definition.Enums.CurrencyType>(),
                    "Key", "Value");
            if (!Id.HasValue) return PartialView("PVModify", new DiscountType(){Enabled = true});
            var languageContent = PaymentComponenets.Instance.DiscountTypeFacade.GetLanuageContent(culture,Id);
            return PartialView("PVModify", languageContent);
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
                var list = AppExtentions.FillDiscountAutoCode(discountType, collection);
                discountType.CurrentUICultureName = collection["LanguageId"];

                if (PaymentComponenets.Instance.DiscountTypeFacade.Update(discountType, list))
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
                return View(discountType);
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
            var paymentType = PaymentComponenets.Instance.DiscountTypeFacade.Get(Id);
            try
            {
                if (PaymentComponenets.Instance.DiscountTypeFacade.Delete(Id))
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
                return View(paymentType);
            }
        }
        public ActionResult GenerateDiscountSection(List<KeyValuePair<byte, string>> source, string modelname)
        {
            ViewBag.Dicounttype = PaymentComponenets.Instance.DiscountTypeFacade.Where(type =>
            
                type.Enabled&&
                type.IsExternal == false
            );
            ViewBag.EnumsSource = source;
            return PartialView("PartialViewDiscounTypeSection", PaymentComponenets.Instance.DiscountTypeSectionFacade.GetByModelName(modelname));
        }
        public ActionResult GenerateDiscount(string model, byte section, Guid? tId = null)
        {
            var discountTypes =
                PaymentComponenets.Instance.DiscountTypeSectionFacade.GetDiscountTypes(model, section, tId);
            ViewBag.hasdiscount = discountTypes.Any();
            System.Web.HttpContext.Current.Session[Constants.TransactionDiscountAttach] = new List<DiscountType>();
            return PartialView("PartialViewDiscount", discountTypes);
        }

        public ActionResult PrintDiscountype(Guid Id)
        {
            try
            {
                var stiReport1 = new StiReport();
                var discountType = PaymentComponenets.Instance.DiscountTypeFacade.Get(Id);
                discountType.DiscountTypeAutoCodes=PaymentComponenets.Instance.DiscountTypeAutoCodeFacade.Where(x=>x.DiscountTypeId==Id);
                stiReport1.Load(Server.MapPath("~/Areas/Payment/Reports/RptDiscountType.mrt"));
                stiReport1.RegBusinessObject("Model", discountType);
                SessionParameters.Report = stiReport1;
                return Content("");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
                return Content("false");
            }
        }
        public ActionResult UploadDicountFile(Guid Id)
        {
            var discountTypes = (List<DiscountType>)System.Web.HttpContext.Current.Session[Constants.TransactionDiscountAttach];
            var httpPostedFileBase = Request.Files[0];
            if (httpPostedFileBase != null)
            {
                if (httpPostedFileBase.InputStream == null) return Content("");
                var firstOrDefault = discountTypes.FirstOrDefault(x => x.Id == Id);
                if (firstOrDefault != null)
                   firstOrDefault.UserAttachedFile = httpPostedFileBase;
                else
                {
                    var discountType = new DiscountType() { Id = Id,UserAttachedFile = httpPostedFileBase };
                    discountTypes.Add(discountType);
                }
                return Content("true");
            }
            return Content("false");
        }
        public ActionResult DeleteAutoCode(Guid Id)
        {
            try
            {

                if (PaymentComponenets.Instance.DiscountTypeAutoCodeFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return Content("true");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
                return Content("false");
            }
        }
        public ActionResult RemoveDicountFile(Guid Id)
        {
            var httpPostedFileBases = (List<DiscountType>)System.Web.HttpContext.Current.Session[Constants.TransactionDiscountAttach];
            var discountType = httpPostedFileBases.Find(x => x.Id == Id);
            if (discountType==null) return Content("false");
            discountType.UserAttachedFile = null;
            return Content("true");
        }
        public ActionResult GenerateDiscountTyepDetail(Guid Id)
        {
            var discountType = PaymentComponenets.Instance.DiscountTypeFacade.Get(Id);
            return PartialView("PVDetails", discountType);

        }
        public ActionResult GenerateAuotocode(Guid discounttypeId, int count, int characterCount)
        {
            var list = PaymentComponenets.Instance.DiscountTypeAutoCodeFacade.GenerateAutoCode(discounttypeId, count, characterCount);
            return PartialView("PartialViewDiscountAutoCode", list);
        }
        public ActionResult GetModelAuotocode(Guid discounttypeId)
        {
            var byFilter =
           PaymentComponenets.Instance.DiscountTypeAutoCodeFacade.Where(
               x => x.DiscountTypeId == discounttypeId);
            return PartialView("PartialViewDiscountAutoCode", byFilter);
        }

     

    }
}