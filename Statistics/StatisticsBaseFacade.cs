using System.Configuration;
using Radyn.EnterpriseNode;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Statistics
{

    public abstract class StatisticsBaseFacade<T> : BaseFacade<T> where T : class
    {
        protected StatisticsBaseFacade()
            : base(new StatisticConnectionHandler(), false)
        {

        }

        protected StatisticsBaseFacade(IConnectionHandler connectionHandler)
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
    }


    public class StatisticConnectionHandler : ConnectionHandler
    {

        public StatisticConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString; 
        }

    }


}
