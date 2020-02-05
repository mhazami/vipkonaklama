using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Article.DA
{
    public class ArticleCommentDA: DALBase<DataStructure.ArticleComment>
    {
        public ArticleCommentDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
}
