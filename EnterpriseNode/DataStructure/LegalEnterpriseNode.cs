using System;
using Radyn.Framework;

namespace Radyn.EnterpriseNode.DataStructure
{
    [Serializable]
    [Schema("EnterpriseNode")]
    public sealed class LegalEnterpriseNode : DataStructureBase<LegalEnterpriseNode>
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
                    this.EnterpriseNode = new EnterpriseNode { Id = value };
            }
        }

        [Assosiation]
        public EnterpriseNode EnterpriseNode { get; set; }

        private string _title;
        [DbType("nvarchar(550)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private string _createDate;
        [IsNullable]
        [DbType("char(10)")]
        public string CreateDate
        {
            get { return _createDate; }
            set { base.SetPropertyValue("CreateDate", value); }
        }

        private string _nationalId;
        [Layout(Caption = "شناسه ملی")]
        [IsNullable]
        [DbType("nchar(10)")]
        public string NationalId
        {
            get { return _nationalId; }
            set { base.SetPropertyValue("NationalId", value); }
        }

        private string _economicCode;
        [Layout(Caption = "کد اقتصادی")]
        [IsNullable]
        [DbType("varchar(50)")]
        public string EconomicCode
        {
            get { return _economicCode; }
            set { base.SetPropertyValue("EconomicCode", value); }
        }

        private string _registerNo;
        [Layout(Caption = "شماره ثبت")]
        [IsNullable]
        [DbType("nvarchar(10)")]
        public string RegisterNo
        {
            get { return _registerNo; }
            set { base.SetPropertyValue("RegisterNo", value); }
        }

        private string _activityDescription;
        [IsNullable]
        [DbType("nvarchar(max)")]
        public string ActivityDescription
        {
            get { return _activityDescription; }
            set { base.SetPropertyValue("ActivityDescription", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
