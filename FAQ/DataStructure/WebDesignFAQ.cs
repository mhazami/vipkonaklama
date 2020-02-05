using System;
using Radyn.Framework;

namespace Radyn.FAQ.DataStructure
{
    [Serializable]
    [Schema("FAQ")]
    public sealed class WebDesignFAQ : DataStructureBase<WebDesignFAQ>
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

    
        private Guid _fAQId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid FAQId
        {
            get
            {
                return this._fAQId;
            }
            set
            {
                base.SetPropertyValue("FAQId", value);
                this.WebSiteFaq = new Radyn.FAQ.DataStructure.FAQ { Id = value };
            }
        }

        [Assosiation]
        public Radyn.FAQ.DataStructure.FAQ WebSiteFaq { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.WebSiteFaq.Question; }
        }
    }
}
