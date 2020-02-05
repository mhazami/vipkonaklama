using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Radyn.EnterpriseNode;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Tools;
using Radyn.Utility;
using Radyn.Web.Html;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.Payment.Controllers
{
    public class UserTransactionController : WebDesignBaseController
    {


        public ActionResult PayTemp(Guid Id)
        {

            return Json(new { Url = Extentions.PrepaymenyUrl(Id) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserTransaction(Guid userId, string callbackurl, string postdataUrl)
        {
            ViewBag.userId = userId;
            ViewBag.callbackurl = callbackurl;
            ViewBag.value = string.Empty;
            ViewBag.postdataUrl = postdataUrl;
            return View();
        }
        public ActionResult LookUPUserTransaction(Guid Id, string callbackurl, string postdataUrl)
        {
            ViewBag.userId = Id;
            ViewBag.callbackurl = callbackurl;
            ViewBag.value = StringUtils.Encrypt("IsAdmin");
            ViewBag.postdataUrl = postdataUrl;
            return View(new Temp());
        }

        public ActionResult GenerateUserTransaction(Guid userId, string callbackurl, string value, string postdataUrl)
        {
            ViewBag.userId = userId;
            ViewBag.callbackurl = callbackurl;
            ViewBag.value = value;
            ViewBag.postdataUrl = postdataUrl;
            return PartialView("PartialViewUserTransaction");
        }
        public ActionResult GetUserTransactionList(Guid userId, string value)
        {
            var list = PaymentComponenets.Instance.TransactionFacade.GetUserTransactions(userId);
            ViewBag.fromAdmin = !string.IsNullOrEmpty(value) && StringUtils.Decrypt(value) == "IsAdmin";
            return PartialView("PartialViewListUserTransaction", list);
        }
        public ActionResult GetUserTemp(Guid? tempId, Guid userId, string callbackurl, string postdataUrl)
        {
            var temp = tempId == null ? new Temp() : PaymentComponenets.Instance.TempFacade.Get((Guid)tempId);
            ViewBag.userId = userId;
            ViewBag.callbackurl = callbackurl;
            ViewBag.CurrencyEnums = new SelectList(
              EnumUtils.ConvertEnumToIEnumerableInLocalization<Radyn.Common.Definition.Enums.CurrencyType>().Select(
                  keyValuePair =>
                      new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Radyn.Common.Definition.Enums.CurrencyType>(),
                          keyValuePair.Value)), "Key", "Value");
            ViewBag.postdataUrl = postdataUrl;
            return PartialView("PartialViewTemp", temp);
        }
        public ActionResult AddUserTemp(FormCollection collection)
        {
            var temp = new Temp { Id = Guid.NewGuid() };
            if (!string.IsNullOrEmpty(collection["PayGroup"]) && collection["PayGroup"].ToBool())
            {
                try
                {


                    var model = new List<Guid>();
                    var firstOrDefault = collection.AllKeys.FirstOrDefault(s => s.Equals("CheckSelect"));
                    if (string.IsNullOrEmpty(collection[firstOrDefault])) return Json(new { Result = false, Url = "" }, JsonRequestBehavior.AllowGet);
                    var strings = collection[firstOrDefault].Split(',');
                    foreach (var vale in strings)
                    {
                        if (string.IsNullOrEmpty(vale)) continue;
                        model.Add(vale.ToGuid());
                    }
                    if (model.Count == 0)
                    {
                        ShowMessage(Resources.Payment.PleaseSelectTempforPayment, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                        return Json(new { Result = false, Url = "" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(collection["C-UserId"]) || string.IsNullOrEmpty(collection["C-callbackurl"]))
                    {
                        ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                             messageIcon: MessageIcon.Error);
                        return Json(new { Result = false, Url = "" }, JsonRequestBehavior.AllowGet);
                    }
                    temp.PayerId = collection["C-UserId"].ToGuid();
                    if (temp.PayerId != null)
                        temp.PayerTitle = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get((Guid)temp.PayerId).DescriptionField;
                    temp.CallBackUrl = "/Payment/UserTransaction/TempPayment?Id=" + temp.Id + "&callbackurl=" + collection["C-callbackurl"];
                    if (PaymentComponenets.Instance.TempFacade.GroupPayTemp(temp, model))
                    {
                        return Json(new { Result = true, Url = Extentions.PrepaymenyUrl(temp.Id) }, JsonRequestBehavior.AllowGet);
                    }
                    ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                              messageIcon: MessageIcon.Error);
                    return Json(new { Result = false, Url = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception exception)
                {

                    ShowExceptionMessage(exception);
                    return Json(new { Result = false, Url = "" }, JsonRequestBehavior.AllowGet);
                }
            }

            try
            {


                var dirty = !string.IsNullOrEmpty(collection["Id"]) && collection["Id"].ToGuid() != Guid.Empty;
                if (dirty)
                    temp = PaymentComponenets.Instance.TempFacade.Get(collection["Id"].ToGuid());
                this.RadynTryUpdateModel(temp, collection);
                var messageStack = new List<string>();
                if (temp.Amount == 0)
                    messageStack.Add(Resources.Payment.Please_Enter_Amount);
                if (string.IsNullOrEmpty(temp.Description))
                    messageStack.Add(Resources.Payment.PleaseEnterDescription);
                var messageBody = messageStack.Aggregate("", (current, item) => current + Tag.Li(item));
                if (messageBody != "")
                {
                    ShowMessage(messageBody, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                    return Content("false");
                }
                if (!dirty)
                {
                    if (string.IsNullOrEmpty(collection["UserId"]) || string.IsNullOrEmpty(collection["callbackurl"]))
                    {
                        ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                             messageIcon: MessageIcon.Error);
                        return Content("false");
                    }

                    temp.PayerId = collection["UserId"].ToGuid();
                    temp.PayerTitle = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get((Guid)temp.PayerId).DescriptionField;
                    temp.CallBackUrl = "/Payment/UserTransaction/TempPayment?Id=" + temp.Id + "&callbackurl=" + collection["callbackurl"];
                    if (PaymentComponenets.Instance.TempFacade.Insert(temp))
                    {
                        ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                             messageIcon: MessageIcon.Succeed);
                        return Content("true");
                    }
                }
                else
                {
                    if (PaymentComponenets.Instance.TempFacade.Update(temp))
                    {
                        ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                             messageIcon: MessageIcon.Succeed);
                        return Content("true");
                    }
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                          messageIcon: MessageIcon.Error);
                return Content("false");
            }
            catch (Exception exception)
            {

                ShowExceptionMessage(exception);
                return Content("false");
            }
        }

        public ActionResult TempPayment(Guid Id, string callbackurl)
        {
            try
            {
                return PaymentComponenets.Instance.TempFacade.RemoveTempAndReturnTransactionGroup(Id) != null
                    ? Redirect("~" + callbackurl)
                    : Redirect("~" + callbackurl);
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Redirect("~" + callbackurl);
            }
        }



    }
}
