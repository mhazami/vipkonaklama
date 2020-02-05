using System;
using Radyn.Framework;

namespace Radyn.News.DataStructure
{
    [Serializable]
    [Description("نحوه انتشار خبر")]
    [Schema("News")]
    public sealed class NewsPublishType : DataStructureBase<NewsPublishType>
    {
        private Int32 _newsId;
        [Layout(Caption = "کد خبر")]
        [Key(false)]
        [DbType("int")]
        public Int32 NewsId
        {
            get
            {
                return this._newsId;
            }
            set
            {
                base.SetPropertyValue("NewsId", value);
                if (News == null)
                    this.News = new Radyn.News.DataStructure.News { Id = value };
            }
        }

        [Layout(Caption = "کد خبر")]
        [Assosiation(PropName = "NewsId")]
        public Radyn.News.DataStructure.News News { get; set; }

        private Int32 _publishTypeId;
        [Layout(Caption = "کد نحوه انتشار خبر")]
        [Key(false)]
        [DbType("int")]
        public Int32 PublishTypeId
        {
            get
            {
                return this._publishTypeId;
            }
            set
            {
                base.SetPropertyValue("PublishTypeId", value);
                if (PublishType == null)
                    this.PublishType = new PublishType { Id = value };
            }
        }

        [Layout(Caption = "کد نحوه انتشار خبر")]
        [Assosiation(PropName = "PublishTypeId")]
        public PublishType PublishType { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.PublishType.Title; }
        }
    }
}
