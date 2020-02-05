using System.Configuration;
using Radyn.EnterpriseNode;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.PhoneBook
{
    public abstract class PhoneBookBaseFacade<T> : BaseFacade<T> where T : class
    {
        
        protected PhoneBookBaseFacade(): base(new PhoneBookConnectionHandler(), false)
        {

        }
        protected PhoneBookBaseFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {

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
      
    }
    public class PhoneBookConnectionHandler : ConnectionHandler
    {
        public PhoneBookConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString;
        }

    }
}
