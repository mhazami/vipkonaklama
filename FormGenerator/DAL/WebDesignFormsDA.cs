using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.FormGenerator.DataStructure;

namespace Radyn.FormGenerator.DAL
{
    public sealed class WebDesignFormsDA : DALBase<WebDesignForms>
    {
        public WebDesignFormsDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class FormsCommandBuilder
    {
    }
}
