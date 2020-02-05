using Radyn.EnterpriseNode;
using Radyn.Payment;
using Radyn.Utility;
using Radyn.Web.Html;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Radyn.Payment.DataStructure;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebDesign;

namespace Radyn.WebApp.Areas.Payment.Controllers
{
    public class WebDesignUserPanelController : WebDesignBaseController
    {

      








   
      
   

        [WebDesignUserAuthorize]
        public ActionResult WebDesignUserTemps()
        {


            return
                Redirect("~/Payment/UserTransaction/UserTransaction?userId=" + SessionParameters.WebDesignUser.Id +
                         "&callbackurl=/WebDesign/UserPanel/Home" + "&postdataUrl=/WebDesignUserPanel/UserPanel/AddWebDesignUserUserTemp");


        }
    
        public ActionResult AddWebDesignUserUserTemp(FormCollection collection)
        {


            if (!string.IsNullOrEmpty(collection["PayGroup"]) && collection["PayGroup"].ToBool())
            {
                try
                {
                    var temp = new Temp { Id = Guid.NewGuid() };
                    temp.AdditionalData = WebDesignComponent.Instance.ConfigurationFacade.FillTempAdditionalData(this.WebSite.Id);
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
                    if (string.IsNullOrEmpty(collection["C-UserId"]))
                    {
                        ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                             messageIcon: MessageIcon.Error);
                        return Json(new { Result = false, Url = "" }, JsonRequestBehavior.AllowGet);
                    }
                    if (temp.Id == Guid.Empty) temp.Id = Guid.NewGuid();
                    temp.PayerId = collection["C-UserId"].ToGuid();
                    temp.PayerTitle = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get((Guid)temp.PayerId).DescriptionField;
                    temp.CallBackUrl = "/Payment/WebDesignUserPanel/UpdateStatusAfterTransactionGroupTemp?Id=" + temp.Id;

                    if (PaymentComponenets.Instance.TempFacade.GroupPayTemp(temp, model))
                    {
                        return Json(new { Result = true, Url = Radyn.Payment.Tools.Extentions.PrepaymenyUrl(temp.Id) }, JsonRequestBehavior.AllowGet);
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
                var temp = new Temp
                {
                    Id = Guid.NewGuid(),
                    AdditionalData =
                        WebDesignComponent.Instance.ConfigurationFacade.FillTempAdditionalData(
                            this.WebSite.Id)
                };
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
                temp.AdditionalData = WebDesignComponent.Instance.ConfigurationFacade.FillTempAdditionalData(this.WebSite.Id);
                if (!dirty)
                {
                    if (string.IsNullOrEmpty(collection["UserId"]) || string.IsNullOrEmpty(collection["callbackurl"]))
                    {
                        ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                        return Content("false");
                    }
                    if (temp.Id == Guid.Empty) temp.Id = Guid.NewGuid();
                    temp.PayerId = collection["UserId"].ToGuid();
                    temp.PayerTitle = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get((Guid)temp.PayerId).DescriptionField;
                    temp.CallBackUrl = "/Payment/WebDesignUserPanel/UpdateStatusAfterTransactionGroupTemp?Id=" + temp.Id;
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

        [WebDesignUserAuthorize]
        public ActionResult UpdateStatusAfterTransactionGroupTemp(Guid id)
        {
            try
            {
                var tr = PaymentComponenets.Instance.WebDesignDiscountTypeFacade.UpdateStatusAfterTransactionGroupTemp(SessionParameters.WebDesignUser.Id, id);
                return tr != Guid.Empty
                    ? Redirect("~/Payment/Transaction/TransactionResult?Id=" + tr + "&callbackurl=/WebDesign/UserPanel/Home")
                    : Redirect("~/WebDesign/UserPanel/Home");

            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Redirect("~/WebDesign/UserPanel/Home");
            }
        }

    }
}
