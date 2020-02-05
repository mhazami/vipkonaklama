using System.Configuration;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements
{
    public abstract class AdvertisementsBaseFacade<T> : BaseFacade<T> where T : class
    {

        protected AdvertisementsBaseFacade()
            : base(new AdvertisementsConnectionHandler(), false)
        {

        }
        protected AdvertisementsBaseFacade(IConnectionHandler connectionHandler)
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
    public class AdvertisementsConnectionHandler : ConnectionHandler
    {
        public AdvertisementsConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString;
        }

    }
}
