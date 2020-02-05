using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Radyn.ContentManager.BO;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Definition;
using Radyn.ContentManager.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;

namespace Radyn.ContentManager.Facade
{
    internal sealed class PartialLoadFacade : ContentManagerBaseFacade<PartialLoad>, IPartialLoadFacade
    {
        internal PartialLoadFacade()
        {
        }

        internal PartialLoadFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }




        public bool Insert(PartialLoad partialLoad, string partialPostion)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var partialLoadBo = new PartialLoadBO();
                if (!string.IsNullOrEmpty(partialPostion))
                {
                    var load = partialLoadBo.Get(this.ConnectionHandler, partialPostion, partialLoad.CustomId,
                        partialLoad.HtmlDesginId);
                    if (load != null)
                    {

                        if (partialLoad.position == (byte)Enums.UiPartialEnums.Down)
                            partialLoad.position = (byte)(load.position + 1);
                        else
                            partialLoad.position = (byte)(load.position > 1 ? load.position - 1 : 1);
                        var list = partialLoadBo.GetForUpdatePostion(this.ConnectionHandler, partialLoad,
                            partialLoad.position);
                        foreach (var partialLoad1 in list)
                        {
                            partialLoad1.position++;
                            if (!partialLoadBo.Update(this.ConnectionHandler, partialLoad1))
                                throw new Exception("خطایی در ویرایش پارشیال وجود دارد");
                        }
                    }
                }
                else
                    partialLoad.position = 1;
                if (!partialLoadBo.Insert(this.ConnectionHandler, partialLoad))
                    throw new Exception("خطایی در ذخیره پارشیال وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Insert(string partialId, string customId, Guid htmlId, int? position)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var partialLoadBo = new PartialLoadBO();
                if (!partialLoadBo.Insert(this.ConnectionHandler, partialId, customId, htmlId, position))
                    throw new Exception("خطایی در ذخیره پارشیال وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Insert(string partialId, string customId, int? position,HtmlDesgin html)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var partialLoadBo = new PartialLoadBO();
                if (!partialLoadBo.Insert(this.ConnectionHandler, partialId, customId, position, html))
                    throw new Exception("خطایی در ذخیره پارشیال وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
        public bool DeletePartial(string partialId, string customId, Guid htmlId)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var partialLoadBo = new PartialLoadBO();
                if (!partialLoadBo.DeletePartial(this.ConnectionHandler, partialId, customId, htmlId))
                    throw new Exception("خطایی در ذخیره پارشیال وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool SwapPartials(string partialId, string customId, Guid htmlId, string type)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var partialLoadBo = new PartialLoadBO();
                if (!partialLoadBo.SwapPartials(this.ConnectionHandler, partialId, customId, htmlId, type))
                    throw new Exception("خطایی در ذخیره پارشیال وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool CustomeSwap(string partialId, string customId, Guid htmlId, int getorder)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var partialLoadBo = new PartialLoadBO();
                if (!partialLoadBo.CustomeSwap(this.ConnectionHandler, partialId, customId, htmlId, getorder))
                    throw new Exception("خطایی در ذخیره پارشیال وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }


        public List<PartialLoad> GetHtmlPartials(Guid htmlId, string culture, Container DefaultContrainer = null)
        {
            try
            {

                var list = new PartialLoadBO().Where(this.ConnectionHandler, load => load.HtmlDesginId == htmlId);
                var partialModels = new List<PartialLoad>();
                var contentBo = new ContentBO();
                var contentContentBo = new ContentContentBO();
                var partialsBo = new PartialsBO();
                var contenetidlist = list.Where(partialLoad => partialLoad.PartialId.ToInt() > 0).Select(x => x.PartialId.ToInt());
                var partialidlist = list.Where(partialLoad => partialLoad.PartialId.ToGuid() != Guid.Empty).Select(x => x.PartialId.ToGuid());
                var contents = new List<Content>();
                var ContentContents = new List<ContentContent>();
                var partialses = new List<Partials>();
                if (contenetidlist.Any())
                {
                    contents = contentBo.Where(ConnectionHandler, x => x.Id.In(contenetidlist));
                    ContentContents = contentContentBo.Where(ConnectionHandler, x => x.Id.In(contenetidlist) && x.LanguageId == culture);
                }

                if (partialidlist.Any())
                    partialses = partialsBo.Where(ConnectionHandler, x => x.Id.In(partialidlist));
                foreach (var partialLoad in list)
                {
                    var partialModel = new PartialLoad();
                    if (partialLoad.PartialId.ToInt() > 0)
                    {
                        int Id = partialLoad.PartialId.ToInt();
                        var content = contents.FirstOrDefault(x => x.Id == Id);
                        if (content == null) continue;
                        var contentContent = ContentContents.FirstOrDefault(x => x.Id == Id) ?? new ContentContent
                        {
                            Id = content.Id,
                            Abstract = content.Abstract,
                            Description = content.Description,
                            Text = content.Text,
                            Subject = content.Subject,
                            Title = content.Title,

                        };
                        partialModel.Content = content;
                        partialModel.PartialId = partialLoad.PartialId;
                        partialModel.Type = Enums.PartialTypes.ContentManager;
                        partialModel.Html = contentBo.GetHtml(content, contentContent, partialLoad.HasContainer, DefaultContrainer);
                    }

                    else
                    {
                        var Id = partialLoad.PartialId.ToGuid();
                        var partials = partialses.FirstOrDefault(x => x.Id == Id);
                        if (partials == null) continue;
                        partialModel.Partials = partials;
                        partialModel.PartialId = partialLoad.PartialId;
                        partialModel.Type = Enums.PartialTypes.Modual;
                    }
                    partialModel.CustomId = partialLoad.CustomId;
                    partialModel.position = partialLoad.position;
                    partialModel.HasContainer = partialLoad.HasContainer;
                    partialModel.HtmlDesginId = htmlId;
                    partialModels.Add(partialModel);
                }
                return partialModels;
            }
            catch (KnownException ex)
            {
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }


    }
}
