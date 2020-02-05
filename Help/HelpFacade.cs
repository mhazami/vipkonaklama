using System.Configuration;
using Radyn.Common;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Help
{
    public abstract class HelpFacade<T> : BaseFacade<T> where T : class
    {

        protected HelpFacade()
            : base(new HelpConnectionHandler(), false)
        {

        }
        protected HelpFacade(IConnectionHandler connectionHandler)
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
    public class HelpConnectionHandler : ConnectionHandler
    {
        public HelpConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString; 
        }

    }
}
