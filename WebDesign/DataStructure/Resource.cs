using System;
using Radyn.Framework;

namespace Radyn.WebDesign.DataStructure
{
    [Serializable]
    [Schema("WebDesign")]
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

        private Guid _webId;
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
                this.WebSite = new WebSite { Id = value };
            }
        }

        [Assosiation]
        public WebSite WebSite { get; set; }

        private byte _type;
        [DbType("tinyint")]
        public byte Type
        {
            get { return _type; }
            set { base.SetPropertyValue("Type", value); }
        }

        private string _fileId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        [MultiLanguage]
        public string FileId
        {
            get { return _fileId; }
            set
            {
                base.SetPropertyValue("FileId", value);

            }

        }


        private string _title;
        [IsNullable]
        [DbType("nvarchar(1000)")]
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private string _text;
        [IsNullable]
        [DbType("nvarchar(1000)")]
        [MultiLanguage]
        public string Text
        {
            get { return _text; }
            set { base.SetPropertyValue("Text", value); }
        }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }
        private byte _order;
        [DbType("tinyint")]
        public byte Order
        {
            get { return _order; }
            set { base.SetPropertyValue("Order", value); }
        }

        private string _useLayoutId;
        [DbType("varchar(30)")]
        public string UseLayoutId
        {
            get { return _useLayoutId; }
            set { base.SetPropertyValue("UseLayoutId", value); }
        }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Order.ToString(); }
        }

        
    }
}
