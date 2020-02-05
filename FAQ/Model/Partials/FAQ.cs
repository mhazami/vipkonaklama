using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radyn.FAQ.Model
{
    public partial class FAQ
    {


        public FAQContent Content(string culture)
        {
            var content = this.FAQContents.FirstOrDefault(x => x.LanguageId.Equals(culture));
            if (content != null) return content;

            content = new FAQContent
            {
                Id = this.Id,
                Answer = this.Answer,
                Question = this.Question,
               
            };
            return content;
        }
    }
}
