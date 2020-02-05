using Radyn.FAQ.DataStructure;
using Radyn.FAQ.Facade;

namespace Radyn.FAQ.Tools
{
   public static class Extentions
    {
       public  static FAQContent Content(this FAQ.DataStructure.FAQ faq,string culture)
       {
           var content = new FAQContentFacade().Get(faq.Id, culture);
           if (content != null)
           {
               if (content.Question != null)
                   content.Question = content.Question.Replace("\r\n", "<br />");
               return content;
           }
           content = new FAQContent
           {
               Id = faq.Id,
               Answer = faq.Answer,
               Question = faq.Question,
           };
           return content;
       }
    }
}
