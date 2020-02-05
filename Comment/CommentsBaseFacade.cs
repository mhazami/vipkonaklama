using System.Configuration;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Comments
{
    public abstract class CommentsBaseFacade<T> : BaseFacade<T> where T : class
    {

        protected CommentsBaseFacade()
            : base(new ContentManagerConnectionHandler(), false)
        {

        }
        protected CommentsBaseFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {

        }


      

    }
    public class ContentManagerConnectionHandler : ConnectionHandler
    {
        public ContentManagerConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString; 
        }

    }
}
