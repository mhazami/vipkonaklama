using System.Configuration;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FileManager
{
    public abstract class FileManageBaseFacade<T> : BaseFacade<T> where T : class
    {

        protected FileManageBaseFacade()
            : base(new FileManagerConnectionHandler(), false)
        {

        }
        protected FileManageBaseFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {

        }
    }
    public class FileManagerConnectionHandler : ConnectionHandler
    {
        public FileManagerConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString;
        }
    }


}
