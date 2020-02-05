using System;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;

namespace Radyn.Article.DataStructure
{
    [Serializable]
    [Schema("Article")]
    public sealed class ArticleCategory : DataStructureBase<ArticleCategory>
    {
        private Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _title;
        [DbType("nvarchar(100)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }


        private bool _enable;
        [DbType("bit")]
        public bool Enable
        {
            get { return _enable; }
            set { base.SetPropertyValue("Enable", value); }
        }

        private Int16? _parentId;
        [IsNullable]
        [DbType("smallint")]
        public Int16? ParentId
        {
            get
            {
                return this._parentId;
            }
            set
            {
                base.SetPropertyValue("ParentId", value);
                if (value.HasValue)
                    this.ParentCategory = new ArticleCategory { Id = value.Value };
                else
                    this.ParentCategory = null;
            }
        }

        [Assosiation(PropName ="ParentId")]
        public ArticleCategory ParentCategory { get; set; }


        private Int32 _order;
        [DbType("int")]
        public Int32 Order
        {
            get { return _order; }
            set { base.SetPropertyValue("Order", value); }
        }

        private Guid? _fileId;
        [DbType("uniqueidentifier")]
        public Guid? FileId
        {
            get { return _fileId; }
            set { base.SetPropertyValue("FileId", value); }
        }

        [Assosiation(PropName = "FileId")]
        public File File { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
