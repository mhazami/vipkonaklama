using System;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;

namespace Radyn.Message.DataStructure
{
    [Serializable]
    [Schema("Mail")]
    public sealed class SentMailAttachment : DataStructureBase<SentMailAttachment>
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
                if (SentMail == null)
                    this.SentMail = new SentMail { Id = value };
            }
        }

        [Assosiation(PropName = "MailId")]
        public SentMail SentMail { get; set; }

        private Guid _fileId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid FileId
        {
            get
            {
                return this._fileId;
            }
            set
            {
                base.SetPropertyValue("FileId", value);
                if (File == null)
                    this.File = new File { Id = value };
            }
        }

        [Assosiation(PropName = "FileId")]
        public File File { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.SentMail.Body; }
        }
    }
}
