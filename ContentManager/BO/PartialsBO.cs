using System;
using System.Collections.Generic;
using System.Linq;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Definition;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;

namespace Radyn.ContentManager.BO
{
    internal class PartialsBO : BusinessBase<Partials>
    {

        public override bool Delete(IConnectionHandler connectionHandler, params object[] keys)
        {

            var partials = this.Get(connectionHandler, keys);
            var partialLoadBo = new PartialLoadBO();
            var byFilter = partialLoadBo.Where(connectionHandler, load => load.PartialId == partials.Id.ToString());
            foreach (var partialLoad in byFilter)
            {
                if (!partialLoadBo.Delete(connectionHandler, partialLoad.PartialId, partialLoad.CustomId, partialLoad.HtmlDesginId))
                    throw new Exception("خطایی در حذف صفحه وجود دارد");
            }

            if (!base.Delete(connectionHandler, keys))
                throw new Exception("خطایی در حذف صفحه وجود دارد");
            return true;
        }

        public List<Partials> GetContentPartials(IConnectionHandler connectionHandler, List<Content> contents,string culture)
        {
            var contentContentBo = new ContentContentBO();
            var contentBo = new ContentBO();
            var partialModels = new List<Partials>();
            var ContentContents = new List<ContentContent>();
            if (contents.Any())
                ContentContents = contentContentBo.Where(connectionHandler,
                    x => x.Id.In(contents.Select(i => i.Id)) && x.LanguageId == culture);

            foreach (var content in contents)
            {
                var contentContent = ContentContents.FirstOrDefault(x => x.Id == content.Id) ?? new ContentContent
                {
                    Id = content.Id,
                    Abstract = content.Abstract,
                    Description = content.Description,
                    Text = content.Text,
                    Subject = content.Subject,
                    Title = content.Title,
                };
                var partialModel = new Partials
                {
                    StringId = content.Id.ToString(),
                    Title = content.Title,
                    Type = Enums.PartialTypes.ContentManager,
                    Html = contentBo.GetHtml(content, contentContent)
                };
                partialModels.Add(partialModel);
            }

            return partialModels;
        }
        public override bool Insert(IConnectionHandler connectionHandler, Partials obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }

        public List<Partials> GetOperationPartials(IConnectionHandler connectionHandler,  Guid OperationId)
        {
            var partialModels = new List<Partials>();
            var dBpartialModels = new PartialsBO().Where(connectionHandler, x => x.OperationId == OperationId);
            foreach (var partials in dBpartialModels)
            {
                partials.StringId = partials.Id.ToString();
                partials.Title = partials.Title;
                partials.Type = Enums.PartialTypes.Modual;
                partialModels.Add(partials);
            }
            return partialModels;

        }
        
     
    
      

    }
}
