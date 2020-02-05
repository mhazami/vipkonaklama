using Radyn.Framework;
using Radyn.Slider.DataStructure;

namespace Radyn.Slider.Facade.Interface
{
public interface ISlideFacade : IBaseFacade<Slide>
{
   
    Slide GetSlideWithSliders(short slideId);
}
}
