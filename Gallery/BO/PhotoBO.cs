using System;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Gallery.DataStructure;
using Radyn.Utility;

namespace Radyn.Gallery.BO
{
    internal class PhotoBO : BusinessBase<Photo>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Photo obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            if (obj.UploadDate==null||string.IsNullOrEmpty(obj.UploadDate.Trim()))
                obj.UploadDate = DateTime.Now.ShamsiDate();
            return base.Insert(connectionHandler, obj);
        }

       
    }
}
