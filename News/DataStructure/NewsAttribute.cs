using System;
using Radyn.Framework;

namespace Radyn.News.DataStructure
{
    [Serializable]
    [Description("ویژگی های خبر")]
    [Schema("News")]
    public sealed class NewsAttribute : DataStructureBase<NewsAttribute>
    {
        private Int32 _id;
        [Layout(Caption = "کد ویژگی های خبر")]
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

        [Layout(Caption = "کد ویژگی های خبر")]
        [Assosiation(PropName = "Id")]
        public Radyn.News.DataStructure.News News { get; set; }

        private Guid? _galleryId;
        [Layout(Caption = "کد گالری")]
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? GalleryId
        {
            get
            {
                return this._galleryId;
            }
            set
            {
                base.SetPropertyValue("GalleryId", value);
                if (Gallery == null)
                {
                    if (value.HasValue)
                        this.Gallery = new Gallery.DataStructure.Gallery() { Id = value.Value };
                    else
                        this.Gallery = null;
                }
              
            }
        }

        [Layout(Caption = "کد گالری")]
        [Assosiation(PropName = "GalleryId")]
        public Gallery.DataStructure.Gallery Gallery { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Gallery.Title; }
        }
    }
}
