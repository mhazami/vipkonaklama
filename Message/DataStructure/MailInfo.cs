using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Utility;

namespace Radyn.Message.DataStructure
{
    [Serializable]
    [Schema("Message")]
    public sealed class MailInfo : DataStructureBase<MailInfo>
    {
        public MailInfo()
        {
            Attachments = new List<MailAttachment>();
        }
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _subject;
        [IsNullable]
        [DbType("nvarchar(250)")]
        public string Subject
        {
            get { return _subject; }
            set { base.SetPropertyValue("Subject", value); }
        }

        private DateTime _date;
        [DbType("datetime")]
        public DateTime Date
        {
            get { return _date; }
            set { base.SetPropertyValue("Date", value); }
        }

        private bool _isDraft;
        [DbType("bit")]
        public bool IsDraft
        {
            get { return _isDraft; }
            set { base.SetPropertyValue("IsDraft", value); }
        }

        private Guid _fromId;
        [DbType("uniqueidentifier")]
        public Guid FromId
        {
            get { return _fromId; }
            set { base.SetPropertyValue("FromId", value); }
        }

        private string _to;
        [DbType("varchar(4000)")]
        public string To
        {
            get { return _to; }
            set { base.SetPropertyValue("To", value); }
        }

        private string _bcc;
        [IsNullable]
        [DbType("varchar(4000)")]
        public string Bcc
        {
            get { return _bcc; }
            set { base.SetPropertyValue("Bcc", value); }
        }

        private string _cc;
        [IsNullable]
        [DbType("varchar(4000)")]
        public string Cc
        {
            get { return _cc; }
            set { base.SetPropertyValue("Cc", value); }
        }

        private string _body;
        [IsNullable]
        [DbType("ntext")]
        public string Body
        {
            get { return _body; }
            set { base.SetPropertyValue("Body", value); }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string ToNames { get; set; }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string BccNames { get; set; }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string CcNames { get; set; }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string EmailDateTime
        {
            get
            {
                if (Date == DateTime.MinValue) return string.Empty;
                return Date.Date == DateTime.Now.Date ? Date.GetTime() : Date.ShamsiDate() + "-" + Date.GetTime();
            }
        }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public List<MailAttachment> Attachments { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Subject; }
        }
    }
}
