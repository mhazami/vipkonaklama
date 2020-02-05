using System;
using Radyn.Framework;

namespace Radyn.Advertisements.DataStructure
{
    [Serializable]
    [Description("سمت ")]
    [Schema("Advertisement")]
    public sealed class Position : DataStructureBase<Position>
    {
        private byte _id;
        [Layout(Caption = "کد سمت")]
        [Key(true)]
        [DbType("tinyint")]
        public byte Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _title;
        [Layout(Caption = "عنوان سمت")]
        [IsNullable]
        [DbType("nvarchar(100)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
