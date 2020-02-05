using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;

namespace Radyn.News.DA
{
    public class NewsCommentDA : DALBase<NewsComment>
    {
        public NewsCommentDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        {
        }
    }
}
