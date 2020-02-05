using System;
using Radyn.Common.DataStructure;
using Radyn.Framework;

namespace Radyn.Help.DataStructure
{
    [Serializable]
    [Schema("Help")]
    public sealed class HelpContent : DataStructureBase<HelpContent>
    {
        private Guid _helpId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid HelpId
        {
            get
            {
                return this._helpId;
            }
            set
            {
                base.SetPropertyValue("HelpId", value);
                if (Help == null)
                    this.Help = new Help { Id = value };
            }
        }

        [Assosiation(PropName = "HelpId")]
        public Help Help { get; set; }

        private string _languageId;
        [Key(false)]
        [DbType("char(5)")]
        public string LanguageId
        {
            get
            {
                return this._languageId;
            }
            set
            {
                base.SetPropertyValue("LanguageId", value);
                if (Language == null)
                    this.Language = new Language { Id = value };
            }
        }

        [Assosiation(PropName = "LanguageId")]
        public Language Language { get; set; }

        private string _title;
        [IsNullable]
        [DbType("nvarchar(500)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private string _content;
        [IsNullable]
        [DbType("ntext")]
        public string Content
        {
            get { return _content; }
            set { base.SetPropertyValue("Content", value); }
        }

        private DateTime _createDate;
        [DbType("datetime")]
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { base.SetPropertyValue("CreateDate", value); }
        }

        private DateTime _lastUpdate;
        [DbType("datetime")]
        public DateTime LastUpdate
        {
            get { return _lastUpdate; }
            set { base.SetPropertyValue("LastUpdate", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
