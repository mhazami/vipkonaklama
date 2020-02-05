using System.Collections.Generic;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Facade;

namespace Radyn.ContentManager.Tools
{
    public static class Extentions
    {
        public static ContentContent GetContent(this Content content,string culture)
        {
            var firstOrDefault = new ContentContentFacade().Get(content.Id, culture);
            if (firstOrDefault != null) return firstOrDefault;
            firstOrDefault = new ContentContent
            {
                Id = content.Id,
                Abstract = content.Abstract,
                Description = content.Description,
                Text = content.Text,
                Subject = content.Subject,
                Title = content.Title,
            };
            return firstOrDefault;
        }
        
        public static bool HaveChild(this Menu menu)
        {
            return new MenuFacade().Any(menu1 => menu1.ParentId == menu.Id);
        }
        public static IEnumerable<Menu> Childs(this Menu menu)
        {
            return new MenuFacade().Where(menu1 => menu1.ParentId == menu.Id);
        }
    }
}
