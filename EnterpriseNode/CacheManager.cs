using System;
using Radyn.Cache;

namespace Radyn.EnterpriseNode
{
    internal class EnterpriseNodeCacheManager : CacheCategoryManager
    {
        private static EnterpriseNodeCacheManager instance { get; set; }

        public static EnterpriseNodeCacheManager EnterpriseNodeCache
        {
            get { return instance ?? (instance = new EnterpriseNodeCacheManager()); }
        }

        public const string CacheCategoryName = "EnterpriseNode";
        public EnterpriseNodeCacheManager()
        {
            base.CategoryName = CacheCategoryName;
            base.CategoryInfo.ExpirationType=ExpirationTypes.NeverExpire;
        }

        public void Add(Guid key, DataStructure.EnterpriseNode obj)
        {
            if (base[key.ToString()] == null)
                base.Add(key.ToString(), obj);
        }

        public void Remove(Guid key)
        {
            if (base[key.ToString()] != null)
                base.Remove(key.ToString());
        }

        //public Model.EnterpriseNode this[Guid key]
        //{
        //    get
        //    {
        //        return base[key.ToString()] as Model.EnterpriseNode;
        //    }
        //    set { base[key.ToString()] = value; }
        //}
    }
}
