using System;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Slider.DataStructure;

namespace Radyn.Slider.BO
{
    internal class SlideItemBO : BusinessBase<SlideItem>
    {
        public bool Delete(IConnectionHandler connectionHandler, IConnectionHandler filemanagerconnection,  params object[] keys)
        {
            var obj = this.Get(connectionHandler, keys);
            if (!base.Delete(connectionHandler, keys))
                throw new Exception("خطایی در حذف اسلاید  آیتم وجود دارد");
            if (obj.ImageId != null && !string.IsNullOrWhiteSpace(obj.ImageId))
                FileManagerComponent.Instance.FileTransactionalFacade(filemanagerconnection).Delete(obj.ImageId);
           
            return true;
        }


     
    }
}
