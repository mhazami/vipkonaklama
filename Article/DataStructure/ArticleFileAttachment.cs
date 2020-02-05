using System;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;

namespace Radyn.Article.DataStructure
{
    [Serializable]
    [Schema("Article")]
    public sealed class ArticleFileAttachment : DataStructureBase<ArticleFileAttachment>
    {
        private Guid _articleId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid ArticleId
        {
            get
            {
                return this._articleId;
            }
            set
            {
                base.SetPropertyValue("ArticleId", value);
                if (Article == null)
                    this.Article = new Radyn.Article.DataStructure.Article { Id = value };
            }
        }

        [Assosiation(PropName = "ArticleId", FillData = false)]
        public Article Article { get; set; }

        private Guid _fileId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid FileId
        {
            get { return _fileId; }
            set { base.SetPropertyValue("FileId", value); }
        }

        [Assosiation(PropName = "FileId")]
        public File File { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string FileTitle { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.File.DescriptionField; }
        }
    }
}
