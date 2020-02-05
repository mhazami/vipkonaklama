using Radyn.Framework.DbHelper;
using Radyn.News.Facade;
using Radyn.News.Facade.Interface;

namespace Radyn.News
{
    public class NewsComponent
    {
        private NewsComponent()
        {

        }


        public static NewsComponent Instance
        {
            get { return new NewsComponent(); }
        }

        public INewsFacade NewsFacade
        {
            get { return new NewsFacade(); }
        }
        public INewsFacade NewsTransactionalFacade(IConnectionHandler connectionHandler)
        {
           return new NewsFacade(connectionHandler); 
        }
        public INewsCategoryFacade NewsCategoryFacade
        {
            get { return new NewsCategoryFacade(); }
        }
        public INewsContentTypeFacade NewsContentTypeFacade
        {
            get { return new NewsContentTypeFacade(); }
        }
        public INewsContentFacade NewsContentFacade
        {
            get { return new NewsContentFacade(); }
        }
        public INewsPropertyFacade NewsPropertyFacade
        {
            get { return new NewsPropertyFacade(); }
        }
        public IPublishTypeFacade PublishTypeFacade
        {
            get { return new PublishTypeFacade(); }
        }
        public INewsAttachedFileFacade AttachedFileFacade
        {
            get { return new NewsAttachedFileFacade(); }
        }

        public INewsCommentFacade NewsCommentFacade
        {
            get { return new NewsCommentFacade(); }
        }
        public IWebDesignNewsFacade WebDesignNewsFacade
        {
            get { return new WebDesignNewsFacade(); }
        }
    }
}
