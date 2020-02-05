using System;
using Radyn.Framework;

namespace Radyn.Common.DataStructure
{
    [Serializable]
    [Schema("Common")]
    public sealed class WebDesignLanguage : DataStructureBase<WebDesignLanguage>
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

     

        private string _languageId;
        [Key(false)]
        [DbType("char(5)")]
        public string LanguageId
        {
            get
            {
                return this._languageId;
            }
            set
            {
                base.SetPropertyValue("LanguageId", value);
                this.WebSiteLanguage = new Radyn.Common.DataStructure.Language { Id = value };
            }
        }

        [Assosiation(PropName = "LanguageId")]
        public Radyn.Common.DataStructure.Language WebSiteLanguage { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.WebSiteLanguage.DisplayName; }
        }
    }
}
