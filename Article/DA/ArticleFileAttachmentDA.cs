using Radyn.Article.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Article.DA
{
    public sealed class ArticleFileAttachmentDA : DALBase<ArticleFileAttachment>
    {
        public ArticleFileAttachmentDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class ArticleFileAttachmentCommandBuilder
    {
    }
}
