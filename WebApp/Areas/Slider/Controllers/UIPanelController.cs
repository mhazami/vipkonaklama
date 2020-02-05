using System.Web.Mvc;
using Radyn.Slider;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.Slider.Controllers
{
    public class UIPanelController : WebDesignBaseController
    {

   
        public ActionResult GetWebDesignMinSlideShow()
        {
            if (this.WebSite.Configuration.MiniSlideId == null) return null;
            ViewBag.Id = "WebSiteMinSlideShow";
            return PartialView("PVWebDesignSlideShow", (short)this.WebSite.Configuration.MiniSlideId);
        }
        public ActionResult GetWebDesignAverageSlideShow()
        {
            if (this.WebSite.Configuration.AverageSlideId == null) return null;
            ViewBag.Id = "WebSiteAverageSlideShow";
            return PartialView("PVWebDesignSlideShow", (short)this.WebSite.Configuration.AverageSlideId);
        }
        public ActionResult GetWebDesignBigSlideShow()
        {
            if (this.WebSite.Configuration.BigSlideId == null) return null;
            ViewBag.Id = "WebSiteBigSlideShowSlideShow";
            return PartialView("PVWebDesignSlideShow", (short)this.WebSite.Configuration.BigSlideId);
        }





        public ActionResult GetSlideShow(short id,string name)
        {
            if (id == 0) return null;
            var list = SliderComponent.Instance.SlideFacade.GetSlideWithSliders(id);
            if (list == null) return null;
            ViewBag.Id = name;
            return PartialView("PVSlideShow", list);
        }
        


    }

}
