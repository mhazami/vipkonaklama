using System;
using Radyn.Framework;

namespace Radyn.News.DataStructure
{
    [Serializable]
    [Description("دسته بندی اخبار")]
    [Schema("News")]
    public sealed class NewsCategory : DataStructureBase<NewsCategory>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _title;
        [Layout(Caption = "عنوان پیش فرض")]
        [DbType("nvarchar(50)")]
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private Guid? _parentCategoryId;
        [Layout(Caption = "رده بالادستی")]
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? ParentCategoryId
        {
            get
            {
                return this._parentCategoryId;
            }
            set
            {
                base.SetPropertyValue("ParentCategoryId", value);
                if (ParentNewsCategory == null)
                {
                    if (value.HasValue)
                        this.ParentNewsCategory = new NewsCategory { Id = value.Value };
                    else
                        this.ParentNewsCategory = null;
                }
            }
        }

        [Layout(Caption = "رده بالادستی")]
        [Assosiation(PropName = "ParentCategoryId")]
        public NewsCategory ParentNewsCategory { get; set; }

        private bool _enabled;
        [Layout(Caption = "فعال ")]
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
