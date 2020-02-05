using System;
using Radyn.Framework;

namespace Radyn.FAQ.DataStructure
{
    [Serializable]
    [Schema("FAQ")]
    public sealed class FAQ : DataStructureBase<FAQ>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _question;
        [DbType("nvarchar(500)")]
        public string Question
        {
            get { return _question; }
            set { base.SetPropertyValue("Question", value); }
        }

        private string _answer;
        [DbType("ntext")]
        public string Answer
        {
            get { return _answer; }
            set { base.SetPropertyValue("Answer", value); }
        }

        private Guid? _creatorId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? CreatorId
        {
            get { return _creatorId; }
            set { base.SetPropertyValue("CreatorId", value); }
        }

        private string _createDate;
        [DbType("char(10)")]
        public string CreateDate
        {
            get { return _createDate; }
            set { base.SetPropertyValue("CreateDate", value); }
        }

        private Int32? _viewCount;
        [IsNullable]
        [DbType("int")]
        public Int32? ViewCount
        {
            get { return _viewCount; }
            set { base.SetPropertyValue("ViewCount", value); }
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
      

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Question; }
        }
    }
}
