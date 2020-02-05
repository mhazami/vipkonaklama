using System;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Gallery.BO
{
    internal class WebDesignGalleryBO : BusinessBase<DataStructure.WebDesignGallery>
    {
        public override bool Insert(IConnectionHandler connectionHandler, DataStructure.WebDesignGallery obj)
        {
            obj.WebSiteGallery.CreateDate = Utility.DateTimeUtil.ShamsiDate(DateTime.Now);
            return base.Insert(connectionHandler, obj);
        }
    }
}
