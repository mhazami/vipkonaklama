using Radyn.PaymentGateway.Facade;
using Radyn.PaymentGateway.Facade.Interface;

namespace Radyn.PaymentGateway
{
    public class PaymentGatewayComponenets
    {
        internal PaymentGatewayComponenets()
        {

        }
        private static PaymentGatewayComponenets _instance;
        public static PaymentGatewayComponenets Instance
        {
            get { return _instance ?? new PaymentGatewayComponenets(); }
        }

        public IGeneralFacade GeneralFacade
        {
            get { return new GeneralFacade(); }
        }
        public IMelliFacade MelliFacade
        {
            get { return new MelliFacade(); }
        }
        public ISaderatFacade SaderatFacade
        {
            get { return new SaderatFacade(); }
        }
        public ISamanFacade SamanFacade
        {
            get { return new SamanFacade(); }
        }
        public IMallatFacade MallatFacade
        {
            get { return new MellatFacade(); }
        }
        public IPasargadFacade PasargadFacade
        {
            get { return new PasargadFacade(); }
        }
        public IEghtesadeNovinFacade EghtesadeNovinFacade
        {
            get { return new EghtesadeNovinFacade(); }
        }
        public IIranKishFacade IranKishFacade
        {
            get { return new IranKishFacade(); }
        }
        public IMabnaFacade MabnaFacade
        {
            get { return new MabnaFacade(); }
        }

        public IGhavaminFacade GhavaminFacade
        {
            get { return new GhavaminFacade(); }
        }

        public IZarinPalFacade ZarinPalFacade
        {
            get { return new ZarinPalFacade(); }
        }

        public IMelliApiFacade MelliApiFacade
        {
            get { return new MelliApiFacade(); }
        }
    }
}
