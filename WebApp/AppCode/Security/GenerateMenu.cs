using Radyn.Security.DataStructure;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;

namespace Radyn.WebApp.AppCode.Security
{
    public static class GenerateMenu
    {
        public static void GenerateMenuHorizontalVertical(this HtmlHelper helper, IEnumerable<Menu> enumerable, string Id)
        {
            var stringWriter = new StringWriter();
            var writer = new Html32TextWriter(stringWriter);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "list-nav  nav-panel");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            foreach (var menu in enumerable)
            {
                var value = !menu.Url.StartsWith("http://")
                   ? Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + menu.Url
                   : menu.Url;
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute(HtmlTextWriterAttribute.Href, value);
                if (menu.Children.Count > 0)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown-btn");
                
                   
                writer.RenderBeginTag(HtmlTextWriterTag.A);

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "img-nav");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.AddAttribute(HtmlTextWriterAttribute.Src, "/RadynFileHandler/" + menu.ImageId);
                writer.RenderBeginTag(HtmlTextWriterTag.Img);

                writer.RenderEndTag();//img
                writer.RenderEndTag();//div.img-nav
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(menu.Title);
                writer.RenderEndTag();//span

                writer.RenderEndTag();//a.dropdown-btn


                    if (menu.Children.Count > 0)
                    {
                        GenerateChildMenuVertical(ref writer, menu);
                    }
                writer.RenderEndTag();//li


            }
            
            writer.RenderEndTag();//ul.list-nav
            

            helper.ViewContext.Writer.Write(stringWriter.ToString());

        }

        private static void GenerateChildMenuVertical(ref Html32TextWriter writer, Menu getmenu)
        {
          
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown-container");
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            foreach (var menu in getmenu.Children)
            {
                var value = !menu.Url.StartsWith("http://")
                ? Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + menu.Url
                : menu.Url;
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute(HtmlTextWriterAttribute.Href, value);
                if (menu.Children.Count > 0)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown-btn");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "img-nav");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Src, "/RadynFileHandler/" + menu.ImageId);
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();//img.icon-upper

                writer.RenderEndTag();//div.img-nav
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(menu.Title);
                writer.RenderEndTag();//span


                //writer.AddAttribute(HtmlTextWriterAttribute.Src, String.Format("/Scripts/Radyn/RadynMenu/imgaes/keyboard_arrow_{0}.png", Resources.Design.Direction));
                //writer.AddAttribute(HtmlTextWriterAttribute.Class, "icon-upper");
                //writer.RenderBeginTag(HtmlTextWriterTag.Img);

                //writer.RenderEndTag();//img.icon-upper
                

                writer.RenderEndTag();//a.dropdown-btn


                if (menu.Children.Count > 0)
                {
                    GenerateChildMenuVertical(ref writer, menu);
                }
                writer.RenderEndTag();//li
            }

            writer.RenderEndTag();
        }
    }
}