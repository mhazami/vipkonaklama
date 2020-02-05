using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.WebDesign.DataStructure;
using Radyn.WebDesign.Definition;

namespace Radyn.WebDesign.BO
{
    internal class ResourceBO : BusinessBase<Resource>
    {

        public override bool Insert(IConnectionHandler connectionHandler, Resource obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }
        public string GetWebSiteResourceHtml(IConnectionHandler connectionHandler, Guid webId, string culture, Enums.UseLayout layout)
        {
            try
            {
                var str = new StringWriter();
                var html32TextWriter = new Html32TextWriter(str);
                var byFilter = new ResourceBO().OrderBy(connectionHandler, x => x.Order, x => x.WebId == webId && x.Enabled);
                if (!string.IsNullOrEmpty(culture))
                    new ResourceBO().GetLanuageContent(connectionHandler, culture, byFilter);

                foreach (var resource in byFilter)
                {
                    if (layout != Enums.UseLayout.None &&
                        (string.IsNullOrEmpty(resource.UseLayoutId) ||
                         (!resource.UseLayoutId.Split(',').Contains(((byte)layout).ToString()))))
                        continue;

                    switch ((Enums.ResourceType)resource.Type)
                    {
                        case Enums.ResourceType.JSFile:
                            if (resource.FileId == null) continue;
                            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
                            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Src, FileManagerContants.FileHandlerRoot + resource.FileId);
                            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Script);
                            html32TextWriter.RenderEndTag();
                            break;
                        case Enums.ResourceType.CssFile:
                            if (resource.FileId == null) continue;
                            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
                            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Rel, "stylesheet");
                            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Href, FileManagerContants.FileHandlerRoot + resource.FileId);
                            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Link);
                            html32TextWriter.RenderEndTag();
                            break;
                        case Enums.ResourceType.JSFunction:
                            if (string.IsNullOrEmpty(resource.Text)) continue;
                            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
                            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Style);
                            html32TextWriter.Write(resource.Text);
                            html32TextWriter.RenderEndTag();
                            break;
                        case Enums.ResourceType.Style:
                            if (string.IsNullOrEmpty(resource.Text)) continue;
                            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Style);
                            html32TextWriter.Write(resource.Text);
                            html32TextWriter.RenderEndTag();
                            break;
                    }
                }
                return str.ToString();
            }
            catch (KnownException knownException)
            {
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
