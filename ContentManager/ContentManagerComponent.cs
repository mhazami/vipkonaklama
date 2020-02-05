using Radyn.ContentManager.Facade;
using Radyn.ContentManager.Facade.Interface;
using Radyn.Framework.DbHelper;


namespace Radyn.ContentManager
{
    public class ContentManagerComponent
    {

        public ContentManagerComponent()
        {

        }
        private static ContentManagerComponent _instance;
        public static ContentManagerComponent Instance
        {
            get { return _instance ?? (_instance = new ContentManagerComponent()); }
        }
        public IMenuFacade MenuFacade
        {
            get
            {
                return new MenuFacade();
            }
        }
        public IWebDesignMenuFacade WebDesignMenuFacade
        {
            get { return new WebDesignMenuFacade(); }
        }
        public IDefaultHtmlDesginFacade DefaultHtmlDesginFacade
        {
            get { return new DefaultHtmlDesginFacade(); }
        }
        public IWebDesignMenuHtmlFacade WebDesignMenuHtmlFacade
        {
            get { return new WebDesignMenuHtmlFacade(); }
        }
        public IWebDesignHtmlFacade WebDesignHtmlFacade
        {
            get { return new WebDesignHtmlFacade(); }
        }

        public IWebDesignContainerFacade WebDesignContainerFacade
        {
            get { return new WebDesignContainerFacade(); }
        }
        public IWebDesignContentFacade WebDesignContentFacade
        {
            get { return new WebDesignContentFacade(); }
        }
        public IMenuFacade MenuTransactionalFacade(IConnectionHandler connectionHandler)
        {
            return new MenuFacade(connectionHandler);
        }

        public ISectionFacade SectionFacade
        {
            get
            {
                return new SectionFacade();
            }
        }
        public IContentFacade ContentFacade
        {
            get
            {
                return new ContentFacade();
            }
        }
        public IContentFacade ContentTransactionalFacade(IConnectionHandler connectionHandler)
        {

            return new ContentFacade(connectionHandler);

        }
        public IPartialLoadFacade PartialLoadFacade
        {
            get
            {
                return new PartialLoadFacade();
            }
        }
        public IPartialsFacade PartialsFacade
        {
            get
            {
                return new PartialsFacade();
            }
        }
        public IPartialsFacade PartialsTransactionalFacade(IConnectionHandler connectionHandler)
        {

            return new PartialsFacade(connectionHandler);

        }
        public IHtmlDesginFacade HtmlDesginTransactinalFacade(IConnectionHandler connectionHandler)
        {

            return new HtmlDesginFacade(connectionHandler);

        }
        public IMenuHtmlFacade MenuHtmlTransactinalFacade(IConnectionHandler connectionHandler)
        {

            return new MenuHtmlFacade(connectionHandler);

        }
        public IHtmlDesginFacade HtmlDesginFacade
        {
            get
            {
                return new HtmlDesginFacade();
            }
        } public IMenuHtmlFacade MenuHtmlFacade
        {
            get
            {
                return new MenuHtmlFacade();
            }
        }
        public IContentContentFacade ContentContentFacade
        {
            get
            {
                return new ContentContentFacade();
            }
        }
        public IContainerFacade ContainerFacade
        {
            get
            {
                return new ContainerFacade();
            }
        }

        public IPagesFacade PagesFacade
        {
            get
            {
                return new PagesFacade();
            }
        }
        public IWebDesignPagesFacade WebDesignPagesFacade
        {
            get
            {
                return new WebDesignPagesFacade();
            }
        }
        public IContainerFacade ContainerTransactionalFacade(IConnectionHandler connectionHandler)
        {
            return new ContainerFacade(connectionHandler);
        }
    }
}
