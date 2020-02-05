using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Graphic.BO;
using Radyn.Graphic.DataStructure;
using Radyn.Graphic.Definition;
using Radyn.Graphic.Facade.Interface;

namespace Radyn.Graphic.Facade
{
    internal sealed class ResourceFacade : GraphicBaseFacade<Resource>, IResourceFacade
    {
        internal ResourceFacade() { }

        internal ResourceFacade(IConnectionHandler connectionHandler)
        : base(connectionHandler)
        { }

     
        public string GetThemeResourceHtml(Guid themeId, string culture)
        {
            try
            {
                var str = new StringWriter();
                var html32TextWriter = new Html32TextWriter(str);
                var byFilter = new ResourceBO().OrderBy(this.ConnectionHandler,x=>x.Order, x =>
                
                    x.ThemeId == themeId&&
                    x.LanuageId == culture
                );
                foreach (var resource in byFilter)
                {
                    switch ((Enums.ResourceType)resource.Type)
                    {
                        case Enums.ResourceType.JSFile:
                            if (resource.FileId == null) continue;
                            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
                            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Src, FileManagerContants.FileHandlerRoot + resource.FileId);
                            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Script);
                            html32TextWriter.RenderEndTag();
                            break;
                        case Enums.ResourceType.CssFile:
                            if (resource.FileId == null) continue;
                            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
                            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Rel, "stylesheet");
                            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Href, FileManagerContants.FileHandlerRoot + resource.FileId);
                            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Link);
                            html32TextWriter.RenderEndTag();
                            break;
                        case Enums.ResourceType.JSFunction:
                            if (string.IsNullOrEmpty(resource.Text)) continue;
                            html32TextWriter.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
                            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Style);
                            html32TextWriter.Write(resource.Text);
                            html32TextWriter.RenderEndTag();
                            break;
                        case Enums.ResourceType.Style:
                            if (string.IsNullOrEmpty(resource.Text)) continue;
                            html32TextWriter.RenderBeginTag(HtmlTextWriterTag.Style);
                            html32TextWriter.Write(resource.Text);
                            html32TextWriter.RenderEndTag();
                            break;
                    }
                }
                return str.ToString();
            }
            catch (KnownException knownException)
            {
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                throw new KnownException(ex.Message, ex);
            }
        }
        public bool Insert(Resource resource, HttpPostedFileBase resourceFile)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (resourceFile != null)
                {
                    resource.FileId = FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection).Insert(resourceFile);
                }

                if (!new ResourceBO().Insert(this.ConnectionHandler, resource))
                    throw new Exception("  خطایی در ذخیره resource  وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var obj = new ResourceBO().Get(this.ConnectionHandler, keys);
                if (obj.FileId != null)
                {
                    FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection).Delete(obj.FileId);

                }
                if (!new ResourceBO().Delete(this.ConnectionHandler, keys))
                    throw new Exception("  خطایی در حذف resource  وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Update(Resource resource, HttpPostedFileBase resourceFile)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (resourceFile != null)
                {
                    if (resource.FileId.HasValue)
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection).Update(resourceFile, (Guid)resource.FileId);
                    else
                        resource.FileId = FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection).Insert(resourceFile);
                }
                if (!new ResourceBO().Update(this.ConnectionHandler, resource))
                    throw new Exception("  خطایی در ویرایش resource  وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
