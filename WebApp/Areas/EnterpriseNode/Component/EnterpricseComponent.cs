using System;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using Radyn.EnterpriseNode;
using Radyn.EnterpriseNode.Tools;
using Radyn.Web.Mvc;

namespace Radyn.WebApp.Areas.EnterpriseNode.Component
{
    public static class EnterpricseComponent
    {
        public static void SearchEnterpriseNode<TModel, TValue>(this HtmlHelper helper, TModel model, System.Linq.Expressions.Expression<Func<TModel, TValue>> value, string enterprisetype = "n", string url = "/EnterPriseNode/EnterPriseNode/Search", int width = 800, int height = 600) where TModel : class
        {
            var s = value.Body.ToString();
            var lastIndexOf = s.LastIndexOf(".");
            var id = s.Substring(lastIndexOf + 1, s.Length - lastIndexOf - 1);

            var stringWriter = new StringWriter();
            var writer = new Html32TextWriter(stringWriter);

            helper.ViewContext.AddScript("/Scripts/Radyn/ModalWindows.js");

            writer.AddAttribute("Type", "text");
            writer.AddAttribute("readonly", "readonly");
            writer.AddAttribute("name", "txt" + id);
            writer.AddAttribute("id", "txt" + id);
            if (model != null)
            {
                var val = model.GetType().GetProperty(id).GetValue(model, null);
                if (val != null)
                {
                    Guid eid;
                    if (Guid.TryParse(val.ToString(), out eid))
                    {
                        var enterpriseNode =EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get(eid);
                        if (enterpriseNode != null)
                            writer.AddAttribute("value", enterpriseNode.Title());
                    }
                }
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            writer.AddAttribute("Type", "hidden");
            writer.AddAttribute("name", id);
            writer.AddAttribute("id", id);
            if (model != null)
            {
                var val = model.GetType().GetProperty(id).GetValue(model, null);
                if (val != null)
                    writer.AddAttribute("value", val.ToString());
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();


            //writer.AddAttribute("Type", "image");
            writer.AddAttribute("src", "/Content/Images/search_button.png");
            writer.AddAttribute("style", "border-style: groove; border-width: thin;");

            writer.AddAttribute("onclick", string.Format("ShowModalWithReturnValue('{0}','" + id + "','txt" + id + "');", url + "?type=" + enterprisetype));
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();


            //writer.AddAttribute("Type", "image");
            writer.AddAttribute("src", "/Content/Images/eraser.png");
            writer.AddAttribute("style", "border-style: groove; border-width: thin;");

            writer.AddAttribute("onclick", string.Format("document.getElementById('txt" + id + "').value='';document.getElementById('" + id + "').value='';", url, width, height));
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();

            var resourceScript = helper.ViewContext.GenerateResourceScript();

            helper.ViewContext.Writer.Write(resourceScript + stringWriter);
        }
    }
}