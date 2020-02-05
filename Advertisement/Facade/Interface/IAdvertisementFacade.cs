using System;
using System.Web;
using Radyn.Advertisements.DataStructure;
using Radyn.Framework;

namespace Radyn.Advertisements.Facade.Interface
{
    public interface IAdvertisementFacade : IBaseFacade<Advertisement>
    {
        bool Insert(Advertisement obj, HttpPostedFileBase file);
        bool Update(Advertisement obj, HttpPostedFileBase file);
        string GetHtml4Position(string keyword);
        string GetNewAdvertisement(string positionId, string sPkeyword);
        bool AdsAddClickCount(Guid AdsId);
    }
}
