using System;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager.BO
{
    internal class ContainerBO : BusinessBase<Container>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Container obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }

        public override bool Delete(IConnectionHandler connectionHandler, params object[] keys)
        {
            var containerBO = new ContainerBO();
            var obj = containerBO.Get(connectionHandler, keys);
            var contents = new ContentBO().Any(connectionHandler,
                container => container.ContainerId == obj.Id);
            if (contents)
            {
                throw new Exception("این قالب  در محتوا استفاده شده است و قابل حذف نیست");
            }
            if (!base.Delete(connectionHandler, keys))
                throw new Exception("خطایی در حذف  قالب وجود دارد");
            return true;
        }
    }
}
