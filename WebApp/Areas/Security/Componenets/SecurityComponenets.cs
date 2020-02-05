using System;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using Radyn.Security;
using Radyn.Web.Mvc;

namespace Radyn.WebApp.Areas.Security.Componenets
{
    public static class SecurityComponenets
    {
       
        public static void SearchMenu<TModel, TValue>(this HtmlHelper helper, TModel model,System.Linq.Expressions.Expression<Func<TModel, TValue>> value,int width = 800,int height=300) where TModel : class
        {
            string url = "/Security/Menu/LookUPSearch";
            var s = value.Body.ToString();
            var lastIndexOf = s.LastIndexOf(".");
            var id = s.Substring(lastIndexOf + 1, s.Length - lastIndexOf - 1);

            var stringWriter = new StringWriter();
            var writer = new Html32TextWriter(stringWriter);
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
                        var help = SecurityComponent.Instance.MenuFacade.Get(eid);
                        if (help != null)
                            writer.AddAttribute("value", help.Title);
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

            writer.AddAttribute("class", "m-btn waves-blue i-search icon-btn");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.AddAttribute("src", "/Content/Images/search_button.png");

            writer.AddAttribute("onclick", string.Format("ShowModalWithReturnValue('{0}','" + id + "','txt" + id + "');", url));
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();



            writer.AddAttribute("class", "m-btn waves-blue i-eraser icon-btn");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.AddAttribute("src", "/Content/Images/eraser.png");

            writer.AddAttribute("onclick", string.Format("document.getElementById('txt" + id + "').value='';document.getElementById('" + id + "').value='';", url, width, height));
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();

            var resourceScript = helper.ViewContext.GenerateResourceScript();

            helper.ViewContext.Writer.Write(resourceScript + stringWriter);
        }
    }
}