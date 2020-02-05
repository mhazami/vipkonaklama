using Radyn.ContentManager.DataStructure;
using Radyn.Framework;

namespace Radyn.ContentManager.Facade.Interface
{
    public interface IPagesFacade : IBaseFacade<Pages>
    {
        bool Insert(Pages obj, HtmlDesgin htmlDesgin);

        bool Update(Pages obj, HtmlDesgin htmlDesgin);
    }
}
