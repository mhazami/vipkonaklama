using System;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using Radyn.ContentManager;
using Radyn.Web.Mvc;

namespace Radyn.WebApp.Areas.ContentManager.Componenet
{
    public static class ContentManagerComponents
    {
       

        public static void SearchParentInMenuTree<TModel, TValue>(this HtmlHelper helper, TModel model, System.Linq.Expressions.Expression<Func<TModel, TValue>> value, string url = "/ContentManager/Menu/LookUpMenu", int width = 400, int height = 300) where TModel : class
        {
            var s = value.Body.ToString();
            var lastIndexOf = s.LastIndexOf(".");
            var id = s.Substring(lastIndexOf + 1, s.Length - lastIndexOf - 1);
            if (url == null) url = "/ContentManager/Menu/LookUpMenu";
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
                        var menu = ContentManagerComponent.Instance.MenuFacade.Get(eid);
                        if (menu != null)
                            writer.AddAttribute("value", menu.Text);
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

            //writer.AddAttribute("Type", "a");

            writer.AddAttribute("src", "/Content/Images/search_button.png");
            writer.AddAttribute("Id", "SearchBtn");

            writer.AddAttribute("onclick", string.Format("ShowModalWithReturnValue('{0}','" + id + "','txt" + id + "');", url));
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();


            writer.AddAttribute("class", "m-btn waves-blue i-eraser icon-btn");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.RenderBeginTag(HtmlTextWriterTag.Span);

            writer.AddAttribute("src", "/Content/Images/eraser.png");
            writer.AddAttribute("Id", "EraseBtn");

            writer.AddAttribute("onclick", string.Format("document.getElementById('txt" + id + "').value='';document.getElementById('" + id + "').value='';", url, width, height));
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
           
            var resourceScript = helper.ViewContext.GenerateResourceScript();

            helper.ViewContext.Writer.Write(resourceScript + stringWriter);
        }
        public static void SearchParentInSectionTree<TModel, TValue>(this HtmlHelper helper, TModel model, System.Linq.Expressions.Expression<Func<TModel, TValue>> value, string url = "/ContentManager/Section/LookUpSection", int width = 400, int height = 300) where TModel : class
        {
            var s = value.Body.ToString();
            var lastIndexOf = s.LastIndexOf(".");
            var id = s.Substring(lastIndexOf + 1, s.Length - lastIndexOf - 1);

            var stringWriter = new StringWriter();
            var writer = new Html32TextWriter(stringWriter);
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
           writer.AddAttribute("Type", "text");
            writer.AddAttribute("style", "width:89%;");
            writer.AddAttribute("readonly", "readonly");
            writer.AddAttribute("name", "txt" + id);
            writer.AddAttribute("id", "txt" + id);
            if (model != null)
            {
                var val = model.GetType().GetProperty(id).GetValue(model, null);
                if (val != null)
                {
                    int eid;
                    if (int.TryParse(val.ToString(), out eid))
                    {
                        var menu = ContentManagerComponent.Instance.SectionFacade.Get(eid);
                        if (menu != null)
                            writer.AddAttribute("value", menu.Title);
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

            writer.AddAttribute("onclick", string.Format("ShowModalWithReturnValue('{0}','" + id + "','txt" + id + "');", url));
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();


            //writer.AddAttribute("Type", "image");
            writer.AddAttribute("src", "/Content/Images/eraser.png");

            writer.AddAttribute("onclick", string.Format("document.getElementById('txt" + id + "').value='';document.getElementById('" + id + "').value='';", url, width, height));
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();

            var resourceScript = helper.ViewContext.GenerateResourceScript();
            helper.ViewContext.Writer.Write(resourceScript + stringWriter);
        }
    }
}