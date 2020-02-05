using Radyn.Framework;
using Radyn.Security;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class LogController : WebDesignBaseController
    {
        // GET: Security/Log
        public ActionResult Index(string id, string title)
        {
            var tracker = new Tracker() { RootId = id };
            ViewBag.Operationlist = new SelectList(EnumUtils.ConvertEnumToIEnumerable<OperationLogType>(), "Value", "Value");
            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.UserNames = new SelectList(
                   SecurityComponent.Instance.TrackerFacade.GroupBy(new Expression<Func<Tracker, object>>[] { x => x.UserName },
                        x => x.RootId == id, true).Select(x => x.UserName).ToList());
            }
            ViewBag.Title = title;
            return View(tracker);
        }

        public List<Tracker> SearchLog(FormCollection collection)
        {
            var tracker = new Tracker();
            var fromTo = "";
            var fromDate = "";
            var toDate = "";
            RadynTryUpdateModel(tracker, collection);
            if (!string.IsNullOrEmpty(collection["trackOperation"]))
                tracker.Operation = collection["trackOperation"];
            if (!string.IsNullOrEmpty(collection["FromTo"]))
                fromTo = collection["FromTo"];
            if (!string.IsNullOrEmpty(collection["FromDate"]))
                fromDate = collection["FromDate"];
            if (!string.IsNullOrEmpty(collection["ToDate"]))
                toDate = collection["ToDate"];
            var result= SecurityComponent.Instance.TrackerFacade.Search(0, 0, tracker, fromDate, toDate, fromTo);
            return result;
        }
        [HttpPost]
        public ActionResult GetIndex(FormCollection collection)
        {
            try
            {
                var result = SearchLog(collection);
                return PartialView("PVGetIndex", result);
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                return Content("false");
            }
        }

        [HttpPost]
        public ActionResult GetPrint(FormCollection collection)
        {
            try
            {
                var stiReport1 = new StiReport();
                stiReport1.Load(Server.MapPath("~/Areas/Security/Reports/RptLogList.mrt"));
                stiReport1.RegBusinessObject("Model", SearchLog(collection));
                stiReport1.RegBusinessObject("ReportTitle", new { ReportTitle = collection["Title"] });
                SessionParameters.Report = stiReport1;
                return Content("true");
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                return Content("false");
            }
        }
    }
}