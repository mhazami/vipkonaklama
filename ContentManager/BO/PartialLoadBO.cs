using System;
using System.Collections.Generic;
using System.Linq;
using Radyn.ContentManager.DA;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager.BO
{
    internal class PartialLoadBO : BusinessBase<PartialLoad>
    {



        public IEnumerable<PartialLoad> GetForUpdatePostion(IConnectionHandler connectionHandler, PartialLoad partialLoad, byte position)
        {
            var partialLoadDa = new PartialLoadDA(connectionHandler);
            return partialLoadDa.GetForUpdatePostion(partialLoad, position);
        }

        public bool Insert(IConnectionHandler connectionHandler, string partialId, string customId, Guid htmlId, int? position)
        {
            var partialLoad = new PartialLoad
            {
                CustomId = customId,
                PartialId = partialId,
                HtmlDesginId = htmlId,

            };
            if (this.Any(connectionHandler, x => x.HtmlDesginId == htmlId && x.CustomId == customId && x.PartialId == partialId))
                throw new KnownException("این صفحه قبلا دراین ناحیه اضافه شده است");

            var controls = this.Where(connectionHandler, x => x.HtmlDesginId == htmlId && x.CustomId == customId);
            if (position == null)
            {
                var max = this.Max(connectionHandler, x => x.position, x => x.HtmlDesginId == htmlId && x.CustomId == customId);
                partialLoad.position = (byte)(max + 1);
            }
            else
            {
                partialLoad.position = (byte)(position + 1);
                var orDefault = controls.FirstOrDefault(control => control.position >= partialLoad.position);
                if (orDefault != null)
                {
                    foreach (var variable in controls.Where(control => control.position >= partialLoad.position))
                    {
                        variable.position++;
                        this.Update(connectionHandler, variable);
                    }
                }

            }


            if (!this.Insert(connectionHandler, partialLoad))
                throw new Exception("خطایی در ذخیره پارشیال وجود دارد");
            return true;
        }

        public bool Insert(IConnectionHandler connectionHandler, string partialId, string customId, int? position,HtmlDesgin html)
        {
            if (!new HtmlDesginBO().Update(connectionHandler,html))
            {
                throw new Exception("خطا در ذخیره سازی اطلاعات");
            }

            return this.Insert(connectionHandler, partialId, customId, html.Id, position);

        }

        public bool DeletePartial(IConnectionHandler connectionHandler, string partialId, string customId, Guid htmlId)
        {
            var partialLoad = this.Get(connectionHandler, partialId, customId, htmlId);
            if (partialLoad == null)
                return false;
            var controls = this.Where(connectionHandler, x => x.HtmlDesginId == htmlId && x.CustomId == customId);
            var order = partialLoad.position;
            foreach (var  variable in controls.Where(control => control.position> order))
            {
                variable.position--;
                this.Update(connectionHandler, variable);
            }
            if (!this.Delete(connectionHandler, partialLoad))
                throw new Exception("خطایی در ذخیره پارشیال وجود دارد");
            return true;
        }

        public bool CustomeSwap(IConnectionHandler connectionHandler, string partialId, string customId, Guid htmlId, int getorder)
        {
            var controls = this.Where(connectionHandler, x => x.HtmlDesginId == htmlId && x.CustomId == customId);
            var item = controls.FirstOrDefault(x => x.PartialId == partialId);
            if (item == null) return false;
           
            var orDefault = controls.FirstOrDefault(control1 => control1.position >= getorder);
            if (orDefault != null)
            {
                
                foreach (var  variable in controls.Where(control1 => control1.position >= getorder))
                {
                    variable.position++;
                    this.Update(connectionHandler, variable);
                }
                
               
            }
           
            return true;
        }

        public bool SwapPartials(IConnectionHandler connectionHandler, string partialId, string customId, Guid htmlId, string type)
        {
            var controls = this.Where(connectionHandler, x => x.HtmlDesginId == htmlId && x.CustomId == customId);
            var item = controls.FirstOrDefault(x=>x.PartialId==partialId);
            if (item == null) return false;
            switch (type)
            {
                case "Up":
                    var orDefaultdown =controls.OrderByDescending(x => x.position).FirstOrDefault(control => control.position < item.position);
                    if (orDefaultdown == null) return false;
                    var orderdown = item.position;
                    item.position = (orDefaultdown).position;
                    (orDefaultdown).position = orderdown;
                    this.Update(connectionHandler, item);
                    this.Update(connectionHandler, orDefaultdown);

                    break;
                case "Down":
                    var orDefault =
                        controls.FirstOrDefault(control => control.position> item.position);
                    if (orDefault == null) return false;
                    var order = item.position;
                    item.position = (orDefault).position;
                    (orDefault).position = order;
                    this.Update(connectionHandler, item);
                    this.Update(connectionHandler, orDefault);

                    break;
            }
            
            return true;
        }
    }
}
