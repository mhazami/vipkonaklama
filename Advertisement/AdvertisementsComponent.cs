using Radyn.Advertisements.Facade;
using Radyn.Advertisements.Facade.Interface;

namespace Radyn.Advertisements
{
    public sealed class AdvertisementsComponent
    {
        private AdvertisementsComponent()
        {

        }

        private static AdvertisementsComponent _instance;
        public static AdvertisementsComponent Instance
        {
            get { return _instance ?? (_instance = new AdvertisementsComponent()); }
        }

        public IAdvertisementFacade AdvertisementFacade
        {
            get
            {
                return new AdvertisementFacade();
            }
        }


        public ISectionFacade AdvertisementSectionFacade
        {
            get
            {
                return new SectionFacade();
            }
        }


        public ISectionPositionFacade AdvertisementSectionPositionFacade
        {
            get
            {
                return new SectionPositionFacade();
            }
        }


        public IAdvertisementTypeFacade AdvertisementTypeFacade
        {
            get
            {
                return new AdvertisementTypeFacade();
            }
        }


        public ITariffFacade TariffFacade
        {
            get
            {
                return new TariffFacade();
            }
        }


        public ITariffClassFacade TariffClassFacade
        {
            get
            {
                return new TariffClassFacade();
            }
        }
    }
}
