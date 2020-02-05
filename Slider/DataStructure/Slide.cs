using System;
using System.Collections.Generic;
using Radyn.Framework;

namespace Radyn.Slider.DataStructure
{
    [Serializable]
    [Schema("Slider")]
    public sealed class Slide : DataStructureBase<Slide>
    {
        public Slide()
        {
            this.SlideItems = new List<SlideItem>();
        }
        private Int16 _id;
        [Key(true)]
        [DbType("smallint")]
        public Int16 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _title;
        [DbType("nvarchar(50)")]
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }
        private bool _isExternal;
        [DbType("bit")]
        public bool IsExternal
        {
            get { return _isExternal; }
            set { base.SetPropertyValue("IsExternal", value); }
        }

        private string _effectType;
        [IsNullable]
        [DbType("varchar(20)")]
        public string EffectType
        {
            get { return _effectType; }
            set { base.SetPropertyValue("EffectType", value); }
        }

        private int? _heightSlide;
        [IsNullable]
        [DbType("int")]
        public int? HeightSlide
        {
            get { return _heightSlide; }
            set { base.SetPropertyValue("HeightSlide", value); }
        }

        private int _slidItemTimeOut;
        [DbType("int")]
        public int SlidItemTimeOut
        {
            get { return _slidItemTimeOut; }
            set { base.SetPropertyValue("SlidItemTimeOut", value); }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public List<SlideItem> SlideItems { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
