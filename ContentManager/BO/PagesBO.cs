using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System;

namespace Radyn.ContentManager.BO
{
    public class PagesBO : BusinessBase<Pages>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Pages obj)
        {
            if (!base.Insert(connectionHandler, obj))
                throw new Exception("خطا در ذخیره سازی اطلاعات");
            obj.Url = $"{obj.Url}/Pages/{obj.Id}/{obj.Title}";
            return base.Update(connectionHandler, obj);

        }
        public bool Insert(IConnectionHandler connectionHandler, Pages obj, HtmlDesgin htmlDesgin)
        {
            htmlDesgin.Title = obj.Title;
            if (!new HtmlDesginBO().Insert(connectionHandler, htmlDesgin))
                throw new Exception("خطا در ذخیره سازی اطلاعات");
            obj.HtmlId = htmlDesgin.Id;
            if (!this.Insert(connectionHandler, obj))
                throw new Exception("خطا در ذخیره سازی اطلاعات");
            return true;

        }
        public bool Update(IConnectionHandler connectionHandler, Pages obj, HtmlDesgin htmlDesgin)
        {
            htmlDesgin.Title = obj.Title;
            htmlDesgin.Id = obj.HtmlId;
            new HtmlDesginBO().Update(connectionHandler, htmlDesgin);
            return new PagesBO().Update(connectionHandler, obj);
              

        }

        public override bool Delete(IConnectionHandler connectionHandler, Pages obj)
        {

            base.Delete(connectionHandler, obj);
            if (!new HtmlDesginBO().Delete(connectionHandler, obj.HtmlDesgin))
            {
                throw new Exception("خطا در حذف اطلاعات");
            }

            return true;
        }

        protected override void CheckConstraint(IConnectionHandler connectionHandler, Pages item)
        {
            if (string.IsNullOrEmpty(item.Title))
            {
                throw new Exception("لطفا عنوان صفحه را وارد کنید");
            }

        }
    }
}
