using System;
using Radyn.Advertisements.DataStructure;
using Radyn.Advertisements.Facade;
using Radyn.Utility;

namespace Radyn.Advertisements.AdvetisementQueue
{
    public static class AdvertisementQueue
    {
        public static System.Collections.Queue Queue
        {
            get
            {
                return (System.Collections.Queue)Web.HttpContext.Current.Session["Queue"];
            }
            set
            {
                Web.HttpContext.Current.Session["Queue"] = value;
            }
        }

        public static System.Collections.Queue GetViewAbleAdvertisements(SectionPosition sectionPosition)
        {

            var Today = DateTime.Now.ShamsiDate();
            var list = new System.Collections.Queue();
            var advertisements =
                new AdvertisementFacade().GetAllByDate(sectionPosition.Id, Today);
            foreach (var item in advertisements)
            {
                list.Enqueue(item);
            }
            return list;
        }
        public static Advertisement POP(SectionPosition advertisementSectionPosition)
        {
            if (Queue != null && Queue.Count > 0)
                return (Advertisement)Queue.Dequeue();
            Queue = GetViewAbleAdvertisements(advertisementSectionPosition);
            return Queue.Count == 0 ? null : POP(advertisementSectionPosition);
        }
    }
}
