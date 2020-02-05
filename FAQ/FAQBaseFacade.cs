using System.Configuration;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FAQ
{
    public abstract class FAQBaseFacade<T> : BaseFacade<T> where T : class
    {
        
        protected FAQBaseFacade()
            : base(new FAQConnectionHandler(), false)
        {

        }
        protected FAQBaseFacade(IConnectionHandler connectionHandler)
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
    public class FAQConnectionHandler : ConnectionHandler
    {
        public FAQConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString; 
        }

    }
}
