using System;
using System.Web;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.Common.DataStructure;
using Radyn.EnterpriseNode;
using Radyn.EnterpriseNode.DataStructure;
using Radyn.Framework;
using Radyn.WebApp.AppCode.Base;
using Radyn.Utility;

namespace Radyn.WebApp.Areas.EnterpriseNode.Controllers
{
    public class EnterPriseNodeController : WebDesignBaseController
    {

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
                    ViewBag.ProvinceList = new SelectList(CommonComponent.Instance.ProvinceFacade.SelectKeyValuePair(c => c.Id, c => c.Title, new OrderByModel<Province>() { Expression = x => x.Title }), "Key", "Value");
                    return PartialView("PVEnterpriseNodeInfo", enterpriseNode);
                    break;

            }


            return null;


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
        public ActionResult GenerateLegalInfo(string state, Guid id)
        {
            var objectState = state.ToEnum<Radyn.Common.Definition.ObjectState>();
            LegalEnterpriseNode legalEnterpriseNode = null;
            switch (objectState)
            {
                case Radyn.Common.Definition.ObjectState.Create:
                    legalEnterpriseNode = new LegalEnterpriseNode();
                    if (!string.IsNullOrEmpty(Request.QueryString["title"]))
                        legalEnterpriseNode.Title = Request.QueryString["title"];
                    break;
                case Radyn.Common.Definition.ObjectState.Edit:
                    legalEnterpriseNode =
                        EnterpriseNodeComponent.Instance.LegalEnterpriseNodeFacade.Get(id);
                    break;
                case Radyn.Common.Definition.ObjectState.Details:
                    legalEnterpriseNode =
                          EnterpriseNodeComponent.Instance.LegalEnterpriseNodeFacade.Get(id);
                    break;
                case Radyn.Common.Definition.ObjectState.Delete:

                    break;
                case Radyn.Common.Definition.ObjectState.List:
                    break;
              
            }
            return PartialView("Legalinfo", legalEnterpriseNode);
        }
        public ActionResult GenerateLegalInfoDetails(string state, Guid id)
        {
            var objectState = state.ToEnum<Radyn.Common.Definition.ObjectState>();
            LegalEnterpriseNode legalEnterpriseNode = null;
            switch (objectState)
            {
                case Radyn.Common.Definition.ObjectState.Create:
                    legalEnterpriseNode = new LegalEnterpriseNode();

                    break;
                case Radyn.Common.Definition.ObjectState.Edit:

                    break;
                case Radyn.Common.Definition.ObjectState.Details:
                    legalEnterpriseNode =
                        EnterpriseNodeComponent.Instance.LegalEnterpriseNodeFacade.Get(id);

                    break;
                case Radyn.Common.Definition.ObjectState.Delete:
                    legalEnterpriseNode =
                      EnterpriseNodeComponent.Instance.LegalEnterpriseNodeFacade.Get(id);
                    break;
                case Radyn.Common.Definition.ObjectState.List:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
            return PartialView("LegalinfoDetails", legalEnterpriseNode);
        }

        public ActionResult GenerateRealInfo(string state, Guid id)
        {
            var objectState = state.ToEnum<Radyn.Common.Definition.ObjectState>();
            RealEnterpriseNode realEnterpriseNode = null;
           switch (objectState)
            {
                case Radyn.Common.Definition.ObjectState.Create:
                    realEnterpriseNode = new RealEnterpriseNode();
                    if (!string.IsNullOrEmpty(Request.QueryString["name"]))
                        realEnterpriseNode.FirstName = Request.QueryString["name"];
                    if (!string.IsNullOrEmpty(Request.QueryString["family"]))
                        realEnterpriseNode.LastName = Request.QueryString["family"];
                    realEnterpriseNode.Gender = string.IsNullOrEmpty(Request.QueryString["Gender"]) ||
                                                Request.QueryString["Gender"].ToBool();
                    break;
                case Radyn.Common.Definition.ObjectState.Edit:
                    realEnterpriseNode = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get(id).RealEnterpriseNode;
                    break;
                case Radyn.Common.Definition.ObjectState.Details:
                    realEnterpriseNode = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get(id).RealEnterpriseNode;
                    break;
                case Radyn.Common.Definition.ObjectState.Delete:
                    realEnterpriseNode = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get(id).RealEnterpriseNode;
                    break;
                case Radyn.Common.Definition.ObjectState.List:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
            return PartialView("Realinfo", realEnterpriseNode);
        }

        public ActionResult GenerateRealInfoDetails(string state, Guid id)
        {
            var objectState = state.ToEnum<Radyn.Common.Definition.ObjectState>();
            RealEnterpriseNode realEnterpriseNode = null;
            switch (objectState)
            {
                case Radyn.Common.Definition.ObjectState.Create:
                    realEnterpriseNode = new RealEnterpriseNode();
                    break;
                case Radyn.Common.Definition.ObjectState.Edit:

                    break;
                case Radyn.Common.Definition.ObjectState.Details:
                    realEnterpriseNode =
                        EnterpriseNodeComponent.Instance.RealEnterpriseNodeFacade.Get(id);
                    break;
                case Radyn.Common.Definition.ObjectState.Delete:
                    realEnterpriseNode =
                       EnterpriseNodeComponent.Instance.RealEnterpriseNodeFacade.Get(id);
                    break;
                case Radyn.Common.Definition.ObjectState.List:
                    break;
                
            }
            return PartialView("RealInfoDetails", realEnterpriseNode);
        }

        public ActionResult GenerateEnterpriseNodeModify(string state, Guid id, string type = "n", bool ShowParent = false, bool ShowPicture = true, Guid? ParentId = null)
        {
            var objectState = state.ToEnum<Radyn.Common.Definition.ObjectState>();
            ViewBag.EnterpriseNodeType = new SelectList(EnterpriseNodeComponent.Instance.EnterpriseNodeTypeFacade.GetAll(), "Id", "Title");
            Radyn.EnterpriseNode.DataStructure.EnterpriseNode enterpriseNode = null;
            ViewBag.PrefixTitleList =
                    new SelectList(EnterpriseNodeComponent.Instance.PrefixTitleFacade.GetAll(), "Id", "DescriptionField");
            ViewBag.ProvinceList = new SelectList(CommonComponent.Instance.ProvinceFacade.SelectKeyValuePair(c => c.Id, c => c.Title, new OrderByModel<Province>() { Expression = x => x.Title }), "Key", "Value");
            switch (objectState)
            {
                case Radyn.Common.Definition.ObjectState.Create:
                    enterpriseNode = new Radyn.EnterpriseNode.DataStructure.EnterpriseNode();
                    if (ParentId != null) enterpriseNode.EnterpriseNodeParentId = ParentId;
                    break;
                case Radyn.Common.Definition.ObjectState.Edit:
                    enterpriseNode = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get(id);
                    break;
                case Radyn.Common.Definition.ObjectState.Details:
                    enterpriseNode = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get(id);
                    break;
                case Radyn.Common.Definition.ObjectState.Delete:
                    enterpriseNode = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get(id);
                    break;
                case Radyn.Common.Definition.ObjectState.List:
                    break;
                
            }

            ViewBag.Type = type;
            ViewBag.ShowParent = ShowParent;
            ViewBag.ShowPicture = ShowPicture;
            ViewBag.State = state;
            if (state == "Delete" || state == "Details") return PartialView("EnterpriseNodeDetails", enterpriseNode);
            return PartialView("EnterpriseNodeModify", enterpriseNode);
        }

        public ActionResult GenerateEnterpriseNodeDetails(string state, Guid id, string type = "n")
        {

            try
            {
                var objectState = state.ToEnum<Radyn.Common.Definition.ObjectState>();
                Radyn.EnterpriseNode.DataStructure.EnterpriseNode enterpriseNode = null;
                switch (objectState)
                {
                    case Radyn.Common.Definition.ObjectState.Create:
                        enterpriseNode = new Radyn.EnterpriseNode.DataStructure.EnterpriseNode();

                        break;
                    case Radyn.Common.Definition.ObjectState.Edit:

                        break;
                    case Radyn.Common.Definition.ObjectState.Details:
                        enterpriseNode = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get(id);
                        break;
                    case Radyn.Common.Definition.ObjectState.Delete:
                        enterpriseNode = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get(id);
                        break;
                    case Radyn.Common.Definition.ObjectState.List:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("state");
                }
                ViewBag.Type = type;
                ViewBag.State = state;
                return PartialView("EnterpriseNodeDetails", enterpriseNode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        
     

     
    }
}
