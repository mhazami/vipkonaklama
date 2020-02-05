using Radyn.FormGenerator.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FormGenerator.DAL
{
    public sealed class FormAssigmentDA : DALBase<FormAssigment>
    {
        public FormAssigmentDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }


        public FormAssigment GetByUrl(string url)
        {
            var commandBuilder = new FormAssigmentCommandBuilder();
            var query = commandBuilder.GetByUrl(url);
            return DBManager.GetObject<FormAssigment>(base.ConnectionHandler, query);
        }

        
    }
    internal class FormAssigmentCommandBuilder
    {
        public string GetByUrl(string url)
        {
            return string.Format("select top(1)* from [FormGenerator].[FormAssigment] where [Url]=N'{0}'", url.ToLower());
        }

       
    }
}
