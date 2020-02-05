using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.PhoneBook.Security.Filter
{
    [PhoneBookHost]
    public class PhoneBookBaseController : WebDesignBaseController
    {

      

    }
    [PhoneBookHost]
    public class PhoneBookBaseController<T> : WebDesignBaseController<T> where T : class
    {

        

    }
}