using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.Web.Mvc;
using Radyn.Web.Mvc.Utility;
using Radyn.Web.Mvc.Utility.Interface;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Controllers
{
    public class CommonUIController : WebDesignBaseController
    {
        public ActionResult UploadFile(IEnumerable<HttpPostedFileBase> fileBase, string controlName, string sessionKey, bool multiple = false)
        {
            var file = Request.Files[controlName];

            if (file != null)
            {
                if (file.InputStream != null)
                {
                    if (multiple)
                    {
                        var list = (List<HttpPostedFileBase>)RadynSession[sessionKey];

                        if (list == null)
                        {
                            list = new List<HttpPostedFileBase> {file};
                            RadynSession[sessionKey] = list;
                        }
                        else
                            list.Add(file);
                    }
                    else
                        RadynSession[sessionKey] = file;
                }
            }
            return Content("");
        }
        public ActionResult RemoveFile(IEnumerable<HttpPostedFileBase> fileBase, string sessionKey, bool multiple = false)
        {
            if (multiple)
            {
                var list = (List<HttpPostedFileBase>)RadynSession[sessionKey];
                if (list.Count > 0)
                {
                    var file = list.FirstOrDefault(x =>
                                                       {
                                                           var fileName = Path.GetFileName(x.FileName);
                                                           return fileName != null && fileName.Equals(Request.Form["fileNames"]);
                                                       });
                    list.RemoveAt(list.IndexOf(file));
                }
                if (list.Count == 0)
                    RadynSession.Remove(sessionKey);
            }
            else
                RadynSession.Remove(sessionKey);
            return Content("");
        }
        public ActionResult SetMessage(string message)
        {
            this.ShowMessage(message);
            return Content("true");
        }

        public ActionResult GetModal(string url)
        {

            if (string.IsNullOrEmpty(url)) return null;
            var model = this.RadynRenderActionInvoke(url);
            if (model == null) return null;
            ViewBag.addformtag = Radyn.Utility.Utils.GetQueryStringValue<bool>(url, "addformtag");
            ViewBag.FullScreen = Radyn.Utility.Utils.GetQueryStringValue<bool>(url, "fullscreen");
            ViewBag.FormId = url.Split('?')[0].Replace('/', '_').Remove(0, 1) + "_" + DateTime.Now.Ticks;
            ViewBag.Viewurl = url;
            ViewBag.fki = Radyn.Utility.Utils.GetQueryStringValue<string>(url, "fki");
            ViewBag.fkd = Radyn.Utility.Utils.GetQueryStringValue<string>(url, "fkd");
            ViewBag.fkb = Radyn.Utility.Utils.GetQueryStringValue<string>(url, "fkb");
            ViewBag.Html = model.Html;
            return PartialView("PVModal", GetModel(model));

        }
        public ActionResult GetPartial(string url)
        {

            if (string.IsNullOrEmpty(url)) return null;
            var model = this.RadynRenderActionInvoke(url);
            if (model == null) return null;
            ViewBag.FormId = url.Split('?')[0].Replace('/', '_').Remove(0, 1) + "_" + DateTime.Now.Ticks;
            ViewBag.addformtag = Radyn.Utility.Utils.GetQueryStringValue<bool>(url, "addformtag");
            ViewBag.Viewurl = url;
            ViewBag.Html = model.Html;
            return PartialView("PVView", GetModel(model));

        }

        private object GetModel(RadynInvokeActionModel model)
        {
            return model == null ? new object() : model.Model;
        }



        public ActionResult GetPopMessage()
        {
            var popMessage = StaticMessagebootstrapPresenter.PopMessage();
            if (popMessage == "<div id='radynMessage'></div>") return null;
            return Json(new { Message = popMessage }, JsonRequestBehavior.AllowGet);

        }



    }
}
