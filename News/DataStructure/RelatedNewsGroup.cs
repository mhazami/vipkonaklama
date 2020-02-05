using System;
using Radyn.Framework;

namespace Radyn.News.DataStructure
{
    [Serializable]
    [Schema("News")]
    public sealed class RelatedNewsGroup : DataStructureBase<RelatedNewsGroup>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private Int32? _mainNewsId;
        [IsNullable]
        [DbType("int")]
        public Int32? MainNewsId
        {
            get
            {
                return this._mainNewsId;
            }
            set
            {
                base.SetPropertyValue("MainNewsId", value);
                if (News == null)
                {
                    if (value.HasValue)
                        this.News = new Radyn.News.DataStructure.News { Id = value.Value };
                    else
                        this.News = null;
                }
            }
        }

        [Assosiation(PropName = "MainNewsId")]
        public Radyn.News.DataStructure.News News { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.News.DescriptionField; }
        }
    }
}
