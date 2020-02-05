using System;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;

namespace Radyn.Graphic.DataStructure
{
    [Serializable]
    [Schema("Graphic")]
    public sealed class Resource : DataStructureBase<Resource>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private Guid _themeId;
        [DbType("uniqueidentifier")]
        public Guid ThemeId
        {
            get
            {
                return this._themeId;
            }
            set
            {
                base.SetPropertyValue("ThemeId", value);
                if (Theme == null)
                    this.Theme = new Theme { Id = value };
            }
        }

        [Assosiation(PropName = "ThemeId")]
        public Theme Theme { get; set; }

        private string _lanuageId;
        [DbType("char(5)")]
        public string LanuageId
        {
            get { return _lanuageId; }
            set { base.SetPropertyValue("LanuageId", value); }
        }

        private byte _type;
        [DbType("tinyint")]
        public byte Type
        {
            get { return _type; }
            set { base.SetPropertyValue("Type", value); }
        }

        private Guid? _fileId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? FileId
        {
            get { return _fileId; }
            set { base.SetPropertyValue("FileId", value); }
        }
        [Assosiation(PropName = "FileId")]
        public File File { get; set; }


        private byte _order;
        [DbType("tinyint")]
        public byte Order
        {
            get { return _order; }
            set { base.SetPropertyValue("Order", value); }
        }

        private string _text;
        [IsNullable]
        [DbType("nvarchar(1000)")]
        public string Text
        {
            get { return _text; }
            set { base.SetPropertyValue("Text", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Text; }
        }

    }
}
