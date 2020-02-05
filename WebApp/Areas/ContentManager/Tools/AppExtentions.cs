using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using Radyn.ContentManager;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Definition;
using Radyn.Utility;
using Radyn.Web.Mvc.Utility;
using Radyn.Web.Parser;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.ContentManager.Tools
{
    public static class AppExtentions
    {

        public static HtmlDesgin GetHtmlDesgin(string value)
        {
            var htmlDesgin = new HtmlDesgin();
            if (string.IsNullOrEmpty(value)) return htmlDesgin;
            value = value.Replace(" ", "+");
            var decompress = value.Decompress();
            var split = decompress.Split(',');
            htmlDesgin.Id = split[0].ToGuid();
            htmlDesgin.Calbackurl = split[1];
            htmlDesgin.ShowUrl = split[2];
            htmlDesgin.Culture = split[3];
            htmlDesgin.LoadPartialUrl = split[4];
            htmlDesgin.Resourcehtml = split[5];
            return htmlDesgin;
        }

        public static string GetHtml(this Controller outcontroller, HtmlDesgin design, string htmlTitle, string resourceHtml = "", Guid? DefaultContrainerId = null, bool desginemode = false)
        {

            if (design == null || string.IsNullOrEmpty(design.Body)) return string.Empty;
            var body = design.Body;
            var partialLoadFacade = ContentManagerComponent.Instance.PartialLoadFacade;
            Container Defaultcontainer = null;
            if (DefaultContrainerId.HasValue)
                Defaultcontainer = ContentManagerComponent.Instance.ContainerFacade.Get(DefaultContrainerId);
            var parser = new HtmlParser { ParseMethod = ParseMethod.All };
            var sbElements = new StringBuilder();
            parser.Parse(body);
            HtmlTag firsttag = null;
            var htmlTag = parser.Tags.First;
            if (htmlTag != null && !string.IsNullOrEmpty(htmlTag.Name))
                firsttag = htmlTag;
            else
            {
                for (var i = 0; i < parser.Tags.Count; i++)
                {
                    if (parser.Tags[i] == null || string.IsNullOrEmpty(parser.Tags[i].Name)) continue;
                    firsttag = parser.Tags[i];
                    break;
                }
            }
            if (firsttag != null)
            {
                var span = new TagBuilder(firsttag.Name);
                foreach (HtmlAttribute attribute in htmlTag.Attributes)
                {
                    if (span.Attributes.ContainsKey(attribute.Name))
                        span.Attributes[attribute.Name] =
                            firsttag.Attributes.AttributeValue(attribute.Value);
                    else span.Attributes.Add(attribute.Name, attribute.Value);
                }
                var partialLoadlist = partialLoadFacade.GetHtmlPartials(design.Id, SessionParameters.Culture, Defaultcontainer);
                outcontroller.GetPartialHtmlByUrl(partialLoadlist, Defaultcontainer);
                if (firsttag.Tags.Count > 0)
                    AddTag(ref span, outcontroller, htmlTitle, firsttag.Tags, partialLoadlist, resourceHtml, design.Id, desginemode);
                sbElements.Append(span);
            }
            sbElements = sbElements.Replace("<theme>", "");
            sbElements = sbElements.Replace("</theme>", "");
            return sbElements.ToString();
        }

        private static void AddTag(ref TagBuilder sbElements, Controller outcontroller, string htmlTitle, HtmlTagList list, IEnumerable<PartialLoad> partialLoads, string resourceHtml, Guid htmlId, bool desginemode = false)
        {

            foreach (HtmlTag htmlTag in list)
            {
                var tag = htmlTag.NextTag;
                if (tag == null) continue;
                if (string.IsNullOrEmpty(tag.Name)) continue;
                var span = new TagBuilder(tag.Name);
                if (tag.Name == "theme" && !string.IsNullOrEmpty(resourceHtml))
                    span.InnerHtml = resourceHtml;
                if (tag.Name == "title")
                    span.SetInnerText(htmlTitle);
                if (tag.NextTag != null && tag.NextTag.IsText)
                {
                    var text = tag.NextTag.Text;
                    if (!string.IsNullOrEmpty(text.Trim()) && text.Trim() != "\r\n")
                        span.SetInnerText(text);
                }
                foreach (HtmlAttribute attribute in tag.Attributes)
                {
                    if (attribute.Name == "Radyn.Web.Parser.HtmlAttribute") continue;
                    if (span.Attributes.ContainsKey(attribute.Name))
                        span.Attributes[attribute.Name] =
                            tag.Attributes.AttributeValue(attribute.Name);
                    else
                        span.Attributes.Add(attribute.Name,
                            tag.Attributes.AttributeValue(attribute.Name));
                }
                string str;
                span.Attributes.TryGetValue("customid", out str);
                if (span.Attributes.Count > 0 && !string.IsNullOrEmpty(str))
                {

                    var attributeValue = span.Attributes["customid"];
                    var enumerable = partialLoads.Where(load => load.CustomId == attributeValue).OrderBy(load => load.position);
                    if (enumerable.Any())
                    {
                        foreach (var partialLoad in enumerable)
                        {

                            if (desginemode)
                            {
                                var stringWriter = new StringWriter();
                                var writer = new Html32TextWriter(stringWriter);
                                writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-edit");
                                writer.AddAttribute("onmouseover", "visible(this);GetDragedItem('" + partialLoad.position + "');");
                                writer.AddAttribute("onmouseout", "HideMenu(this);");
                                writer.AddAttribute("id", "row_" + partialLoad.CustomId + "_" + partialLoad.PartialId + "_" + htmlId);
                                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                                writer.Write(partialLoad.Html);
                                writer.RenderEndTag();
                                writer.AddAttribute("id", "Order_" + partialLoad.CustomId + "_" + partialLoad.position);
                                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                                writer.RenderEndTag();
                                var radynRenderActionUrl = outcontroller.RadynRenderActionUrl(
                                    "/ContentManager/UIDesginPanel/GetEditNavbar?PartialId=" + partialLoad.PartialId + "&CustomId=" + partialLoad.CustomId + "&htmlId=" + htmlId);
                                writer.Write(radynRenderActionUrl);
                                writer.RenderEndTag();
                                span.InnerHtml += stringWriter.ToString();
                            }
                            else
                            {
                                span.InnerHtml += partialLoad.Html;
                            }

                        }
                    }
                    else
                    {
                        if (desginemode)
                        {
                            span.Attributes.Add("onmouseover", "GetDragedItem('0');");
                            span.Attributes.Add("onmouseout", "HideMenu(this);");
                            span.Attributes.Add("id", "row_" + attributeValue + "_" + htmlId);
                            span.InnerHtml += String.Format("<div id=\"Order_{0}_0\"></div>", attributeValue);
                        }
                    }

                }

                if (tag.Tags.Count > 0)
                    AddTag(ref span, outcontroller, htmlTitle, tag.Tags, partialLoads, resourceHtml, htmlId, desginemode);

                sbElements.InnerHtml += span;
            }

        }

        public static string GenerateMenuHorizontalHtml(this IEnumerable<Menu> enumerable)
        {
            var stringWriter = new StringWriter();
            var writer = new Html32TextWriter(stringWriter);



            writer.RenderBeginTag(HtmlTextWriterTag.Script);
            writer.Write("jQuery(function () {" +
                         "jQuery('#example').superfish({});" +
                         "$(\"ul.sf-menu li\").addClass(\"ui-state-default\");" +
                         "$(\"ul.sf-menu li\").hover(function () { $(this).addClass('ui-state-hover'); }," +
                         "function () { $(this).removeClass('ui-state-hover'); });});");
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Style, "direction:" + Resources.Design.Direction + ";");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "sf-menu");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "float: " + Resources.Design.Align + ";");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "example");
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);


            foreach (var menu in enumerable)
            {
                if (string.IsNullOrEmpty(menu.Text))
                {
                    continue;
                }
                var link = !menu.Link.StartsWith("http://") ? Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + menu.Link : menu.Link;
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "current");
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "float: " + Resources.Design.Align + ";");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);

                writer.AddAttribute(HtmlTextWriterAttribute.Href, link);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write("&nbsp;" + menu.Text);
                writer.RenderEndTag();
                if (menu.Children.Count > 0)
                {
                    GenerateChildMenuHorizontal(ref writer, menu);
                }
                writer.RenderEndTag();
            }

            writer.RenderEndTag();
            writer.RenderEndTag();



            return stringWriter.ToString();
        }

        public static string GenerateMenuWithDynamicHtml(this IEnumerable<Menu> enumerable, MenuHtml menuHtml)
        {
            if (menuHtml == null || string.IsNullOrEmpty(menuHtml.ChildMenuBody) ||
                string.IsNullOrEmpty(menuHtml.MasterBody) || string.IsNullOrEmpty(menuHtml.HasChildBody) || string.IsNullOrEmpty(menuHtml.RootMenuBody))
                return string.Empty;
            var stringWriter = new StringBuilder();
            foreach (var menu in enumerable)
            {
                if (string.IsNullOrEmpty(menu.Text))
                {
                    continue;
                }

                if (menu.Children.Count > 0)
                {
                    var subMenuBodyreplace = menuHtml.HasChildBody.Replace("{menutitle}", menu.Text);
                    var value = subMenuBodyreplace.Replace("{menulink}", GetMenuLink(menu));
                    var stringWritercild = new StringBuilder();
                    GenerateChildMenuWithDynamicHtml(ref stringWritercild, menu, menuHtml);
                    stringWriter.Append(value.Replace("{childbody}", stringWritercild.ToString()));

                }
                else
                {
                    var replace = menuHtml.RootMenuBody.Replace("{menutitle}", menu.Text);
                    var valuesum = replace.Replace("{menulink}", GetMenuLink(menu));
                    stringWriter.Append(valuesum);
                }

            }
            var newValue = stringWriter.ToString();
            return menuHtml.MasterBody.Replace("{childbody}", newValue);




        }

        private static void GenerateChildMenuWithDynamicHtml(ref StringBuilder stringWriter, Menu getmenu, MenuHtml menuHtml)
        {

            foreach (var menu in getmenu.Children)
            {
                if (string.IsNullOrEmpty(menu.Text))
                {
                    continue;
                }
                if (menu.Children.Count > 0)
                {
                    var subMenuBodyreplace = menuHtml.HasChildBody.Replace("{menutitle}", menu.Text);
                    var value = subMenuBodyreplace.Replace("{menulink}", GetMenuLink(menu));
                    var stringWritercild = new StringBuilder();
                    GenerateChildMenuWithDynamicHtml(ref stringWritercild, menu, menuHtml);
                    stringWriter.Append(value.Replace("{childbody}", stringWritercild.ToString()));

                }
                else
                {
                    var replace = menuHtml.ChildMenuBody.Replace("{menutitle}", menu.Text);
                    var valuesum = replace.Replace("{menulink}", GetMenuLink(menu));
                    stringWriter.Append(valuesum);
                }

            }


        }

        private static string GetMenuLink(Menu menu)
        {
            var link = !menu.Link.StartsWith("http://")
                ? Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + menu.Link
                : menu.Link;
            return link;
        }

        public static string GenerateMenuHorizontalVertical(this IEnumerable<Menu> enumerable)
        {
            var stringWriter = new StringWriter();
            var writer = new Html32TextWriter(stringWriter);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "cssmenu");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            var count = 1;
            int modelcount = enumerable.Count();
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);

            foreach (var menu in enumerable)
            {
                if (string.IsNullOrEmpty(menu.Text)) continue;
                var link = !menu.Link.StartsWith("http://") ? Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + menu.Link : menu.Link;

                if (menu.Children.Count > 0)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "active has-sub");
                else if (count == modelcount)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "last");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute(HtmlTextWriterAttribute.Href, link);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(menu.Text);
                writer.RenderEndTag();
                writer.RenderEndTag();
                if (menu.Children.Count > 0)
                {
                    GenerateChildMenuVertical(ref writer, menu);
                }
                writer.RenderEndTag();

                count++;
            }

            writer.RenderEndTag();

            writer.RenderEndTag();

            return stringWriter.ToString();
        }

        private static void GenerateChildMenuHorizontal(ref Html32TextWriter writer, Menu getmenu)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "sf-menu childUl");
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);

            foreach (var menu in getmenu.Children)
            {
                if (string.IsNullOrEmpty(menu.Text))
                {
                    continue;
                }
                var link = !menu.Link.StartsWith("http://") ? Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + menu.Link : menu.Link;
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "current sc");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);

                writer.AddAttribute(HtmlTextWriterAttribute.Href, link);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write("&nbsp;" + menu.Text);
                writer.RenderEndTag();
                if (menu.Children.Count > 0)
                {
                    GenerateChildMenuHorizontal(ref writer, menu);
                }
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
        }


        private static void GenerateChildMenuVertical(ref Html32TextWriter writer, Menu getmenu)
        {

            var count = 1;
            var childcount = getmenu.Children.Count;
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            foreach (var menu in getmenu.Children)
            {
                if (string.IsNullOrEmpty(menu.Text)) continue;
                var link = !menu.Link.StartsWith("http://") ? Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + menu.Link : menu.Link;
                if (menu.Children.Count > 0)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "has-sub");
                else if (count == childcount)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "last");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);

                writer.AddAttribute(HtmlTextWriterAttribute.Href, link);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(menu.Text);
                writer.RenderEndTag();
                writer.RenderEndTag();

                if (menu.Children.Count > 0)
                {
                    GenerateChildMenuVertical(ref writer, menu);
                }
                writer.RenderEndTag();
                count++;
            }
            writer.RenderEndTag();
        }

        public static void GetPartialHtmlByUrl(this Controller outcontroller, List<PartialLoad> partiallist, Container DefaultContrainer = null)
        {


            var enumerable = partiallist.Where(x => x.Type == Enums.PartialTypes.Modual);
            if (!enumerable.Any()) return;
            var @where = enumerable.Select(x => x.Partials);
            foreach (var partials in @where)
            {
                var firstOrDefault = enumerable.FirstOrDefault(x => x.PartialId == partials.Id.ToString());
                if (firstOrDefault == null) continue;
                firstOrDefault.Html = GetPartialHtmlByUrl(outcontroller, partials, firstOrDefault.HasContainer, DefaultContrainer);


            }


        }

        public static string GetPartialHtmlByUrl(this Controller outcontroller, Partials partials, bool hascontainer = true, Container DefaultContrainer = null)
        {

            var resulthtml = string.Empty;
            if (partials == null)
                return resulthtml;
            if (!string.IsNullOrEmpty(partials.Url))
            {
                resulthtml = outcontroller.RadynRenderActionUrl(partials.Url);
            }
            var st = SetContainer(partials, resulthtml, hascontainer, DefaultContrainer);
            return st.ToString().Length < 10 ? string.Empty : st.ToString();

        }

        private static StringBuilder SetContainer(Partials partials, string resulthtml, bool hascontainer, Container DefaultContrainer = null)
        {

            var st = new StringBuilder();
            var partialsContainer = partials.Container;
            if (DefaultContrainer != null)
                partialsContainer = DefaultContrainer;
            if (partialsContainer == null || !hascontainer) st.Append(resulthtml);
            else
            {

                if (partialsContainer.Html == null) st.Append(resulthtml);
                else
                {
                    var htmltext = partialsContainer.Html;
                    var html = htmltext.Replace("{Body}", resulthtml);
                    st.Append(html.Replace("{title}", partials.Title));
                }
            }

            return st;
        }


        public static string GetHtmlByUrl(string resulthtml, string title, Container container)
        {

            var st = new StringBuilder();
            if (container == null) st.Append(resulthtml);
            else
            {

                if (container.Html == null) st.Append(resulthtml);
                else
                {
                    var htmltext = container.Html;
                    var html = htmltext.Replace("{Body}", resulthtml);
                    st.Append(html.Replace("{title}", title));
                }
            }
            return st.ToString();

        }


    }
}