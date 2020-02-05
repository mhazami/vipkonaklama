using System;
using Radyn.ContentManager.Definition;
using Radyn.Framework;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class PartialLoad : DataStructureBase<PartialLoad>
    {
        private string _partialId;
        [Key(false)]
        [DbType("varchar(40)")]
        public string PartialId
        {
            get { return _partialId; }
            set { base.SetPropertyValue("PartialId", value); }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public Content Content { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public Partials Partials { get; set; }

        private string _customId;
        [Key(false)]
        [DbType("nvarchar(100)")]
        public string CustomId
        {
            get { return _customId; }
            set { base.SetPropertyValue("CustomId", value); }
        }

        private Guid _htmlDesginId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid HtmlDesginId
        {
            get
            {
                return this._htmlDesginId;
            }
            set
            {
                base.SetPropertyValue("HtmlDesginId", value);
                if (HtmlDesgin == null)
                    this.HtmlDesgin = new HtmlDesgin { Id = value };
            }
        }

        [Assosiation(PropName = "HtmlDesginId")]
        public HtmlDesgin HtmlDesgin { get; set; }

        private byte _position;
        [DbType("tinyint")]
        public byte position
        {
            get { return _position; }
            set { base.SetPropertyValue("position", value); }
        }

        private bool _hasContainer;
        [DbType("bit")]
        public bool HasContainer
        {
            get { return _hasContainer; }
            set { base.SetPropertyValue("HasContainer", value); }
        }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public Enums.PartialTypes Type { get; set; }

      
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string Html { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return ""; }
        }
    }
}
