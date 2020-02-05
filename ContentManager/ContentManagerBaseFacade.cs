using System.Configuration;
using Radyn.Common;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager
{
    public abstract class ContentManagerBaseFacade<T> : BaseFacade<T> where T : class
    {

        protected ContentManagerBaseFacade()
            : base(new ContentManagerConnectionHandler(), false)
        {

        }
        protected ContentManagerBaseFacade(IConnectionHandler connectionHandler)
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
                var commonConnection = new CommonConnectionHandler();
                return commonConnection.Connection.DataSource == this.ConnectionHandler.Connection.DataSource
                     && commonConnection.Connection.Database == this.ConnectionHandler.Connection.Database
                    ? this.ConnectionHandler
                    : commonConnection;
            }
        }

    }
    public class ContentManagerConnectionHandler : ConnectionHandler
    {
        public ContentManagerConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString; 
        }

    }
}
