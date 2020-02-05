using System;
using Radyn.Framework;
using Radyn.WebDesign.Definition;

namespace Radyn.WebDesign.DataStructure
{
    [Serializable]
    [Schema("WebDesign")]
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
                if (EnterpriseNode == null)
                    this.EnterpriseNode = new EnterpriseNode.DataStructure.EnterpriseNode { Id = value };
            }
        }

        [Assosiation(PropName = "Id")]
        public EnterpriseNode.DataStructure.EnterpriseNode EnterpriseNode { get; set; }

        private Guid _webId;
        [DbType("uniqueidentifier")]
        public Guid WebId
        {
            get
            {
                return this._webId;
            }
            set
            {
                base.SetPropertyValue("WebId", value);
                this.WebSite = new WebSite { Id = value };
            }
        }

        [Assosiation]
        public WebSite WebSite { get; set; }

        private string _username;
        [DbType("nvarchar(100)")]
        public string Username
        {
            get { return _username; }
            set { base.SetPropertyValue("Username", value); }
        }

        private Int64 _number;
        [DbType("bigint")]
        [Identity(true)]
        public Int64 Number
        {
            get { return _number; }
            set { base.SetPropertyValue("Number", value); }
        }

        private string _password;
        [IsNullable]
        [DbType("varchar(200)")]
        public string Password
        {
            get { return _password; }
            set { base.SetPropertyValue("Password", value); }
        }

        private string _registerDate;
        [DbType("char(10)")]
        public string RegisterDate
        {
            get { return _registerDate; }
            set { base.SetPropertyValue("RegisterDate", value); }
        }

        private string _comment;
        [IsNullable]
        [DbType("nvarchar(1000)")]
        public string Comment
        {
            get { return _comment; }
            set { base.SetPropertyValue("Comment", value); }
        }


        private Enums.UserStatus _status;
        [DbType("tinyint")]
        public Enums.UserStatus Status
        {
            get { return _status; }
            set { base.SetPropertyValue("Status", value); }
        }

        private Guid? _parentId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? ParentId
        {
            get
            {
                return this._parentId;
            }
            set
            {
                base.SetPropertyValue("ParentId", value);
                if (Parent == null)
                    this.Parent = value.HasValue ? new User() { Id = value.Value } : null;

            }
        }
        [Assosiation(PropName = "ParentId")]
        public User Parent { get; set; }


       


      

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get
            {
                return
                  this.EnterpriseNode != null ? this.EnterpriseNode.DescriptionField : "";
            }
        }

      



    }
}
