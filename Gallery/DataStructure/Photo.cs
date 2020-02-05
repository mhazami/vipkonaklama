using System;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;

namespace Radyn.Gallery.DataStructure
{
    [Serializable]
    [Schema("Gallery")]
    public sealed class Photo : DataStructureBase<Photo>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private Guid _galleryId;
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
                if (Gallery == null)
                    this.Gallery = new Radyn.Gallery.DataStructure.Gallery { Id = value };
            }
        }

        [Assosiation(PropName = "GalleryId")]
        public Gallery Gallery { get; set; }

        private string _title;
        [IsNullable]
        [DbType("nvarchar(550)")]
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private Guid _picFile;
        [DbType("uniqueidentifier")]
        public Guid PicFile
        {
            get
            {
                return this._picFile;
            }
            set
            {
                base.SetPropertyValue("PicFile", value);
                if (File == null)
                    this.File = new File { Id = value };
            }
        }

        [Assosiation(PropName = "PicFile")]
        public File File { get; set; }

        private string _uploadDate;
        [DbType("char(10)")]
        public string UploadDate
        {
            get { return _uploadDate; }
            set { base.SetPropertyValue("UploadDate", value); }
        }

        private Guid? _uploader;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? Uploader
        {
            get
            {
                return this._uploader;
            }
            set
            {
                base.SetPropertyValue("Uploader", value);
                if (EnterpriseNode == null)
                    this.EnterpriseNode = value.HasValue ? new EnterpriseNode.DataStructure.EnterpriseNode() { Id = value.Value } : null;
            }
        }

        [Assosiation(PropName = "Uploader")]
        public EnterpriseNode.DataStructure.EnterpriseNode EnterpriseNode { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
