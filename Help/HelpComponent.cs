using Radyn.Help.Facade;
using Radyn.Help.Facade.Interface;

namespace Radyn.Help
{
    public class HelpComponent
    {

        private static HelpComponent _instance;
        public static HelpComponent Instance
        {
            get { return _instance ?? (_instance = new HelpComponent()); }
        }
        public IHelpMenuFacade HelpMenuFacade
        {
            get
            {
                return new HelpMenuFacade();
            }
        }

        public IHelpFacade HelpFacade
        {
            get
            {
                return new HelpFacade();
            }
        }

        public IHelpContentFacade HelpContentFacade
        {
            get
            {
                return new HelpContentFacade();
            }
        }

        public IHelpReationCollectionFacade HelpReationCollectionFacade 
        {
            get
            {
                return new HelpReationCollectionFacade();
            }
        }
        public IHelpRelationFacade HelpRelationFacade
        {
            get
            {
                return new HelpRelationFacade();
            }
        }

    }
}
