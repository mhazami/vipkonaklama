using System;
using Radyn.Cache;
using Radyn.FileManager.DataStructure;

namespace Radyn.FileManager.CacheManager
{
    public class FileCacheManager : CacheResource
    {
        private string cacheCategoryKey = "RadynFileManagerCacheManager";

        public FileCacheManager()
        {
            this.AddCategory(cacheCategoryKey, ExpirationTypes.ExpireIfNotUsedAfterTime, new TimeSpan(0, 1, 0, 0));
        }

        public static FileCacheManager FileCache { get; } = new FileCacheManager();

        public void AddItem(File file)
        {
            if (file != null)
            {
                var theKey = file.Id.ToString();
                var item = this[this.cacheCategoryKey, theKey];
                if (item == null)
                    this.Add(this.cacheCategoryKey, theKey, file);
                this[this.cacheCategoryKey, theKey] = file;
            }
        }
        public File GetItem(Guid fileId)
        {
            var theKey = fileId.ToString();
            var item = this[this.cacheCategoryKey, theKey] as File;
            return item;
        }

        public void RemoveItem(Guid fileId)
        {
            this.Remove(this.cacheCategoryKey, fileId.ToString());
        }
    }
}