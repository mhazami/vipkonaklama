using System;
using Radyn.Framework;

namespace Radyn.Gallery.DataStructure
{
    [Serializable]
    [Schema("Gallery")]
    public sealed class WebDesignGallery : DataStructureBase<WebDesignGallery>
    {
        private Guid _webId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid WebId
        {
            get
            {
                return this._webId;
            }
            set
            {
                base.SetPropertyValue("WebId", value);
               
            }
        }

        

        private Guid _galleryId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid GalleryId
        {
            get
            {
                return this._galleryId;
            }
            set
            {
                base.SetPropertyValue("GalleryId", value);
                this.WebSiteGallery = new Radyn.Gallery.DataStructure.Gallery { Id = value };
            }
        }

        [Assosiation(PropName = "GalleryId")]
        public Radyn.Gallery.DataStructure.Gallery WebSiteGallery { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.WebSiteGallery.Title; }
        }
    }
}
