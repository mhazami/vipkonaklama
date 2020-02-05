using System;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;

namespace Radyn.Message.DataStructure
{
    [Serializable]
    [Schema("Message")]
    public sealed class MailAttachment : DataStructureBase<MailAttachment>
    {
        private Guid _mailId;
        [Key(false)]
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


        private Guid _attachId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid AttachId
        {
            get
            {
                return this._attachId;
            }
            set
            {
                base.SetPropertyValue("AttachId", value);
                if (AttachFile == null)
                    this.AttachFile = new File { Id = value };
            }
        }

        [Assosiation(PropName = "AttachId")]
        public File AttachFile { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.MailInfo.Subject; }
        }
    }
}
