using System;
using Radyn.Framework;

namespace Radyn.News.DataStructure
{
    [Serializable]
    [Schema("News")]
    public sealed class NewsProperty : DataStructureBase<NewsProperty>
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

        private bool _isSelection;
        [DbType("bit")]
        public bool IsSelection
        {
            get { return _isSelection; }
            set { base.SetPropertyValue("IsSelection", value); }
        }

        private bool _isGeneralView;
        [DbType("bit")]
        public bool IsGeneralView
        {
            get { return _isGeneralView; }
            set { base.SetPropertyValue("IsGeneralView", value); }
        }

        private bool _isNewsGroupSelection;
        [DbType("bit")]
        public bool IsNewsGroupSelection
        {
            get { return _isNewsGroupSelection; }
            set { base.SetPropertyValue("IsNewsGroupSelection", value); }
        }

        private Int16 _order;
        [DbType("smallint")]
        public Int16 Order
        {
            get { return _order; }
            set { base.SetPropertyValue("Order", value); }
        }

        private Int16 _newsGroupOrder;
        [DbType("smallint")]
        public Int16 NewsGroupOrder
        {
            get { return _newsGroupOrder; }
            set { base.SetPropertyValue("NewsGroupOrder", value); }
        }

        private bool _hasContentPic;
        [DbType("bit")]
        public bool HasContentPic
        {
            get { return _hasContentPic; }
            set { base.SetPropertyValue("HasContentPic", value); }
        }

        private bool _hasAttachment;
        [DbType("bit")]
        public bool HasAttachment
        {
            get { return _hasAttachment; }
            set { base.SetPropertyValue("HasAttachment", value); }
        }

        private bool _isImageReport;
        [DbType("bit")]
        public bool IsImageReport
        {
            get { return _isImageReport; }
            set { base.SetPropertyValue("IsImageReport", value); }
        }

        private Int32? _newsContentTypeId;
        [IsNullable]
        [DbType("int")]
        public Int32? NewsContentTypeId
        {
            get
            {
                return this._newsContentTypeId;
            }
            set
            {
                base.SetPropertyValue("NewsContentTypeId", value);
                if (NewsContentType == null)
                {
                    if (value.HasValue)
                        this.NewsContentType = new NewsContentType { Id = value.Value };
                    else
                        this.NewsContentType = null;
                }
            }
        }

        [Assosiation(PropName = "NewsContentTypeId")]
        public NewsContentType NewsContentType { get; set; }

        private bool _isSoundNews;
        [DbType("bit")]
        public bool IsSoundNews
        {
            get { return _isSoundNews; }
            set { base.SetPropertyValue("IsSoundNews", value); }
        }

        private bool _isMovieNews;
        [DbType("bit")]
        public bool IsMovieNews
        {
            get { return _isMovieNews; }
            set { base.SetPropertyValue("IsMovieNews", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.News.DescriptionField; }
        }
    }
}
