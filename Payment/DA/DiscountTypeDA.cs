using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.DataStructure;

namespace Radyn.Payment.DA
{
    public sealed class DiscountTypeDA : DALBase<DiscountType>
    {
        public DiscountTypeDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public IEnumerable<DiscountType> GetDiscountTypes(string modualName, byte section)
        {
            var commandBuilder = new DiscountTypeCommandBuilder();
            var query = commandBuilder.GetDiscountTypes(modualName, section);
            return DBManager.GetCollection<DiscountType>(base.ConnectionHandler, query);
        }

       
    }
    internal class DiscountTypeCommandBuilder
    {
        public string GetDiscountTypes(string modualName, byte section)
        {
            return string.Format("SELECT     Payment.DiscountType.*" +
                                 " FROM         Payment.DiscountType INNER JOIN " +
                                 " Payment.DiscountTypeSection ON Payment.DiscountType.Id = Payment.DiscountTypeSection.DiscountTypeId" +
                                 " WHERE Payment.DiscountTypeSection.MoudelName='{0}' AND Payment.DiscountTypeSection.Section={1} ",
                modualName, section);
        }

        
    }
}
