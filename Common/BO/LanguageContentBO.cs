using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Radyn.Common.DA;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.ResourceManager;

namespace Radyn.Common.BO
{
    internal class LanguageContentBO : BusinessBase<LanguageContent>
    {
        protected override void CheckConstraint(IConnectionHandler connectionHandler, LanguageContent item)
        {
            base.CheckConstraint(connectionHandler, item);
            if (!string.IsNullOrEmpty(item.Value))
                item.Value = item.Value.Replace("'", "''");
        }
        public List<LanguageContent> GetByMoudelname(IConnectionHandler connectionHandler, string moudelname, string culture)
        {
            var key = string.Format("{0}{1}", Constants.CultureInfo.CultureKey, moudelname);
            var managerDa = new LanguageContentDA(connectionHandler);
            var list = managerDa.GetByMoudelname(key, culture);
            var assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly()
                   .EscapedCodeBase.ToLower()
                   .Replace("common", moudelname.ToLower()));
            if (assembly != null)
            {
                var firstOrDefault =
                    assembly.GetTypes().FirstOrDefault(x => x.FullName.ToLower().Contains("resources." + moudelname.ToLower()));
                if (firstOrDefault != null)
                {
                    var resourceManager = new System.Resources.ResourceManager(firstOrDefault);
                    var cultureInfo = CultureInfo.CreateSpecificCulture(culture);
                    var set = resourceManager.GetResourceSet(cultureInfo, true, true);
                    foreach (DictionaryEntry de in set)
                    {
                        var format = string.Format("{0}{1}.{2}", Constants.CultureInfo.CultureKey, moudelname, de.Key);
                        if (list.Any(x => x.Key == format))
                            continue;
                        list.Add(new LanguageContent()
                        {
                            Key = format,
                            Value = de.Value.ToString(),
                            LanguageId = culture,

                        });
                    }
                }
            }
            return list;
        }
      

        public override bool Update(IConnectionHandler connectionHandler, LanguageContent obj)
        {
            ResourceCacheManager.ResourceCache.RemoveItem(obj.Key, obj.LanguageId);
            return base.Update(connectionHandler, obj);
        }
    }
}
