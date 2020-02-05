using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Slider.BO;
using Radyn.Slider.DataStructure;
using Radyn.Slider.Facade.Interface;
using Radyn.Utility;
using System;
using System.Collections.Generic;
using System.Data;

namespace Radyn.Slider.Facade
{
    internal sealed class SlideFacade : SliderBaseFacade<Slide>, ISlideFacade
    {
        internal SlideFacade()
        {
        }

        internal SlideFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

        public override bool Delete(params object[] keys)
        {
            try
            {

                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                SlideBO slideBo = new SlideBO();
                Slide obj = slideBo.Get(ConnectionHandler, keys);
                SlideItemBO slideItemFacade = new SlideItemBO();

                List<SlideItem> list = new SlideItemBO().Where(ConnectionHandler,
                    supporter => supporter.SlideId == obj.Id);
                foreach (SlideItem slideItem in list)
                {
                    if (
                          !slideItemFacade.Delete(ConnectionHandler, FileManagerConnection, slideItem.Id))
                    {
                        throw new Exception("خطایی در ");
                    }
                }

                if (!slideBo.Delete(ConnectionHandler, keys))
                {
                    throw new Exception("خطایی در حذف اسلاید وجود دارد");
                }

                ConnectionHandler.CommitTransaction();
                FileManagerConnection.CommitTransaction();
                return true;

            }

            catch (KnownException ex)
            {
                ConnectionHandler.RollBack();
                FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                ConnectionHandler.RollBack();
                FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }






        public Slide GetSlideWithSliders(short slideId)
        {
            try
            {
                Slide slide = new SlideBO().Get(ConnectionHandler, slideId);
                if (slide == null || !slide.Enabled)
                {
                    return null;
                }

                slide.SlideItems = new SlideItemBO().OrderBy(ConnectionHandler, x => x.Order, x =>

                    x.SlideId == slideId &&
                    x.Enabled && ((string.IsNullOrEmpty(x.StartDate)&&string.IsNullOrEmpty(x.FinishDate))||
                    (x.StartDate.CompareTo(DateTime.Now.ShamsiDate()) <= 0 &&
                    x.FinishDate.CompareTo(DateTime.Now.ShamsiDate()) >= 0)));

                if (string.IsNullOrEmpty(slide.EffectType))
                {
                    slide.EffectType = Definition.Enums.SliderCycleFxType.blindX.ToString();
                }

                return slide;
            }
            catch (KnownException ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
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
