using System;
using Radyn.Framework;

namespace Radyn.Message.DataStructure
{
    [Serializable]
    [Schema("Message")]
    public sealed class MailBox : DataStructureBase<MailBox>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private Guid _mailId;
        [DbType("uniqueidentifier")]
        public Guid MailId
        {
            get
            {
                return this._mailId;
            }
            set
            {
                base.SetPropertyValue("MailId", value);
                if (MailInfo == null)
                    this.MailInfo = new MailInfo { Id = value };
            }
        }

        [Assosiation(PropName = "MailId")]
        public MailInfo MailInfo { get; set; }

        private Guid _fromId;
        [DbType("uniqueidentifier")]
        public Guid FromId
        {
            get
            {
                return this._fromId;
            }
            set
            {
                base.SetPropertyValue("FromId", value);
                if (FromEnterpriseNode == null)
                    this.FromEnterpriseNode = new EnterpriseNode.DataStructure.EnterpriseNode { Id = value };
            }
        }

        [Assosiation(PropName = "FromId")]
        public EnterpriseNode.DataStructure.EnterpriseNode FromEnterpriseNode { get; set; }

        private Guid _toId;
        [DbType("uniqueidentifier")]
        public Guid ToId
        {
            get
            {
                return this._toId;
            }
            set
            {
                base.SetPropertyValue("ToId", value);
                if (ToEnterpriseNode == null)
                    this.ToEnterpriseNode = new EnterpriseNode.DataStructure.EnterpriseNode() { Id = value };
            }
        }

        [Assosiation(PropName = "ToId")]
        public EnterpriseNode.DataStructure.EnterpriseNode ToEnterpriseNode { get; set; }

        private bool _deleted;
        [DbType("bit")]
        public bool Deleted
        {
            get { return _deleted; }
            set { base.SetPropertyValue("Deleted", value); }
        }

        private bool _read;
        [DbType("bit")]
        public bool Read
        {
            get { return _read; }
            set { base.SetPropertyValue("Read", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.MailInfo.Subject; }
        }
    }
}
