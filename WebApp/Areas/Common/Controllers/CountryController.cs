using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.Common.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Common.Controllers
{
    public class CountryController : WebDesignBaseController
    {

        #region IP

        public ActionResult GetIp(int? rowIp)
        {
            CountryIPRange countryIpRange;
            if (rowIp == null || rowIp == 0) countryIpRange = new CountryIPRange();
            else
            {
                var list = (List<CountryIPRange>)RadynSession["IPList"];
                countryIpRange = list.FirstOrDefault(ipRange => ipRange.Id.Equals(rowIp));
            }
            return PartialView("PVCountryIP", countryIpRange);
        }
        [HttpPost]
        public ActionResult GetIp(FormCollection collection)
        {

            if (string.IsNullOrEmpty(collection["StartRange"]) || string.IsNullOrEmpty(collection["EndRange"]))
            {
                ShowMessage(Resources.CommonComponent.IpIsUnvalid, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Content("false");
            }
            var list = (List<CountryIPRange>)RadynSession["IPList"];
            if (list == null) return Content("false");
            var id =collection["IpId"].ToInt();
            if (id == 0)
            {
                var ip = new CountryIPRange();
                this.RadynTryUpdateModel(ip);
                if (CommonComponent.Instance.CountryIpRangeFacade.ValidateIp(ip))
                {
                    ip.Id = list.Count == 0 ? 1 : list.Max(range => range.Id) + 1;
                    list.Add(ip);
                    return Content("true");
                }
                ShowMessage(Resources.CommonComponent.IpIsUnvalid, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Content("false");
            }
            var firstOrDefault = list.FirstOrDefault(countryIpRange => countryIpRange.Id.Equals(id));
            if (firstOrDefault != null) this.RadynTryUpdateModel(firstOrDefault);
            return Content("true");
        }
        public ActionResult DeleteIp(int rowIp)
        {
            var list = (List<CountryIPRange>)RadynSession["IPList"];
            var item = list.FirstOrDefault(ip => ip.Id.Equals(rowIp));
            if (item == null) return Content("false");
            list.Remove(item);
            return Content("true");
        }
        public ActionResult GetIpList(bool hiddenEdit = true)
        {
            ViewBag.hiddenEdit = hiddenEdit;
            var list = (List<CountryIPRange>)RadynSession["IPList"];
            return PartialView("PVCountryIPList", list);
        }
        public ActionResult GetIpListByCountryId(int countryId)
        {
            ViewBag.hiddenEdit = true;
            var list = CommonComponent.Instance.CountryIpRangeFacade.Where(x => x.CountryId == countryId);
            return PartialView("PVCountryIPList", list);
        }
        #endregion
        
        
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = CommonComponent.Instance.CountryFacade.GetAll();
            if (list.Count == 0) return this.Redirect("~/Common/Country/Create");
            return View(list);
        }

        public ActionResult GetDetail(int Id)
        {
            return PartialView("PVDetails", CommonComponent.Instance.CountryFacade.Get(Id));
        }
        public ActionResult GetModify(int? Id)
        {
            RadynSession["IPList"] = Id.HasValue
                ? CommonComponent.Instance.CountryIpRangeFacade.Where(x => x.CountryId == (int) Id)
                : new List<CountryIPRange>();
            ViewBag.LanguageList = new SelectList(CommonComponent.Instance.LanguageFacade.GetValidList(), "Id", "DisplayName");
            return PartialView("PVModify", Id.HasValue ? CommonComponent.Instance.CountryFacade.Get(Id) : new Country());
        }


        [RadynAuthorize]
        public ActionResult Details(Int32 Id)
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
            var country = new Country();
            try
            {
                this.RadynTryUpdateModel(country);
                HttpPostedFileBase file = null;
                if (RadynSession["UpFileImage"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["UpFileImage"];
                    RadynSession.Remove("UpFileImage");
                }
                var list = new List<CountryIPRange>();
                if (RadynSession["IPList"] != null)
                {
                    list = (List<CountryIPRange>)RadynSession["IPList"];
                    RadynSession.Remove("IPList");
                }
                if (CommonComponent.Instance.CountryFacade.Insert(country,list,file))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return (!string.IsNullOrEmpty(Request.QueryString["AddNew"])) ? this.Redirect("~/Common/Country/Create") : this.Redirect("~/Common/Country/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Common/Country/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(country);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Int32 Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Int32 Id, FormCollection collection)
        {
            var country = CommonComponent.Instance.CountryFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(country);
                HttpPostedFileBase file = null;
                if (RadynSession["UpFileImage"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["UpFileImage"];
                    RadynSession.Remove("UpFileImage");
                }
                var list = new List<CountryIPRange>();
                if (RadynSession["IPList"] != null)
                {
                    list = (List<CountryIPRange>)RadynSession["IPList"];
                    RadynSession.Remove("IPList");
                }
                if (CommonComponent.Instance.CountryFacade.Update(country,list,file))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Common/Country/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Common/Country/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(country);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Int32 Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Int32 Id, FormCollection collection)
        {
            var country = CommonComponent.Instance.CountryFacade.Get(Id);
            try
            {
                if (CommonComponent.Instance.CountryFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Common/Country/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Common/Country/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(country);
            }
        }
     
    }
}
