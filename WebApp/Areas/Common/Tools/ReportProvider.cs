using Radyn.FileManager;

namespace Radyn.WebApp.Areas.Common.Tools
{
    public class ReportProvider
    {
        public static byte[] PreaperReport(string Id)
        {
            var file = FileManagerComponent.Instance.FileFacade.Get(Id);
            if (file == null) return null;
            return file.Content;
        }
        public static bool UpdateReport(string Id, byte[] bytes)
        {
            var file = FileManagerComponent.Instance.FileFacade.Get(Id);
            if (file == null) return false;
            file.Content = bytes;
            return FileManagerComponent.Instance.FileFacade.Update(file);
        }
    }
}