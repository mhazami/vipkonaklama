using System;
using System.Collections.Generic;
using System.IO;
using Radyn.WebApp.AppCode.Security;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;
using Ionic.Zip;
using Radyn.ModuleGeneretor;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebDesign;


namespace Radyn.WebApp.Areas.WebDesign.Controllers
{
    public class HostPanelController : WebDesignBaseController
    {


        //        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult Index()
        {


            return View();
        }
        //        [RadynAuthorize(AccessLevel = UserType.Host)]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            string savepath = Server.MapPath("~/Templates/");
            try
            {

                HttpPostedFileBase ResourceFile = null;
                if (RadynSession["Packagefile"] != null)
                {
                    ResourceFile = (HttpPostedFileBase)RadynSession["Packagefile"];

                }


                using (ZipFile zip = ZipFile.Read(ResourceFile.InputStream))
                {
                    zip.ExtractAll(savepath, ExtractExistingFileAction.DoNotOverwrite);
                    string[] files = System.IO.Directory.GetFiles(savepath);
                    var @default = files.FirstOrDefault(x => Path.GetExtension(x).ToLower() == ".xml".ToLower());
                    if (@default != null)
                    {
                        var XmlDocument = new XmlDocument();
                        XmlDocument.Load(@default);
                        var deserialize = Radyn.ModuleGeneretor.Serialize.XmlDeserialize<ModuleXml>(XmlDocument.InnerXml);
                        var list = new List<string>();
                        ReadDirectory(savepath, list);

                        foreach (var VARIABLE in deserialize.DllModel.Dlls)
                        {
                            var v = list.FirstOrDefault(x => x == VARIABLE.Path);
                            if (v != null)
                            {
                                var source = Server.MapPath("~/Templates") + v.Replace("/", "\\");
                                var des = Server.MapPath("~/") + VARIABLE.Path.Replace("/", "\\");
                                System.IO.File.Copy(source, des, true);
                            }

                        }
                        foreach (var VARIABLE in deserialize.ResourcesModel.Resources)
                        {
                            var firstOrDefault = list.FirstOrDefault(x => x == VARIABLE.Path);
                            if (firstOrDefault != null)
                            {
                                var source = Server.MapPath("~/Templates") + firstOrDefault.Replace("/", "\\");
                                var des = Server.MapPath("~/") + VARIABLE.Path.Replace("/", "\\");
                                System.IO.File.Copy(source, des, true);
                            }
                        }
                        var list1 = new List<string>();
                        foreach (var VARIABLE in deserialize.DBModel.DBs)
                        {
                            var firstOrDefault = files.FirstOrDefault(x => Path.GetFileName(x).ToLower() == Path.GetFileName(VARIABLE.Path)?.ToLower());
                            if (firstOrDefault != null)
                            {
                                var allText = System.IO.File.ReadAllText(firstOrDefault);
                                list1.Add(allText);

                            }
                        }

                        WebDesignComponent.Instance.WebSiteFacade.ExecureQuery(list1);



                    }


                }
                System.IO.File.Delete(savepath);
                return Content("true");
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                System.IO.File.Delete(savepath);
                return Content("false");

            }
        }

        private void ReadDirectory(string path, List<string> Getfiles)
        {

            var files = Directory.GetDirectories(path);
            foreach (var subpath in files)
            {
                foreach (string file in Directory.EnumerateFiles(subpath))
                {
                    ReadFiles(file, Getfiles);
                }
                ReadDirectory(subpath, Getfiles);
            }
        }

        private void ReadFiles(string path, List<string> Getfiles)
        {
            var mapPath = Server.MapPath("~/Templates");
            var item = path.Replace(mapPath, "").Replace("\\", "/");
            Getfiles.Add(item);

        }

    }
}
