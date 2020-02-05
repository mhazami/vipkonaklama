using System;
using Radyn.Framework;
using Radyn.Security.DataStructure;

namespace Radyn.News.DataStructure
{
    [Serializable]
    [Schema("News")]
    public sealed class News : DataStructureBase<News>
    {
        private Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private DateTime _saveDate;
        [DbType("datetime")]
        public DateTime SaveDate
        {
            get { return _saveDate; }
            set { base.SetPropertyValue("SaveDate", value); }
        }

        private string _publishDate;
        [DbType("nchar(10)")]
        public string PublishDate
        {
            get { return _publishDate; }
            set { base.SetPropertyValue("PublishDate", value); }
        }

        private string _publishTime;
        [DbType("nchar(10)")]
        public string PublishTime
        {
            get { return _publishTime; }
            set { base.SetPropertyValue("PublishTime", value); }
        }

        private Guid? _interviewerId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? InterviewerId
        {
            get { return _interviewerId; }
            set { base.SetPropertyValue("InterviewerId", value); }
        }

        private Guid? _creatorId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? CreatorId
        {
            get
            {
                return this._creatorId;
            }
            set
            {
                base.SetPropertyValue("CreatorId", value);
                if (User == null)
                {
                    if (value.HasValue)
                        this.User = new User { Id = value.Value };
                    else
                        this.User = null;
                }
            }
        }

        [Assosiation(PropName = "CreatorId")]
        public User User { get; set; }

        private Guid? _editorId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? EditorId
        {
            get
            {
                return this._editorId;
            }
            set
            {
                base.SetPropertyValue("EditorId", value);
                if (EnterpriseNode == null)
                {
                    if (value.HasValue)
                        this.EnterpriseNode = new EnterpriseNode.DataStructure.EnterpriseNode() { Id = value.Value };
                    else
                        this.EnterpriseNode = null;
                }
            }
        }

        [Assosiation(PropName = "EditorId")]
        public EnterpriseNode.DataStructure.EnterpriseNode EnterpriseNode { get; set; }

        private Guid? _newsCategoryId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? NewsCategoryId
        {
            get
            {
                return this._newsCategoryId;
            }
            set
            {
                base.SetPropertyValue("NewsCategoryId", value);
                if (NewsCategory == null)
                {
                    if (value.HasValue)
                        this.NewsCategory = new NewsCategory { Id = value.Value };
                    else
                        this.NewsCategory = null;
                }
            }
        }

        [Assosiation(PropName = "NewsCategoryId")]
        public NewsCategory NewsCategory { get; set; }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }

        private bool _pined;
        [DbType("bit")]
        public bool Pined
        {
            get { return _pined; }
            set { base.SetPropertyValue("Pined", value); }
        }

        private Guid? _thumbnailId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? ThumbnailId
        {
            get { return _thumbnailId; }
            set { base.SetPropertyValue("ThumbnailId", value); }
        }
        private bool _isExternal;
        [DbType("bit")]
        public bool IsExternal
        {
            get { return _isExternal; }
            set { base.SetPropertyValue("IsExternal", value); }
        }

        private bool? _visible;
        [DbType("bit")]
        public bool? Visible
        {
            get { return _visible; }
            set { base.SetPropertyValue("Visible", value); }
        }

        private NewsContent _newsContent;

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public NewsContent NewsContent
        {
            get { return _newsContent ?? new NewsContent() { Body = "", Title1 = "", Title2 = "", Lead = "", Sutitr = "", OverTitle = "" }; }
            set { _newsContent = value; }
        }


        private bool _getComment;
        [DbType("bit")]
        public bool GetComment { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.PublishDate; }
        }
    }
}
