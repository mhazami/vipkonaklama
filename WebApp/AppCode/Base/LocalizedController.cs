using Radyn.WebApp.AppCode.Filter;

namespace Radyn.WebApp.AppCode.Base
{
    [Localization]
    public class LocalizedController : BaseController
    {
       
    }
    [Localization]
    public class LocalizedController<T> : BaseController<T> where T : class
    {

    }
}