using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Radyn.FormGenerator;
using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.FormGenerator.Tools;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.FormGenerator.Controllers
{
    public class BaseControlsController : WebDesignBaseController
    {




        #region BaseControls

       

        public ActionResult BaseGenerateListControl(string Id, Guid formId, string culture)
        {
            var listControl = new ListControl();
            if (!string.IsNullOrEmpty(Id))
            {
                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId, culture);
                if (form == null) return Content("false");
                var firstOrDefault =
                form.Controls.FindControl(Id);
                listControl = (ListControl)firstOrDefault;
            }
            ViewBag.Items = listControl != null&&listControl.Items.Count>0 ? StringUtils.Encrypt(listControl.Items.SerializeListItem().Compress()) : null;
            ViewBag.Id = Id;
            ViewBag.formId = formId;
            ViewBag.culture = culture;
            return PartialView("PVBaseGenerateListControl", listControl);

        }
        public ActionResult BaseGenerateBoolControl(string Id, Guid formId, string culture)
        {
            var boolControl = new BoolControl();
            if (!string.IsNullOrEmpty(Id))
            {
                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId, culture);
                if (form == null) return Content("false");
                var firstOrDefault =
               form.Controls.FindControl(Id);
                if (firstOrDefault != null)
                    boolControl = (BoolControl)firstOrDefault;
            }
            ViewBag.BoolTextAlign = new SelectList(
               EnumUtils.ConvertEnumToIEnumerableInLocalization<Align>().Select(
                   keyValuePair =>
                       new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Align>(),
                           keyValuePair.Value)), "Key", "Value");
            ViewBag.formId = formId;
            ViewBag.culture = culture;
            return PartialView("PVBaseGenerateBoolControl", boolControl);
        }

        public ActionResult BaseGenerateTextControl(string Id, Guid formId, string culture)
        {
            var textControl = new TextControl();
            if (!string.IsNullOrEmpty(Id))
            {

                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId, culture);
                if (form == null) return Content("false");
                var firstOrDefault =
                form.Controls.FindControl(Id);
                if (firstOrDefault != null)
                    textControl = (TextControl)firstOrDefault;
            }
            ViewBag.formId = formId;
            ViewBag.culture = culture;
            return PartialView("PVBaseGenerateTextControl", textControl);
        }
        public ActionResult BaseGenerateControl(string Id, Guid formId, string culture)
        {
            var textControl = new Control() { Enable = true, Visible = true };
            if (!string.IsNullOrEmpty(Id))
            {
                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId, culture);
                if (form == null) return Content("false");
                var firstOrDefault =
                form.Controls.FindControl(Id);
                if (firstOrDefault != null)
                    textControl = (Control)firstOrDefault;
            }
            return PartialView("PVBaseGenerateControl", textControl);
        }

        #endregion




    }
}
