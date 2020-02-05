using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Radyn.FileManager.Facade;
using Radyn.Web.Mvc.Utility;

namespace Radyn.FileManager.Control
{
    public class FileBuilder
    {
        private readonly FileComponent component;
        private HtmlHelper Helper { get; set; }

        public FileBuilder(FileComponent imgae, HtmlHelper helper)
        {
            component = imgae;
            this.Helper = helper;
        }

        public FileBuilder ID(Guid id)
        {
            this.component.Id = id;
            return this;
        }

        public void Render()
        {
            this.Helper.HtmlEncoder(this.component.Buid());
        }

        public FileBuilder Height(int height)
        {
            this.component.Height = height;
            return this;
        }

        public FileBuilder Width(int width)
        {
            this.component.Width = width;
            return this;
        }

        public FileBuilder Style(string style)
        {
            this.component.Style = style;
            return this;
        }

        public FileBuilder Title(string title)
        {
            this.component.Title = title;
            return this;
        }

        public FileBuilder Alt(string alt)
        {
            this.component.Alt = alt;
            return this;
        }

        public FileBuilder OtherAttribute(object otherAttr)
        {
            this.component.OtherAttribute = otherAttr;
            return this;
        }
        public FileBuilder Download()
        {
            this.component.Download = true;
            return this;
        }

        public FileBuilder DeleteButtonVisible(string fieldName)
        {
            this.component.DeleteButtonVisible = true;
            this.component.Name = fieldName;
            return this;
        }
        public FileBuilder FixSize()
        {
            this.component.FixSize = true;
            return this;
        }
    }
    public class FileComponent
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
            var fileType = this.DetectFileType();
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
                    + "$('#Filetag-" + this.Name + "').hide();";
                writer.AddAttribute("id", "Delete-" + this.Name);
                writer.AddAttribute("name", "Delete-" + this.Name);
                writer.AddAttribute("type", "button");
                writer.AddAttribute("value", Resources.FileManager.DeleteFile);
                writer.AddAttribute("onclick", str);
                writer.AddAttribute("style", "width:80px;");
                writer.RenderBeginTag("input");
                writer.RenderEndTag();

            }
            var file = FileManagerComponent.Instance.FileFacade.Get(Id);
            if (Download)
            {
                writer.AddAttribute("Id", "atag-" + this.Name);
                writer.AddAttribute("href", FileManagerContants.FileHandlerRoot + this.Id + "/" + DateTime.Now.Ticks + "?dl=true&" + query);
                writer.AddAttribute("target", "_blank");
                writer.RenderBeginTag("a");
                if (file != null) writer.WriteEncodedText(file.FullName);
                writer.RenderEndTag();
            }
            else
            {
                switch (fileType)
                {
                    case FileType.Image:
                        writer.AddAttribute("Id", "Filetag-" + this.Name);
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
                        break;
                    case FileType.SWF:
                        writer.AddAttribute("codeBase",
                                            "https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7.0.19.0");
                        writer.AddAttribute("classid", "clsid:D27CDB6E-AE6D-11cf-96B8-444553540000");
                        writer.AddAttribute("width", this.Width.ToString());
                        writer.AddAttribute("height", this.Height.ToString());
                        writer.RenderBeginTag(HtmlTextWriterTag.Object);


                        writer.AddAttribute("NAME", "Movie");
                        writer.AddAttribute("VALUE", FileManagerContants.FileHandlerRoot + this.Id + "/" + DateTime.Now.Ticks);
                        writer.RenderBeginTag(HtmlTextWriterTag.Param);

                        writer.AddAttribute("src", FileManagerContants.FileHandlerRoot + this.Id + "/" + DateTime.Now.Ticks);
                        writer.AddAttribute("quality", "high");
                        writer.AddAttribute("wmode", "opaque");
                        writer.AddAttribute("pluginspage", "https://www.macromedia.com/go/getflashplayer");
                        writer.AddAttribute("type", "application/x-shockwave-flash");
                        writer.AddAttribute("width", this.Width.ToString());
                        writer.AddAttribute("height", this.Height.ToString());
                        writer.RenderBeginTag(HtmlTextWriterTag.Embed);

                        writer.RenderEndTag();
                        writer.RenderEndTag();
                        writer.RenderEndTag();
                        break;
                    case FileType.Unkown:
                        writer.AddAttribute("Id", "atag-" + this.Name);
                        writer.AddAttribute("href", FileManagerContants.FileHandlerRoot + this.Id + "/" + DateTime.Now.Ticks + "?dl=true&" + query);
                        writer.AddAttribute("target", "_blank");
                        writer.RenderBeginTag("a");
                        if (file != null) writer.WriteEncodedText(file.FullName);
                        writer.RenderEndTag();
                        break;
                }
            }

            return sw.ToString();
        }

        private FileType DetectFileType()
        {
            if (this.Id == Guid.Empty) return FileType.Unkown;
            var file = new FileFacade().Get(this.Id);
            if (file == null) return FileType.Unkown;
            if (file.Extension.ToLower().Contains("swf")) return FileType.SWF;
            var imageType = new[] { "jpg", "jpeg", "gif", "png" };
            if (imageType.Any(type => file.Extension.ToLower().Contains(type))) return FileType.Image;
            return FileType.Unkown;
        }
    }

    internal enum FileType
    {
        Image,
        SWF,
        Unkown
    }

}
