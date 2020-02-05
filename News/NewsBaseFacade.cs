using System.Configuration;
using Radyn.Common;
using Radyn.EnterpriseNode;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.News
{
    public abstract class NewsBaseFacade<T> : BaseFacade<T> where T : class
    {
        protected NewsBaseFacade()
            : base(new NewsConnectionHandler(), false)
        {

        }

        protected NewsBaseFacade(IConnectionHandler connectionHandler)
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

        public IConnectionHandler EnterpriseNodeConnection
        {
            get
            {
                var enterpriseNodeConnection = new EnterpriseNodeConnectionHandler();
                return enterpriseNodeConnection.Connection.DataSource == this.ConnectionHandler.Connection.DataSource
                     && enterpriseNodeConnection.Connection.Database == this.ConnectionHandler.Connection.Database
                    ? this.ConnectionHandler
                    : enterpriseNodeConnection;
            }
        }
        public IConnectionHandler CommonConnection
        {
            get
            {
                var contentManagerConnection = new CommonConnectionHandler();
                return contentManagerConnection.Connection.DataSource == this.ConnectionHandler.Connection.DataSource
                     && contentManagerConnection.Connection.Database == this.ConnectionHandler.Connection.Database
                    ? this.ConnectionHandler
                    : contentManagerConnection;
            }
        }

    }

    public class NewsConnectionHandler : ConnectionHandler
    {

        public NewsConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString; 
        }

    }
}
