using System;
using Radyn.Framework;


namespace Radyn.Slider.DataStructure
{
    [Serializable]
    [Schema("Slider")]
    public sealed class WebDesignSlider : DataStructureBase<WebDesignSlider>
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

    

        private Int16 _slideId;
        [Key(false)]
        [DbType("smallint")]
        public Int16 SlideId
        {
            get
            {
                return this._slideId;
            }
            set
            {
                base.SetPropertyValue("SlideId", value);
                this.WebSiteSlide = new Radyn.Slider.DataStructure.Slide { Id = value };
            }
        }

        [Assosiation(PropName = "SlideId")]
        public Radyn.Slider.DataStructure.Slide WebSiteSlide { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.WebSiteSlide.Title; }
        }
    }
}
