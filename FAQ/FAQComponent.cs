using Radyn.FAQ.Facade;
using Radyn.FAQ.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.FAQ
{
    public class FAQComponent
    {
        private FAQComponent()
        {

        }
        public static FAQComponent Instance
        {
            get { return new FAQComponent(); }
        }
        public IFAQFacade FAQTransactionalFacade(IConnectionHandler connectionHandler)
        {
            return new FAQFacade(connectionHandler); 
        }
        public IFAQFacade FAQFacade
        {
            get { return new FAQFacade(); }
        }
        public IFAQContentFacade FaqContentFacade
        {
            get { return new FAQContentFacade(); }
        }
        public IWebDesignFAQFacade WebDesignFAQFacade
        {
            get { return new WebDesignFAQFacade(); }
        }
    }
}
