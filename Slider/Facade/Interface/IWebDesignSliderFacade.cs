using System;
using Radyn.Framework;
using Radyn.Slider.DataStructure;

namespace Radyn.Slider.Facade.Interface
{
public interface IWebDesignSliderFacade : IBaseFacade<DataStructure.WebDesignSlider>
{
    bool Insert(Guid websiteId, Slide slide, string url);
    bool Update(Guid websiteId, Slide slide, string url);

    }
}
