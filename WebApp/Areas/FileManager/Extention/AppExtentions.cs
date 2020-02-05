using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using File = Radyn.FileManager.DataStructure.File;

namespace Radyn.WebApp.Areas.FileManager.Extention
{
    public static class AppBaseExtentions
    {
        public static readonly string[] imgExt = { ".jpg", ".gif", ".png", };
        public static bool IsImage(this File item)
        {
            return imgExt.Any(ext => ext.ToLower().Equals(item.Extension.ToLower()));
        }


        public static File PraperFile(this HttpPostedFileBase filebase,Guid? id)
        {

            if (filebase != null && filebase.ContentLength > 0)
            {

                var praperFile = new File() { Id = id.HasValue? (Guid)id: new Guid() };
                var ext = Path.GetExtension(filebase.FileName).ToLower();
                praperFile.Extension = ext;
                var bytes = new byte[filebase.ContentLength];
                filebase.InputStream.Position = 0;
                filebase.InputStream.Read(bytes, 0, filebase.ContentLength);
                praperFile.Content = bytes;
                praperFile.ContentType = filebase.ContentType;
                praperFile.FileName = filebase.FileName;
                return praperFile;



            }
            return null;
        }

        public static HttpPostedFile ToHttpPostedFile(this HttpPostedFileBase filebase)
        {
            var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
            var obj = (HttpPostedFile)constructorInfo
                      .Invoke(new object[] { filebase.FileName, filebase.ContentType, filebase.InputStream });
            return obj;
        }


    }
}