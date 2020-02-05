using System.Configuration;
using Radyn.Common;
using Radyn.EnterpriseNode;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Article
{
    public abstract class ArticleBaseFacade<T> : BaseFacade<T> where T : class
    {

        protected ArticleBaseFacade()
            : base(new EnterpriseNodeConnectionHandler(), false)
        {

        }
        protected ArticleBaseFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {

        }

        public IConnectionHandler FileManagerConnection
        {
            get
            {
                var fileManagerConnection = new FileManagerConnectionHandler();
                return fileManagerConnection.Connection.DataSource == this.ConnectionHandler.Connection.DataSource
                     && fileManagerConnection.Connection.Database == this.ConnectionHandler.Connection.Database
                    ? this.ConnectionHandler
                    : fileManagerConnection;
            }
        }
        public IConnectionHandler CommonConnection
        {
            get
            {
                var fileManagerConnection = new CommonConnectionHandler();
                return fileManagerConnection.Connection.DataSource == this.ConnectionHandler.Connection.DataSource
                     && fileManagerConnection.Connection.Database == this.ConnectionHandler.Connection.Database
                    ? this.ConnectionHandler
                    : fileManagerConnection;
            }
        }

    }
    public class ArticleConnectionHandler : ConnectionHandler
    {
        public ArticleConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString;
        }

    }
}
