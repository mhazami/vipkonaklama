using System.Collections.Generic;
using Radyn.EnterpriseNode.Tools;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.EnterpriseNode.DAL
{
    public sealed class EnterpriseNodeDA : DALBase<DataStructure.EnterpriseNode>
    {
        public EnterpriseNodeDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public List<DataStructure.EnterpriseNode> Search(DataStructure.EnterpriseNode filter)
        {
            var enterpriseNodeCommandBuilder = new EnterpriseNodeCommandBuilder();
            var query = enterpriseNodeCommandBuilder.Search(filter);
            return DBManager.GetCollection<DataStructure.EnterpriseNode>(base.ConnectionHandler, query);
        }

       

        public List<DataStructure.EnterpriseNode> Search(string filter, Enums.EnterpriseNodeType enterpriseNodeTypeId = Enums.EnterpriseNodeType.RealEnterPriseNode)
        {
            var enterpriseNodeCommandBuilder = new EnterpriseNodeCommandBuilder();
            var query = enterpriseNodeCommandBuilder.Search(filter, enterpriseNodeTypeId);
            return DBManager.GetCollection<DataStructure.EnterpriseNode>(base.ConnectionHandler, query);
        }
      
        
      
     
    }
    internal class EnterpriseNodeCommandBuilder
    {
        public string Search(DataStructure.EnterpriseNode obj)
        {

            var query = string.Empty;
            var whereCluase = "";
            if (obj.EnterpriseNodeTypeId == Enums.EnterpriseNodeType.RealEnterPriseNode || obj.RealEnterpriseNode != null)
            {

                query = string.Format("SELECT     EnterpriseNode.EnterpriseNode.* FROM         EnterpriseNode.EnterpriseNode INNER JOIN " +
                      " EnterpriseNode.RealEnterpriseNode ON EnterpriseNode.EnterpriseNode.Id = EnterpriseNode.RealEnterpriseNode.Id ");

                if (!string.IsNullOrEmpty(obj.RealEnterpriseNode.FirstName))
                    whereCluase += string.Format("EnterpriseNode.RealEnterpriseNode.FirstName LIKE N'%{0}%' OR ", obj.RealEnterpriseNode.FirstName);
                if (!string.IsNullOrEmpty(obj.RealEnterpriseNode.LastName))
                    whereCluase += string.Format("EnterpriseNode.RealEnterpriseNode.LastName LIKE N'%{0}%' OR ", obj.RealEnterpriseNode.LastName);
                if (!string.IsNullOrEmpty(obj.RealEnterpriseNode.NationalCode))
                    whereCluase += string.Format("EnterpriseNode.RealEnterpriseNode.NationalCode LIKE '%{0}%' OR ", obj.RealEnterpriseNode.NationalCode);
                if (!string.IsNullOrEmpty(obj.RealEnterpriseNode.IDNumber))
                    whereCluase += string.Format("EnterpriseNode.RealEnterpriseNode.IDNumber LIKE '%{0}%' OR ", obj.RealEnterpriseNode.IDNumber);

            }
            if (obj.EnterpriseNodeTypeId == Enums.EnterpriseNodeType.LegalEnterPriseNode || obj.LegalEnterpriseNode != null)
            {
                query = string.Format("SELECT     EnterpriseNode.EnterpriseNode.* FROM         EnterpriseNode.EnterpriseNode INNER JOIN " +
                      " EnterpriseNode.LegalEnterpriseNode ON EnterpriseNode.EnterpriseNode.Id = EnterpriseNode.LegalEnterpriseNode.Id ");
                if (!string.IsNullOrEmpty(obj.LegalEnterpriseNode.Title))
                    whereCluase += string.Format("EnterpriseNode.LegalEnterpriseNode.Title LIKE N'%{0}%' OR ", obj.LegalEnterpriseNode.Title);
                if (!string.IsNullOrEmpty(obj.LegalEnterpriseNode.RegisterNo))
                    whereCluase += string.Format("EnterpriseNode.LegalEnterpriseNode.RegisterNo LIKE '%{0}%' OR ", obj.LegalEnterpriseNode.RegisterNo);
                if (!string.IsNullOrEmpty(obj.LegalEnterpriseNode.NationalId))
                    whereCluase += string.Format("EnterpriseNode.LegalEnterpriseNode.NationalId LIKE '%{0}%' OR ", obj.LegalEnterpriseNode.NationalId);

            }
            if (!string.IsNullOrEmpty(obj.Address))
                whereCluase += string.Format("EnterpriseNode.[Address] LIKE '%{0}%' OR ", obj.Address);
            if (!string.IsNullOrEmpty(obj.Website))
                whereCluase += string.Format("EnterpriseNode.Website LIKE '%{0}%' OR ", obj.Website);
            if (!string.IsNullOrEmpty(obj.Cellphone))
                whereCluase += string.Format("EnterpriseNode.Cellphone LIKE '%{0}%' OR ", obj.Cellphone);
            if (!string.IsNullOrEmpty(obj.Tel))
                whereCluase += string.Format("EnterpriseNode.Tel LIKE '%{0}%' OR ", obj.Tel);

            if (!string.IsNullOrEmpty(whereCluase))
            {
                whereCluase = whereCluase.Substring(0, whereCluase.Length - 3);
                query = string.Format("{0} WHERE {1}  ", query, whereCluase);
            }
            return query;

        }

      

        public string Search(string filter, Enums.EnterpriseNodeType enterpriseNodeTypeId = Enums.EnterpriseNodeType.RealEnterPriseNode)
        {
            var query = string.Empty;
            var whereCluase = "";
            if (!string.IsNullOrEmpty(filter))
                filter = filter.ToLower();
            if (enterpriseNodeTypeId == Enums.EnterpriseNodeType.RealEnterPriseNode)
            {

                query = string.Format("SELECT     EnterpriseNode.EnterpriseNode.* FROM         EnterpriseNode.EnterpriseNode INNER JOIN " +
                      " EnterpriseNode.RealEnterpriseNode ON EnterpriseNode.EnterpriseNode.Id = EnterpriseNode.RealEnterpriseNode.Id ");


                whereCluase += string.Format("lower(EnterpriseNode.RealEnterpriseNode.FirstName) LIKE N'%{0}%' OR ", filter);
                whereCluase += string.Format("lower(EnterpriseNode.RealEnterpriseNode.LastName) LIKE N'%{0}%' OR ", filter);
                whereCluase += string.Format("lower(EnterpriseNode.RealEnterpriseNode.NationalCode) LIKE '%{0}%' OR ", filter);
                whereCluase += string.Format("lower(EnterpriseNode.RealEnterpriseNode.IDNumber) LIKE '%{0}%' OR ", filter);

            }
            if (enterpriseNodeTypeId == Enums.EnterpriseNodeType.LegalEnterPriseNode)
            {
                query = string.Format("SELECT     EnterpriseNode.EnterpriseNode.* FROM         EnterpriseNode.EnterpriseNode INNER JOIN " +
                      " EnterpriseNode.LegalEnterpriseNode ON EnterpriseNode.EnterpriseNode.Id = EnterpriseNode.LegalEnterpriseNode.Id ");

                whereCluase += string.Format("lower(EnterpriseNode.LegalEnterpriseNode.Title) LIKE N'%{0}%' OR ", filter);
                whereCluase += string.Format("lower(EnterpriseNode.LegalEnterpriseNode.RegisterNo) LIKE '%{0}%' OR ", filter);
                whereCluase += string.Format("lower(EnterpriseNode.LegalEnterpriseNode.NationalId) LIKE '%{0}%' OR ", filter);

            }

            whereCluase += string.Format("lower(EnterpriseNode.[Address]) LIKE '%{0}%' OR ", filter);
            whereCluase += string.Format("lower(EnterpriseNode.Website) LIKE '%{0}%' OR ", filter);
            whereCluase += string.Format("lower(EnterpriseNode.Cellphone) LIKE '%{0}%' OR ", filter);
            whereCluase += string.Format("lower(EnterpriseNode.Tel) LIKE '%{0}%' OR ", filter);
            whereCluase += string.Format("lower(EnterpriseNode.Email) LIKE '%{0}%' OR ", filter);

            if (!string.IsNullOrEmpty(whereCluase))
            {
                whereCluase = whereCluase.Substring(0, whereCluase.Length - 3);
                query = string.Format("{0} WHERE {1}  ", query, whereCluase);
            }
            return query;

        }

      

        
        
      
    }
}
