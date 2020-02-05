using System.Configuration;
using Radyn.Common;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Graphic
{
    public abstract class GraphicBaseFacade<T> :BaseFacade<T> where T : class
    {

        protected GraphicBaseFacade()
            : base(new GraphicConnectionHandler(), false)
        {

        }
        protected GraphicBaseFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {

        }
        public IConnectionHandler CommonConnection
        {
            get
            {
                var commonConnectionHandler = new CommonConnectionHandler();
                return commonConnectionHandler.Connection.DataSource == this.ConnectionHandler.Connection.DataSource
                     && commonConnectionHandler.Connection.Database == this.ConnectionHandler.Connection.Database
                    ? this.ConnectionHandler
                    : commonConnectionHandler;
            }
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
    public class GraphicConnectionHandler : ConnectionHandler
    {
        public GraphicConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString; 
        }

    }
     
}
