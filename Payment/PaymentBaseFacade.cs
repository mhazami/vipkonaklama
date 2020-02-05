using System.Configuration;
using Radyn.Common;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Payment
{
    public abstract class PaymentBaseFacade<T> : BaseFacade<T> where T : class
    {

        protected PaymentBaseFacade()
            : base(new PaymentConnectionHandler(), false)
        {

        }
        protected PaymentBaseFacade(IConnectionHandler connectionHandler)
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
                var commonConnectionHandler = new CommonConnectionHandler();
                return commonConnectionHandler.Connection.DataSource == this.ConnectionHandler.Connection.DataSource
                     && commonConnectionHandler.Connection.Database == this.ConnectionHandler.Connection.Database
                    ? this.ConnectionHandler
                    : commonConnectionHandler;
            }
        }
      
    }
    public class PaymentConnectionHandler : ConnectionHandler
    {
        public PaymentConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString;
        }
    }


}
