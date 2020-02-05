using Radyn.Framework.DbHelper;
using Radyn.Slider.Facade;
using Radyn.Slider.Facade.Interface;

namespace Radyn.Slider
{
    public class SliderComponent
    {
        private SliderComponent()
        {

        }

        private static SliderComponent _instance;
        public static SliderComponent Instance
        {
            get { return _instance ?? (_instance = new SliderComponent()); }
        }

        
        public ISlideFacade SlideFacade
        {
            get { return new SlideFacade(); }
        }
        public ISlideFacade SlideTransactionalFacade(IConnectionHandler connectionHandler)
        {
            return new SlideFacade(connectionHandler);
        }

        public ISlideItemFacade SlideItemFacade
        {
            get { return new SlideItemFacade(); }
        }
        public ISlideItemFacade SlideItemTransactionalFacade(IConnectionHandler connectionHandler)
        {
          return new SlideItemFacade(connectionHandler); 
        }
        public IWebDesignSliderFacade WebDesignSliderFacade
        {
            get { return new WebDesignSliderFacade(); }
        }
    }
}
