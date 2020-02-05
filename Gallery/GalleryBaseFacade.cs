using System.Configuration;
using Radyn.Common;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Gallery
{
    public abstract class GalleryBaseFacade<T> : BaseFacade<T> where T : class
    {

        protected GalleryBaseFacade()
            : base(new FileManagerConnectionHandler(), false)
        {

        }
        protected GalleryBaseFacade(IConnectionHandler connectionHandler)
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
    public class GalleryConnectionHandler : ConnectionHandler
    {
        public GalleryConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString; 
        }
    }


}
