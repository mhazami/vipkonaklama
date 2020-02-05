using System;
using System.Collections.Generic;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Web.Parser;

namespace Radyn.ContentManager.BO
{
    internal class HtmlDesginBO : BusinessBase<HtmlDesgin>
    {
        public override bool Insert(IConnectionHandler connectionHandler, HtmlDesgin obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }

        public Dictionary<string, string> ReturnCustomeAttributes(IConnectionHandler connectionHandler, Guid htmdesignId)
        {
            var parser = new HtmlParser { ParseMethod = ParseMethod.All };
            var htmlDesgin = this.Get(connectionHandler, htmdesignId);
            if (htmlDesgin == null) return null;
            var htmlString = Utility.Utils.ConvertHtmlToString(htmlDesgin.Body);
            parser.Parse(htmlString);
            var list = new Dictionary<string, string>();
            foreach (var tag1 in parser.Tags)
            {
                if (tag1 == null) continue;
                var tag2 = (HtmlTag)tag1;
                if (tag2.Attributes.AttributeByName("customId") != null)
                {
                    if (!string.IsNullOrEmpty(tag2.Attributes.AttributeValue("customId")))
                    {
                        var str = tag2.Attributes.AttributeValue("customId");
                        if (tag2.Attributes.AttributeByName("dec") != null)
                        {
                            if (!string.IsNullOrEmpty(tag2.Attributes.AttributeValue("dec")))
                                str = tag2.Attributes.AttributeValue("dec");
                        }
                        list.Add(tag2.Attributes.AttributeValue("customId"), str);
                    }
                }

            }
            return list;
        }

        public override bool Delete(IConnectionHandler connectionHandler, params object[] keys)
        {

            var htmlDesginBo = new HtmlDesginBO();
            var obj = htmlDesginBo.Get(connectionHandler, keys);
            var contents = new PartialLoadBO().Any(connectionHandler,
                container => container.HtmlDesginId == obj.Id);
            if (contents)
            {
                throw new Exception("این HTML  در محتوا استفاده شده است و قابل حذف نیست");
            }
            if (!base.Delete(connectionHandler, keys))
                throw new Exception("خطایی در حذف  HTML وجود دارد");
            return true;
        }
    }
}
