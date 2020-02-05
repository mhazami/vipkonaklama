using System;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using Radyn.FileManager.Facade;
using Radyn.Web.Mvc.Utility;

namespace Radyn.FileManager.Control
{
    public class ImageBuilder
    {
        private readonly ImageComponent component;
        private HtmlHelper Helper { get; set; }

        public ImageBuilder(ImageComponent imgae, HtmlHelper helper)
        {
            component = imgae;
            this.Helper = helper;
        }

        public ImageBuilder ID(Guid id)
        {
            this.component.Id = id;
            return this;
        }

        public void Render()
        {
            this.Helper.HtmlEncoder(this.component.Buid());
        }

        public ImageBuilder Height(int height)
        {
            this.component.Height = height;
            return this;
        }

        public ImageBuilder Width(int width)
        {
            this.component.Width = width;
            return this;
        }

        public ImageBuilder Style(string style)
        {
            this.component.Style = style;
            return this;
        }

        public ImageBuilder Title(string title)
        {
            this.component.Title = title;
            return this;
        }

        public ImageBuilder Alt(string alt)
        {
            this.component.Alt = alt;
            return this;
        }

        public ImageBuilder OtherAttribute(object otherAttr)
        {
            this.component.OtherAttribute = otherAttr;
            return this;
        }
        public ImageBuilder Download()
        {
            this.component.Download = true;
            return this;
        }

        public ImageBuilder DeleteButtonVisible(string fieldName)
        {
            this.component.DeleteButtonVisible = true;
            this.component.Name = fieldName;
            return this;
        }
        public ImageBuilder FixSize()
        {
            this.component.FixSize = true;
            return this;
        }
    }

    public class ImageComponent
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? Height { get; set; }

        public int? Width { get; set; }

        public string Style { get; set; }

        public string Title { get; set; }

        public string Alt { get; set; }

        public object OtherAttribute { get; set; }

        public bool Download { get; set; }

        public bool DeleteButtonVisible { get; set; }

        public bool FixSize { get; set; }

        public string Buid()
        {
            var sw = new StringWriter();
            var writer = new Html32TextWriter(sw);
            var query = "";
            if (FixSize)
            {
                query = "w=" + this.Width + "&h=" + this.Height;
            }

            writer.AddAttribute("id", this.Name);
            writer.AddAttribute("name", this.Name);
            writer.AddAttribute("type", "hidden");
            writer.AddAttribute("value", this.Id.ToString());
            writer.RenderBeginTag("input");
            writer.RenderEndTag();

            if (DeleteButtonVisible)
            {
                var str = "document.getElementById('" + this.Name + "').value='" + null + "';"
                    + "$('#Delete-" + this.Name + "').hide();"
                    + "$('#atag-" + this.Name + "').hide();"
                    + "$('#imagetag-" + this.Name + "').hide();";

                writer.AddAttribute("id", "Delete-" + this.Name);
                writer.AddAttribute("name", "Delete-" + this.Name);
                writer.AddAttribute("onclick", str);
                writer.AddAttribute("style", "cursor:pointer;");
                writer.AddAttribute("title", Resources.FileManager.DeleteFile);
                writer.RenderBeginTag("a");
                writer.AddAttribute("class", "fa fa-trash fa-1x trash-icon");
                writer.RenderBeginTag("i");
                writer.RenderEndTag();
                writer.RenderEndTag();

            }
            if (Download)
            {
                var file = new FileFacade().Get(Id); 
                writer.AddAttribute("Id", "atag-" + this.Name);
                writer.AddAttribute("href", FileManagerContants.FileHandlerRoot + this.Id + "/" + DateTime.Now.Ticks + "?dl=true&" + query);
                writer.AddAttribute("target", "_blank");
                writer.RenderBeginTag("a");
                if (file != null)
                    writer.WriteEncodedText(file.FullName);
                writer.RenderEndTag();
            }
            else
            {
                writer.AddAttribute("Id", "imagetag-" + this.Name);
                writer.AddAttribute("src", FileManagerContants.FileHandlerRoot + this.Id + "/" + DateTime.Now.Ticks + (string.IsNullOrEmpty(query) ? "" : "?") + query);
                if (this.Height.HasValue)
                    writer.AddAttribute("height", this.Height.Value.ToString());
                if (this.Width.HasValue)
                    writer.AddAttribute("width", this.Width.Value.ToString());
                if (!string.IsNullOrEmpty(this.Style))
                    writer.AddAttribute("style", this.Style);
                if (!string.IsNullOrEmpty(this.Title))
                    writer.AddAttribute("title", this.Title);
                if (!string.IsNullOrEmpty(this.Alt))
                    writer.AddAttribute("alt", this.Alt);
                if (this.OtherAttribute != null)
                {
                    foreach (var property in this.OtherAttribute.GetType().GetProperties())
                    {
                        writer.AddAttribute(property.Name, property.GetValue(this.OtherAttribute, null).ToString());
                    }
                }

                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
            }


            return sw.ToString();
        }
    }
}
