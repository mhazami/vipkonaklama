using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.Common.Component;
using Radyn.Common.DataStructure;
using Radyn.EnterpriseNode;
using Radyn.EnterpriseNode.DataStructure;
using Radyn.EnterpriseNode.Tools;
using Radyn.Framework;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.Utility;
using Radyn.WebApp.AppCode;
using Resources;
using CommonComponent = Radyn.Common.CommonComponent;

namespace Radyn.WebApp.Areas.EnterpriseNode.Controllers
{
    public class EnterPriseNodeController : LocalizedController<Radyn.EnterpriseNode.DataStructure.EnterpriseNode>
    {


     
     

      
       
        public ActionResult UploadUserImage(HttpPostedFileBase fileBase)
        {
            HttpPostedFileBase file = Request.Files["upPhotoImage"];
            if (file != null)
            {
                if (file.InputStream != null)
                {

                    RadynSession["Image"] = file;
                }
            }
            return Content("");
        }
        public ActionResult RemoveUploadImage(HttpPostedFileBase fileBase)
        {
            RadynSession.Remove("Image");
            return Content("");
        }
        public ActionResult GenerateLegalInfo(LegalEnterpriseNode legalEnterpriseNode, PageMode pageMode)
        {


            switch (pageMode)
            {

                case PageMode.Details:
                case PageMode.Delete:
                    return PartialView("LegalInfoDetails", legalEnterpriseNode);
                    break;
                case PageMode.Edit:

                case PageMode.Create:
                    return PartialView("Legalinfo", legalEnterpriseNode);
                    break;

            }


            return null;
        }
        public ActionResult GenerateRealInfo(RealEnterpriseNode realEnterpriseNode, PageMode pageMode)
        {
            
           
            switch (pageMode)
            {
               
                case PageMode.Details:
                    case PageMode.Delete:
                    return PartialView("RealInfoDetails", realEnterpriseNode);
                    break;
                case PageMode.Edit:
                  
                case PageMode.Create:
                    return PartialView("Realinfo", realEnterpriseNode);
                    break;
               
            }

          
            return null;
        }
        public ActionResult GetEnterpriseNodeInfo(Radyn.EnterpriseNode.DataStructure.EnterpriseNode enterpriseNode, PageMode pageMode)
        {

            switch (pageMode)
            {

                case PageMode.Details:
                case PageMode.Delete:
                    return PartialView("PVEnterpriseNodeInfoDetails", enterpriseNode);
                    break;
                case PageMode.Edit:

                case PageMode.Create:
                    ViewBag.Province = new SelectList(CommonComponent.Instance.ProvinceFacade.SelectKeyValuePair(c => c.Id, c => c.Title,new OrderByModel<Province>(){Expression = x=>x.Title}), "Key", "Value");
                    return PartialView("PVEnterpriseNodeInfo", enterpriseNode);
                    break;

            }


            return null;
            
           
        }
      

       
      

      

     
    }
}
