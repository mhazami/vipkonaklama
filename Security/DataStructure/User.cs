using System;
using System.Collections.Generic;
using Radyn.EnterpriseNode;
using Radyn.Framework;

namespace Radyn.Security.DataStructure
{
    [Serializable]
    [Schema("Security")]
    public sealed class User : DataStructureBase<User>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get
            {
                return this._id;
            }
            set
            {
                base.SetPropertyValue("Id", value);
               
            }
        }

        [Assosiation(PropName = "Id")]
        public EnterpriseNode.DataStructure.EnterpriseNode EnterpriseNode { get; set; }

        private string _username;
        [DbType("varchar(20)")]
        [Filter(SqlCompareOperator.Equal)]
        [Layout(Caption = "نام کاربری")]
        public string Username
        {
            get { return _username; }
            set { base.SetPropertyValue("Username", value); }
        }

        private string _password;
        [DbType("varchar(50)")]
        [Layout(Caption = "رمز عبور")]
        [Filter(SqlCompareOperator.Equal)]
        public string Password
        {
            get { return _password; }
            set { base.SetPropertyValue("Password", value); }
        }

        
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public List<KeyValuePair<string, string>> KeyValuePairs { get; set; }

        

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.EnterpriseNode.DescriptionField; }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public EnterpriseNode.DataStructure.EnterpriseNode Enterprise
        {
            get
            {

                return EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get(this.Id);

            }
        }
    }
}
