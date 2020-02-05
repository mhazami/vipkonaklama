using System.Configuration;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Common
{
    public abstract class CommonBase<T> :BaseFacade<T> where T : class
    {

        protected CommonBase()
            : base(new CommonConnectionHandler(), false)
        {

        }
        protected CommonBase(IConnectionHandler connectionHandler)
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

    }
    public class CommonConnectionHandler : ConnectionHandler
    {
        public CommonConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString;
        }

    }
     
}
