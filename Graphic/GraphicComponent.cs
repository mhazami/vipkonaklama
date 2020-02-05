using Radyn.Graphic.Facade;
using Radyn.Graphic.Facade.Interface;

namespace Radyn.Graphic
{
    public class GraphicComponent
    {
        private GraphicComponent()
        {

        }

      
        public static GraphicComponent Instance
        {
            get { return new GraphicComponent(); }
        }
        public IResourceFacade ResourceFacade
        {
            get { return new ResourceFacade(); }
        }

        public IThemeFacade ThemeFacade
        {
            get { return new ThemeFacade(); }
        }
    }
}
