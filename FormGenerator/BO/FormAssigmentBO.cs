using Radyn.FormGenerator.DAL;
using Radyn.FormGenerator.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FormGenerator.BO
{
    internal class FormAssigmentBO : BusinessBase<FormAssigment>
    {


        public override bool Insert(IConnectionHandler connectionHandler, FormAssigment obj)
        {
            obj.Url = obj.Url.ToLower();
            return base.Insert(connectionHandler, obj);
        }


        public FormAssigment GetByUrl(IConnectionHandler connectionHandler, string url)
        {
            var da = new FormAssigmentDA(connectionHandler);
            return da.GetByUrl(url);
        }
     

    }
}
