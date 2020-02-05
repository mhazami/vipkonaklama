using System.Web.Mvc;
using Radyn.FileManager.Control;

namespace Radyn.FileManager
{
    public static class HtmlHelperExtention
    {
        public static RadynExtention RadynFile(this HtmlHelper helper)
        {
            return new RadynExtention(helper);
        }
    }

    public class RadynExtention
    {
        private HtmlHelper Helper { get; set; }

        public RadynExtention(HtmlHelper helper)
        {
            this.Helper = helper;
        }

        public ImageBuilder Image()
        {
            return new ImageBuilder(new ImageComponent(), Helper);
        }

        public FileBuilder File()
        {
            return new FileBuilder(new FileComponent(), Helper);
        }
    }

}
