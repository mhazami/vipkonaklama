using System;
using Radyn.Framework;

namespace Radyn.News.DataStructure
{
    [Serializable]
    [Schema("News")]
    public sealed class RelatedNews : DataStructureBase<RelatedNews>
    {
        private Guid _relatedNewsGroupId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid RelatedNewsGroupId
        {
            get
            {
                return this._relatedNewsGroupId;
            }
            set
            {
                base.SetPropertyValue("RelatedNewsGroupId", value);
                if (RelatedNewsGroup == null)
                    this.RelatedNewsGroup = new RelatedNewsGroup { Id = value };
            }
        }

        [Assosiation(PropName = "RelatedNewsGroupId")]
        public RelatedNewsGroup RelatedNewsGroup { get; set; }

        private Int32 _newsId;
        [Key(false)]
        [DbType("int")]
        public Int32 NewsId
        {
            get
            {
                return this._newsId;
            }
            set
            {
                base.SetPropertyValue("NewsId", value);
                if (News == null)
                    this.News = new Radyn.News.DataStructure.News { Id = value };
            }
        }

        [Assosiation(PropName = "NewsId")]
        public Radyn.News.DataStructure.News News { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.News.DescriptionField; }
        }
    }
}
