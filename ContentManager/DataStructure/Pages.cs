using Radyn.Framework;
using System;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class Pages : DataStructureBase<Pages>
    {
        public Pages()
        {
            Enabled = false;
        }
        private int _id;
        [Key(true)]
        [DbType("int")]
        public int Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }


        private string _url;
        [DbType("nvarchar(500)")]
        [IsNullable]
        public string Url
        {
            get { return _url; }
            set { base.SetPropertyValue("Url", value); }
        }

        private string _title;
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private Guid _htmlId;

        [DbType("uniqueidentifier")]
        public Guid HtmlId
        {
            get { return _htmlId; }
            set
            {
                base.SetPropertyValue("HtmlId", value);
                if (HtmlDesgin == null)
                {
                    this.HtmlDesgin = new HtmlDesgin { Id = value };
                }
            }
        }
        [Assosiation(PropName = "HtmlId")]
        public HtmlDesgin HtmlDesgin { get; set; }


        private bool _enabled;
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
