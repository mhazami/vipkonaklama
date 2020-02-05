using System;
using Radyn.Framework;

namespace Radyn.Statistics.DataStructure
{
    [Serializable]
    [Schema("Statistics")]
    public sealed class WebSite : DataStructureBase<WebSite>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _url;
        [DbType("nvarchar(200)")]
        public string Url
        {
            get { return _url; }
            set { base.SetPropertyValue("Url", value); }
        }

        private Guid _ownerId;
        [DbType("uniqueidentifier")]
        [IsNullable]
        public Guid OwnerId
        {
            get
            {
                return this._ownerId;
            }
            set
            {
                base.SetPropertyValue("OwnerId", value);
                if (EnterpriseNode == null)
                {
                    this.EnterpriseNode =new EnterpriseNode.DataStructure.EnterpriseNode { Id = value };
                }

            }
        }

        [Assosiation(PropName = "OwnerId")]
        public EnterpriseNode.DataStructure.EnterpriseNode EnterpriseNode { get; set; }

        private string _registerDate;
        [DbType("nchar(10)")]
        public string RegisterDate
        {
            get { return _registerDate; }
            set { base.SetPropertyValue("RegisterDate", value); }
        }

        private string _title;
        [IsNullable]
        [DbType("nvarchar(50)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }



        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
