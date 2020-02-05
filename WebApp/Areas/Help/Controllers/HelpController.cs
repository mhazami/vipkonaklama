using System;
using System.Linq;
using System.Web.Mvc;
using Radyn.Help;
using Radyn.Help.DataStructure;
using Radyn.Help.Tools;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Help.Controllers
{
    public class HelpController : WebDesignBaseController
    {
        public ActionResult Index()
        {
            var list = HelpComponent.Instance.HelpFacade.GetAll();
            return View(list);
        }

        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult Search()
        {
            return View();
        }
        [RadynAuthorize(DoAuthorize = false)]
        [HttpPost]
        public ActionResult Search(FormCollection collection)
        {
            var defaultTitle = collection["title"];
            var result = HelpComponent.Instance.HelpFacade.Search(defaultTitle);
            return result.Any() ? PartialView("PartialSearchHelpResult", result) : null;
        }

        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult ChekRealtion(string realtionid)
        {
            var sourceid = RadynSession["SourceHelpId"];
            if (sourceid == null) return Content("true");
            var collections =
                HelpComponent.Instance.HelpRelationFacade.Select(x => x.HelpRelationCollectionId, relation => relation.HelpId == (Guid)sourceid);
            return Content(collections.Any(collection => collection.Equals(Guid.Parse(realtionid))) ? "false" : "true");
        }

        [RadynAuthorize(DoAuthorize = false)]
        [HttpPost]
        public ActionResult SearchHelp(FormCollection collection)
        {
            var defaultTitle = collection["DefaultTitle"];
            var defaultConent = collection["DefaultConent"];

            var result = HelpComponent.Instance.HelpFacade.GetAll().Where(h => h.Enabled);
            if (!string.IsNullOrEmpty(defaultTitle))
                result = HelpComponent.Instance.HelpFacade.GetAll().Where(h => h.DefaultTitle.Contains(defaultTitle));
            if (!string.IsNullOrEmpty(defaultConent))
                result = HelpComponent.Instance.HelpFacade.GetAll().Where(h => h.DefaultConent.Contains(defaultConent));

            return result.Any() ? PartialView("PartialSearchHelpResult", result.ToList()) : null;
        }

        public ActionResult Details(Guid Id)
        {
            return View(HelpComponent.Instance.HelpFacade.Get(Id));
        }

        public ActionResult Create()
        {
            var a = Request;
            return View(new Radyn.Help.DataStructure.Help());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {

            var help = new Radyn.Help.DataStructure.Help();
            try
            {
                this.RadynTryUpdateModel(help, collection);
                HelpContent helpContent = new HelpContent
                {
                    Title = help.DefaultTitle,
                    Content = help.DefaultConent,
                    LanguageId = SessionParameters.Culture,
                    CreateDate = DateTime.Now,
                    LastUpdate = DateTime.Now

                };
                if (HelpComponent.Instance.HelpFacade.Insert(help))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                                 messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                             messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(help);
            }
        }

        public ActionResult Edit(Guid Id)
        {
            return View(HelpComponent.Instance.HelpFacade.Get(Id));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var help = HelpComponent.Instance.HelpFacade.Get(Id);

            try
            {
                TryUpdateModel(help);
                if (HelpComponent.Instance.HelpFacade.Update(help))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                                 messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle,
                             messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(help);
            }
        }

        public ActionResult Delete(Guid Id)
        {
            return View(HelpComponent.Instance.HelpFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {

            var help = HelpComponent.Instance.HelpFacade.Get(Id);
            try
            {
                if (HelpComponent.Instance.HelpFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,
                                 messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,
                             messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(help);
            }
        }
        public ActionResult GetHelpInformation(Guid helpId)
        {
            var help = HelpComponent.Instance.HelpFacade.Get(helpId);
            return PartialView("PartialHelpInformation", help);
        }

        public ActionResult GetRelatedHelpInAddRelation(Guid sourceHelpId)
        {
            var result = HelpComponent.Instance.HelpFacade.GetHelpRelation(sourceHelpId);
            ViewBag.SourceHelp = sourceHelpId;
            return !result.Any() ? null : PartialView("PartialShowRelatedHelp", result);
        }
        [RadynAuthorize]
        public ActionResult CreateHelpRelation(Guid helpId)
        {
            ViewBag.HelpId = helpId;
            return View();
        }

        [HttpPost]
        public ActionResult CreateHelpRelation(FormCollection collection)
        {
            string helpIds = collection["HelpId"];
            var sourceHelpId = collection["sourceHelpId"];
            if (string.IsNullOrEmpty(helpIds) || string.IsNullOrEmpty(sourceHelpId))
            {
                ShowMessage(@Resources.Help.Please_Select_Realtion, Resources.Common.ErrorInInsert, messageIcon: MessageIcon.Error);
                return View();
            }
            string[] listHelpIdStr = helpIds.Split(',');
            var listDestinationHelpIdGuid = listHelpIdStr.Select(Guid.Parse).ToList();
            var result = HelpComponent.Instance.HelpFacade.AddHelpRelation(sourceHelpId.ToGuid(),
                                                                             listDestinationHelpIdGuid);
            try
            {

                if (result)
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                                 messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                return View();

            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle,
                             messageIcon: MessageIcon.Error);
                return View(result);
            }

        }



        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult ShowRelatedHelpForAllViews(string helpId)
        {
            return View();
        }

        public ActionResult GetRealtedHelp(string helpId)
        {

            ViewBag.helpId = helpId;
            var list = HelpComponent.Instance.HelpFacade.GetHelpRelation(Guid.Parse(helpId));
            return PartialView("PartialShowRelatedHelpListInView", list);
        }
        public ActionResult ShowHelp(string helpId)
        {
            var result = HelpComponent.Instance.HelpFacade.Get(Guid.Parse(helpId));
            var contenet = result.GetContent(SessionParameters.Culture);
            return PartialView("PartialShowRelatedHelpInView", contenet);
        }
        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult DeleteHelpRelation(string helpId)
        {
            var list = helpId.Split(',');
            var helpid = Guid.Parse(list[0]);
            var sourcehelp = Guid.Parse(list[1]);
            if (sourcehelp != Guid.Empty && helpid != Guid.Empty)
            {
                if (HelpComponent.Instance.HelpFacade.DeleteHelpRealtion(sourcehelp, helpid))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);

                    if (string.IsNullOrEmpty(Request.QueryString["HelpId"]))
                        Request.QueryString["HelpId"] = sourcehelp.ToString();
                    return Content("true");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");
            }
            return Content("false");
        }
    }
}
