using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.FileManager.Controllers
{
    public class FileController : WebDesignBaseController
    {




        public ActionResult UploadFile(List<HttpPostedFileBase> fileBase)
        {
            var contentFileId = Request.Files["File"];
            if (contentFileId != null)
            {
                if (contentFileId.InputStream != null)
                {
                    RadynSession["File"] = RadynSession["File"] ?? new List<HttpPostedFileBase>();
                    ((List<HttpPostedFileBase>)RadynSession["File"]).Add(contentFileId);
                }
            }
            return Content("");
        }
        public ActionResult RemoveFile(List<HttpPostedFileBase> fileBases)
        {
            if (RadynSession["File"] == null) return Content("");
            var httpPostedFileBases = ((List<HttpPostedFileBase>)RadynSession["File"]);
            if (Request.Form.GetValues(0) != null && Request.Form.GetValues(0)[0] != null)
            {
                var firstOrDefault = httpPostedFileBases.FirstOrDefault(x => x.FileName == Request.Form.GetValues(0)[0]);
                httpPostedFileBases.Remove(firstOrDefault);
                return Content("");
            }
            return Content("");
        }

    }
}
