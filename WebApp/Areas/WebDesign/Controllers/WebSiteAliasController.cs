
using System;
using System.Linq;
using System.Web.Mvc;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebDesign;
using Radyn.WebDesign.DataStructure;

namespace Radyn.WebApp.Areas.WebDesign.Controllers
{
    public class WebSiteAliasController : WebDesignBaseController<WebSiteAlias>
    {

        public ActionResult GetIndex(Guid WebSiteId, PageMode status)
        {
            this.SetPageMode(status, "GetIndex");
            var list = WebDesignComponent.Instance.WebSiteAliasFacade.GetByWebSiteId(WebSiteId); ;
            if (status == PageMode.Details)
                return PartialView("PVAliasList", list);
            this.DataSource = list;
            ViewBag.WebSiteId = WebSiteId;
            return PartialView("PVIndex");

        }
        public ActionResult GetAliasList()
        {
            this.SetPageMode(PageMode.Edit, "GetIndex");
           return PartialView("PVAliasList", this.DataSource);
        }
        public ActionResult GetAlias(Guid? aliasId, Guid WebSiteId, PageMode status)
        {
            WebSiteAlias WebSiteAlias = null;
            switch (status)
            {

                case PageMode.Details:
                case PageMode.Delete:
                case PageMode.Edit:
                    var firstOrDefault = this.DataSource.FirstOrDefault(address => address.Id.Equals(aliasId));
                    if (firstOrDefault != null) WebSiteAlias = firstOrDefault;
                    break;
                case PageMode.Create:
                    WebSiteAlias = new WebSiteAlias() { Id = Guid.NewGuid(), WebSiteId = WebSiteId };
                    break;

            }
           
            this.PrepareViewBags(WebSiteAlias, status);
            return PartialView("PVAlias", WebSiteAlias);

            
        }
        [HttpPost]
        public ActionResult GetAlias(FormCollection collection)
        {



            if(string.IsNullOrEmpty(collection["Url"]))
            {
                ShowMessage("لطفا مسیر را وارد کنید", Resources.Common.MessaageTitle,
                    messageIcon: MessageIcon.Error);
                return Content("false");
            }
            var pageMode = this.GetPageMode<WebSiteAlias>(collection);
            var modelKey = this.GetModelKey<WebSiteAlias>(collection);
            var personAddresses = this.DataSource;
            switch (pageMode)
            {

                case PageMode.Edit:
                    var firstOrDefault = personAddresses.FirstOrDefault(address => address.Id.Equals(modelKey[0].ToString().ToGuid()));
                    if (firstOrDefault != null)
                    {
                        personAddresses.Remove(firstOrDefault);
                        firstOrDefault = new WebSiteAlias();
                        this.RadynTryUpdateModel(firstOrDefault);
                        personAddresses.Add(firstOrDefault);
                        return Content("true");
                    }
                    break;
                case PageMode.Create:
                    var WebSiteAlias = new WebSiteAlias();
                    this.RadynTryUpdateModel(WebSiteAlias);
                    personAddresses.Add(WebSiteAlias);
                    return Content("true");
                    break;

            }
            return Content("false");



          
        }
        public ActionResult DeleteAlias(Guid aliasId)
        {

            
            var item = this.DataSource.FirstOrDefault(ip => ip.Id.Equals(aliasId));
            if (item == null) return Content("false");
            this.DataSource.Remove(item);
            return Content("true");
        }
       
       

    }
}