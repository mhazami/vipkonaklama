using System;
using Radyn.Common.DataStructure;
using Radyn.Framework;

namespace Radyn.News.DataStructure
{
    [Serializable]
    [Description("بازدید اخبار روزانه")]
    [Schema("News")]
    public sealed class NewsDailyVisit : DataStructureBase<NewsDailyVisit>
    {
        private Int32 _id;
        [Key(false)]
        [DbType("int")]
        public Int32 Id
        {
            get
            {
                return this._id;
            }
            set
            {
                base.SetPropertyValue("Id", value);
                if (News == null)
                    this.News = new Radyn.News.DataStructure.News { Id = value };
            }
        }

        [Assosiation(PropName = "Id")]
        public Radyn.News.DataStructure.News News { get; set; }

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

        private DateTime _day;
        [Key(false)]
        [DbType("datetime")]
        public DateTime Day
        {
            get { return _day; }
            set { base.SetPropertyValue("Day", value); }
        }

        private Int32 _visitCount;
        [DbType("int")]
        public Int32 VisitCount
        {
            get { return _visitCount; }
            set { base.SetPropertyValue("VisitCount", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.VisitCount.ToString(); }
        }
    }
}
