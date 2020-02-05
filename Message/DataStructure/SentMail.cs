using System;
using Radyn.Framework;

namespace Radyn.Message.DataStructure
{
    [Serializable]
    [Schema("Mail")]
    public sealed class SentMail : DataStructureBase<SentMail>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _sMTPserver;
        [DbType("char(50)")]
        public string SMTPserver
        {
            get { return _sMTPserver; }
            set { base.SetPropertyValue("SMTPserver", value); }
        }

        private string _subject;
        [DbType("nvarchar(100)")]
        public string Subject
        {
            get { return _subject; }
            set { base.SetPropertyValue("Subject", value); }
        }

        private string _cC;
        [IsNullable]
        [DbType("char(100)")]
        public string CC
        {
            get { return _cC; }
            set { base.SetPropertyValue("CC", value); }
        }

        private string _bcc;
        [IsNullable]
        [DbType("char(100)")]
        public string Bcc
        {
            get { return _bcc; }
            set { base.SetPropertyValue("Bcc", value); }
        }

        private string _from;
        [DbType("char(100)")]
        public string From
        {
            get { return _from; }
            set { base.SetPropertyValue("From", value); }
        }

        private string _to;
        [DbType("char(100)")]
        public string To
        {
            get { return _to; }
            set { base.SetPropertyValue("To", value); }
        }

        private string _body;
        [IsNullable]
        [DbType("ntext(1073741823)")]
        public string Body
        {
            get { return _body; }
            set { base.SetPropertyValue("Body", value); }
        }

        private string _priority;
        [IsNullable]
        [DbType("char(20)")]
        public string Priority
        {
            get { return _priority; }
            set { base.SetPropertyValue("Priority", value); }
        }

        private DateTime _sendDate;
        [DbType("datetime")]
        public DateTime SendDate
        {
            get { return _sendDate; }
            set { base.SetPropertyValue("SendDate", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Body; }
        }
    }
}
