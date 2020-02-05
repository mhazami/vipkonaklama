using Radyn.Help.DataStructure;
using Radyn.Help.Facade;

namespace Radyn.Help.Tools
{
    public static class Extentions
    {
        public static HelpContent GetContent(this Help.DataStructure.Help help, string culture)
        {
            var articleContent = new HelpContentFacade().Get(help.Id, culture);
            if (articleContent != null) return articleContent;
            articleContent = new HelpContent()
            {
                HelpId = help.Id,
                Title = help.DefaultTitle,
                Content = help.DefaultConent,
            };
            return articleContent;
        }
    }
}
