using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.FileManager.Controllers
{
    public class FileUploadController : WebDesignBaseController
    {

        public ActionResult FileUploaderViewMode(Guid? fileid, string fileName, bool showimage = false)
        {
            ViewBag.FileName = fileName;
            ViewBag.Fileid = fileid;
            ViewBag.IsImage = showimage;
            return PartialView("ViewFileUpload");
        }
        
        public ActionResult MultiFileUploader(string fileName)
        {
            ViewBag.FileName = fileName;
            ViewBag.multiselect = true;
            ViewBag.IsImage = false;
            return PartialView("FileUpload");
        }
        public ActionResult FileUploader(Guid? fileid,  string fileName, string filePropName,bool showimage=false)
        {
            ViewBag.FileName = fileName;
            ViewBag.filePropName = filePropName;
            ViewBag.Fileid = fileid;
            ViewBag.multiselect = false;
            ViewBag.IsImage = showimage;
            return PartialView("FileUpload");
        }
        public ActionResult FileUploaderWithCustomUrl(Guid? fileid, string fileName, string filePropName, string saveurl, string removeurl, bool showimage = false)
        {
            ViewBag.FileName = fileName;
            ViewBag.filePropName = filePropName;
            ViewBag.Fileid = fileid;
            ViewBag.multiselect = false;
            ViewBag.IsImage = showimage;
            ViewBag.saveurl = saveurl;
            ViewBag.removeurl = removeurl;
            return PartialView("FileUpload");
        }

        [HttpPost]
        public ActionResult UploadFile(bool Multi = false)
        {
            try
            {
                if (Request.Files.Count <= 0 || string.IsNullOrEmpty(Request.Params["Key"])) Content("false");
                var key = Request.Params["Key"];
                if (Multi)
                {
                    RadynSession[key] = new List<HttpPostedFileBase>();
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        ((List<HttpPostedFileBase>)RadynSession[key]).Add(Request.Files[i]);
                    }
                }
                else
                    RadynSession[key] = Request.Files[0];
                return Content("true");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");
            }
        }
        public ActionResult RemoveFile(string fileName, string selectfilename=null)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName)) return Content("false");
                if (string.IsNullOrEmpty(selectfilename))
                    RadynSession.Remove(fileName);
                else
                {
                    var fileBase = ((List<HttpPostedFileBase>)RadynSession[fileName]).FirstOrDefault(x=>x.FileName.ToLower()==selectfilename.ToLower());
                    if (fileBase != null)
                        ((List<HttpPostedFileBase>) RadynSession[fileName]).Remove(fileBase);
                }
                return Content("true");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");
            }
        }
    }
}