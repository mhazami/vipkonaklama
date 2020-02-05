using System.Configuration;
using Radyn.Common;
using Radyn.EnterpriseNode;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Reservation
{
    public abstract class ReservationBaseFacade<T> : BaseFacade<T> where T : class
    {
        protected ReservationBaseFacade()
            : base(new ReservationBaseFacadeConnectionHandler(), false)
        {

        }

        protected ReservationBaseFacade(IConnectionHandler connectionHandler)
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

    public class ReservationBaseFacadeConnectionHandler : ConnectionHandler
    {

        public ReservationBaseFacadeConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString;
        }

    }
}
