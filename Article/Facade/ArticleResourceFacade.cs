using System;
using System.Collections.Generic;
using System.Data;
using Radyn.Article.BO;
using Radyn.Article.DataStructure;
using Radyn.Article.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Article.Facade
{
    internal sealed class ArticleResourceFacade : ArticleBaseFacade<ArticleResource>, IArticleResourceFacade
    {
        internal ArticleResourceFacade()
        {
        }

        internal ArticleResourceFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

       

        public IEnumerable<ArticleResource> GetAllByArticleId(Guid aId)
        {
            try
            {
                var list = new ArticleResourceBO().GetAllByArticleId(this.ConnectionHandler, aId);
               return list;
            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var obj = new ArticleResourceBO().Get(this.ConnectionHandler, keys);
                var articleResourcesBO = new ArticleResourcesBO();
                var articleResourceses = articleResourcesBO.Where(this.ConnectionHandler,
                    resources => resources.ArticleResourceId == obj.Id);
                foreach (var articleResourcese in articleResourceses)
                {
                    if (
                        !articleResourcesBO.Delete(this.ConnectionHandler, articleResourcese.ArticleId,
                            articleResourcese.ArticleResourceId))
                        throw new Exception("خطایی در حذف منابع وجود دارد");
                }
                if (!new ArticleResourceBO().Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف منابع وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }


    }
}
