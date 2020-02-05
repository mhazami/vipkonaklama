using System;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;

namespace Radyn.News.DataStructure
{
    [Serializable]
    [Description("فایل پیوست شده به خبر")]
    [Schema("News")]
    public sealed class NewsAttachedFile : DataStructureBase<NewsAttachedFile>
    {
        private Int32 _newsId;
        [Layout(Caption = "کد خبر")]
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

        [Layout(Caption = "کد خبر")]
        [Assosiation(PropName = "NewsId")]
        public Radyn.News.DataStructure.News News { get; set; }

        private Guid _fileId;
        [Layout(Caption = "کد فایل پیوست شده")]
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid FileId
        {
            get
            {
                return this._fileId;
            }
            set
            {
                base.SetPropertyValue("FileId", value);
                if (File == null)
                    this.File = new File { Id = value };
            }
        }

        [Layout(Caption = "کد فایل پیوست شده")]
        [Assosiation(PropName = "FileId")]
        public File File { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.File.FileName; }
        }
    }
}
