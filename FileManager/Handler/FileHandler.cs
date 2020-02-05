using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Radyn.FileManager.Facade;

namespace Radyn.FileManager.Handler
{
    public class FileHandler : IHttpHandler
    {
        private Guid _id;

        public FileHandler(Guid id)
        {
            _id = id;
        }

        public void ProcessRequest(HttpContext context)
        {
//            if (context.Request.Url.Host.Equals("localhost")) return;
            if (_id.Equals(Guid.Empty)) return;
            var document = new FileFacade().Get(_id);
            if(document==null)return;
            var memoryStream = new MemoryStream(document.Content) { Position = 0 };
            var ext = document.Extension.Replace(".", "").ToLower();
            switch (ext)
            {
                case "jpg":
                    ext = "Jpeg";
                    break;

                default:
                    ext = ext[0].ToString().ToUpper() + ext.Substring(1, ext.Length - 1);
                    break;
            }

            var c = new ImageFormat(Guid.NewGuid());
            var propertyInfo = c.GetType().GetProperty(ext);
            var doDownload = !string.IsNullOrEmpty(context.Request.QueryString["dl"]);
            if (doDownload)
                doDownload = bool.Parse(context.Request.QueryString["dl"]);
            if (doDownload)
            {
                var attachment = string.Format("attachment; filename={0}.{1}", document.FileName.Replace(" ","_"), document.Extension);
                context.Response.ClearContent();
                context.Response.AddHeader("content-disposition", attachment);
                context.Response.ContentType = "application/" + ext;
                context.Response.BinaryWrite(document.Content);
                context.Response.End();
                return;
            }
            if (propertyInfo != null)
            {
                var imageFormat = (ImageFormat)propertyInfo.GetValue(c, null);
                context.Response.ContentType = "image/" + imageFormat;

                try
                {
                    if (!string.IsNullOrEmpty(context.Request.QueryString["w"]) || !string.IsNullOrEmpty(context.Request.QueryString["h"]))
                    {
                        var width = int.Parse(context.Request.QueryString["w"]);
                        var height = width;
                        if (!string.IsNullOrEmpty(context.Request.QueryString["h"]))
                            height = int.Parse(context.Request.QueryString["h"]);
                        var bitmap = new System.Drawing.Bitmap(System.Drawing.Image.FromStream(memoryStream), width, height);

                        bitmap.Save(context.Response.OutputStream, imageFormat);

                    }
                    else
                        System.Drawing.Image.FromStream(memoryStream).Save(context.Response.OutputStream, imageFormat);
                }
                catch (Exception)
                {
                    context.Response.ContentType = imageFormat + "/plain";
                    System.Drawing.Image.FromStream(memoryStream).Save(context.Response.OutputStream, imageFormat);
                }
            }
            else
            {
                if (ext.ToLower().Contains("swf"))
                {
                    context.Response.AddHeader("Content-Disposition", string.Format("filename={0}", document.FileName));
                    context.Response.AddHeader("Content-Type", "application/x-shockwave-flash");
                }
                else if (ext.ToLower().Equals("js"))
                {
                    var attachment = string.Format("attachment; filename={0}.{1}", document.FileName, document.Extension);
                    context.Response.ClearContent();
                    context.Response.AddHeader("content-disposition", attachment);
                    context.Response.ContentType = "text/javascript";
                }
                else if (ext.ToLower().Equals("css"))
                {
                    var attachment = string.Format("attachment; filename={0}.{1}", document.FileName, document.Extension);
                    context.Response.ClearContent();
                    context.Response.AddHeader("content-disposition", attachment);
                    context.Response.ContentType = "text/css";
                }
                else
                {
                    var attachment = string.Format("attachment; filename={0}.{1}", document.FileName, document.Extension);
                    context.Response.ClearContent();
                    context.Response.AddHeader("content-disposition", attachment);
                    context.Response.ContentType = "application/" + ext;
                }
                
                context.Response.BinaryWrite(document.Content);
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
