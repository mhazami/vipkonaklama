using System;
using Radyn.Framework;

namespace Radyn.Common.DataStructure
{
    [Serializable]
    [Description("ارز")]
    [Schema("Common")]
    public sealed class Currency : DataStructureBase<Currency>
    {
        private Int16 _id;
        [Layout(Caption = "كد")]
        [Key(true)]
        [DbType("smallint")]
        public Int16 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _title;
        [Layout(Caption = "عنوان")]
        [DbType("nvarchar(15)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private bool _enabled;
        [Layout(Caption = "فعال")]
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
