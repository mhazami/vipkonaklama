using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;

namespace Radyn.ContentManager.BO
{
    internal class ContentBO : BusinessBase<Content>
    {
        public IEnumerable<Content> Search(IConnectionHandler connectionHandler, string qry)
        {
            var predicateBuilder=new PredicateBuilder<Content>();
            var b=new ContentContentBO().Select(connectionHandler,x=>x.Id,x=>x.Abstract.Contains(qry)||
            x.Text.Contains(qry)|| x.Title.Contains(qry)|| x.Subject.Contains(qry));
            if (b.Any())
                predicateBuilder.And(x => x.PageName.Contains(qry) || x.Link.Contains(qry) || x.Id.In(b));
            else
                predicateBuilder.And(x => x.PageName.Contains(qry) || x.Link.Contains(qry));
            return this.Where(connectionHandler, predicateBuilder.GetExpression());
           
        }
        public string GetHtml(IConnectionHandler connectionHandler,Content content, string culture,bool hascontainer=true, Container DefaultContrainer = null)
        {

            if (content == null) return string.Empty;
           var contentContent = new ContentContentBO().Get(connectionHandler, content.Id, culture) ??
                                 new ContentContent
                                 {
                                     Id = content.Id,
                                     Abstract = content.Abstract,
                                     Description = content.Description,
                                     Text = content.Text,
                                     Subject = content.Subject,
                                     Title = content.Title,
                                 };
            return GetHtml(content, contentContent, hascontainer, DefaultContrainer);
        }

        public string GetHtml(Content content, ContentContent contentContent, bool hascontainer = true, Container DefaultContrainer = null)
        {

            if (content == null) return string.Empty;
            var st = new StringBuilder();
            if (!string.IsNullOrEmpty(content.UserScript))
                st.Append("<script type=\"text/javascript\">" + content.UserScript + "</script>");
            var contentContainer = content.Container;
            if (DefaultContrainer != null)
                contentContainer = DefaultContrainer;
            if (contentContainer == null || !hascontainer)
                st.Append(contentContent.Text);
            else
            {

                if (contentContainer.Html == null)
                    st.Append(contentContent.Text);
                else
                {
                    var htmltext = contentContainer.Html;
                    var html = htmltext.Replace("{Body}", contentContent.Text);
                    st.Append(html.Replace("{title}", contentContent.Title));
                }

            }
            return contentContent.Text.Length < 10 ? null : st.ToString();
        }
        public override bool Insert(IConnectionHandler connectionHandler, Content obj)
        {
            if (!base.Insert(connectionHandler, obj)) return false;
            obj.Link = "/ContentManager/Content/View/" + obj.Id + "/" + (obj.Title != null ? obj.Title.FixUrlCatchall().Replace(' ', '-') : "");
            return base.Update(connectionHandler, obj);
        }
        public bool Insert(IConnectionHandler connectionHandler,Content content, ContentContent contentContent)
        {
           
                if (!this.Insert(connectionHandler, content))
                    throw new Exception("خطایی در ذخیره محتوا وجود دارد");
                if (contentContent != null)
                {
                    contentContent.Id = content.Id;
                    if (!new ContentContentBO().Insert(connectionHandler, contentContent))
                        throw new Exception("خطایی در ذخیره محتوا وجود دارد");
                }
               
                return true;
            
        }
        public bool Update(IConnectionHandler connectionHandler, Content content, ContentContent contentContent)
        {
            
                if (contentContent != null)
                {
                    if (contentContent.Id == 0)
                    {
                        contentContent.Id = content.Id;
                        if (!new ContentContentBO().Insert(connectionHandler, contentContent))
                            throw new Exception("خطایی در ذخیره محتوا وجود دارد");

                    }
                    else if (!new ContentContentBO().Update(connectionHandler, contentContent))
                        throw new Exception("خطایی در ویرایش محتوا وجود دارد");
                }
                if (!this.Update(connectionHandler, content))
                    throw new Exception("خطایی در ویرایش محتوا وجود دارد");
               
                return true;
           

        }

        public override bool Delete(IConnectionHandler connectionHandler, params object[] keys)
        {
            var obj = new ContentBO().Get(connectionHandler, keys);
            var contentContents = new ContentContentBO().Where(connectionHandler,
                contentContent => contentContent.Id == obj.Id);
            if (contentContents.Count > 0)
            {
                foreach (var contentContent in contentContents)
                {
                    if (
                        !new ContentContentBO().Delete(connectionHandler, contentContent.Id,
                            contentContent.LanguageId))
                        throw new Exception("خطایی در حذف محتوای محتوا وجود دارد");
                }
            }
            if (!base.Delete(connectionHandler, keys))
                throw new Exception("خطایی در حذف  محتوا وجود دارد");
            return true;
        }

        protected override void CheckConstraint(IConnectionHandler connectionHandler, Content item)
        {
            if (item.MenuId.HasValue)
                item.IsMenu = true;
            if (item.IsMenu.Equals(false) || (item.IsMenu && item.MenuId == null))
            {
                item.IsMenu = false;
                item.MenuId = null;
            }
            if (item.IsSection.Equals(false) || (item.IsSection && item.SectionId == null))
            {
                item.IsSection = false;
                item.SectionId = null;
            }
            if (item.HasContainer.Equals(false) || (item.HasContainer && item.ContainerId == null))
            {
                item.HasContainer = false;
                item.ContainerId = null;
            }
            if (item.Abstract != null)
            {
                if (item.Abstract.Contains("'"))
                    item.Abstract = item.Abstract.Replace("'", "''");
            }
            if (item.Text != null)
            {
                if (item.Text.Contains("'"))
                    item.Text = item.Text.Replace("'", "''");
            }
            if (item.UserScript != null)
            {
                if (item.UserScript.Contains("'"))
                    item.UserScript = item.UserScript.Replace("'", "''");
            }

            item.Link = "/ContentManager/Content/View/" + item.Id + "/" + (item.Title != null ? item.Title.FixUrlCatchall().Replace(' ', '-') : "");
            if (item.MenuId != null)
            {
                var menuBo = new MenuBO();
                var menu = menuBo.Get(connectionHandler, item.MenuId);
                menu.Link = item.Link;
                if (!menuBo.Update(connectionHandler, menu))
                    throw new Exception("خطایی در ویرایش منوی محتوا وجود دارد");
            }
            base.CheckConstraint(connectionHandler, item);
        }
    }
}
