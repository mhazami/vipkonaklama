using Radyn.Article.Facade;
using Radyn.Article.Facade.Interface;

namespace Radyn.Article
{
    public sealed class ArticleComponent
    {
        private ArticleComponent()
        {

        }

        private static ArticleComponent _instance;
        public static ArticleComponent Instance
        {
            get { return _instance ?? (_instance = new ArticleComponent()); }
        }


        public IArticleFacade ArticleFacade
        {
            get
            {
                return new ArticleFacade();
            }
        }


        public IArticleCategoryFacade ArticleCategoryFacade
        {
            get
            {
                return new ArticleCategoryFacade();
            }
        }

        

        public IArticleResourceFacade ArticleResourceFacade
        {
            get
            {
                return new ArticleResourceFacade();
            }
        }

        public IArticleResourcesFacade ArticleResourcesFacade
        {
            get
            {
                return new ArticleResourcesFacade();
            }
        }
      
   
        public IArticleFileAttachmentFacade ArticleFileAttachmentFacade
        {
            get
            {
                return new ArticleFileAttachmentFacade();
            }
        }

        public IArticleCommentFacade ArticleCommentFacade
        {
            get
            {
                return new ArticleCommentFacade();
            }
        }
    }

}
