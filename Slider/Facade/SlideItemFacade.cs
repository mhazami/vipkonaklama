using System;
using System.Data;
using System.Web;
using Radyn.FileManager;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Slider.BO;
using Radyn.Slider.DataStructure;
using Radyn.Slider.Facade.Interface;
using Radyn.Utility;

namespace Radyn.Slider.Facade
{
    internal sealed class SlideItemFacade : SliderBaseFacade<SlideItem>, ISlideItemFacade
    {

        internal SlideItemFacade()
        {
        }

        internal SlideItemFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

       
        public bool Update(SlideItem slideItem,  HttpPostedFileBase image)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.CommonConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);

                if (image != null)
                {
                    var fileTransactionalFacade = FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection);
                    var lanuageContent=new SlideItemBO().GetLanuageContent(base.ConnectionHandler,
                        slideItem.CurrentUICultureName, slideItem.Id);
                    if (!string.IsNullOrEmpty(lanuageContent.ImageId))
                    {
                        fileTransactionalFacade
                            .Update(image, lanuageContent.ImageId.ToGuid());
                    }
                    else
                    {
                        slideItem.ImageId =
                    fileTransactionalFacade
                        .Insert(image, new File() { MaxSize = 300 }).ToString();

                    }
                }
               
                if (!new SlideItemBO().Update(this.ConnectionHandler, slideItem))
                    throw new Exception("خطایی در ذخیره اسلاید  آیتم وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.CommonConnection.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.CommonConnection.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.CommonConnection.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
               this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var slideItemBo = new SlideItemBO();
                if (!slideItemBo.Delete(this.ConnectionHandler, this.FileManagerConnection,  keys))
                    throw new Exception("خطایی در حذف اسلاید  آیتم وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Insert(SlideItem slideItem, HttpPostedFileBase image)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.CommonConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (image != null)
                {

                    slideItem.ImageId =
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                            .Insert(image, new File() { MaxSize = 300 }).ToString();
                }

                if (!new SlideItemBO().Insert(this.ConnectionHandler, slideItem))
                    throw new Exception("خطایی در ذخیره اسلاید  آیتم وجود دارد");

                this.ConnectionHandler.CommitTransaction();
                this.CommonConnection.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.CommonConnection.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.CommonConnection.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
