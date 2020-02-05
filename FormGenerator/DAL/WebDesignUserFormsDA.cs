using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.FormGenerator.DataStructure;

namespace Radyn.FormGenerator.DAL
{
  public sealed class WebDesignUserFormsDA : DALBase<WebDesignUserForms>
    {
        public WebDesignUserFormsDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        {
        }
        internal class UserFormsCommandBuilder
        {
        }
    }
}
