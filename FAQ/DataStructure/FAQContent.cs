using System;
using Radyn.Common.DataStructure;
using Radyn.Framework;

namespace Radyn.FAQ.DataStructure
{
    [Serializable]
    [Schema("FAQ")]
    public sealed class FAQContent : DataStructureBase<FAQContent>
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
                if (FAQ == null)
                    this.FAQ = new Radyn.FAQ.DataStructure.FAQ { Id = value };
            }
        }


        [Assosiation(PropName = "Id")]
        //[Assosiation]
        public Radyn.FAQ.DataStructure.FAQ FAQ { get; set; }

        private string _question;
        [IsNullable]
        [DbType("nvarchar(500)")]
        public string Question
        {
            get { return _question; }
            set { base.SetPropertyValue("Question", value); }
        }

        private string _answer;
        [IsNullable]
        [DbType("ntext")]
        public string Answer
        {
            get { return _answer; }
            set { base.SetPropertyValue("Answer", value); }
        }

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

        // [Assosiation(PropName = "LanguageId")]
        [Assosiation]
        public Language Language { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Question; }
        }
    }
}
