using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using Radyn.Advertisements.AdvetisementQueue;
using Radyn.Advertisements.BO;
using Radyn.Advertisements.DataStructure;
using Radyn.Advertisements.Facade.Interface;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;

namespace Radyn.Advertisements.Facade
{
    internal sealed class AdvertisementFacade : AdvertisementsBaseFacade<Advertisement>, IAdvertisementFacade
    {
        internal AdvertisementFacade()
        {
        }

        internal AdvertisementFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

        

        public IEnumerable<Advertisement> GetAllByDate(int sectionPosition, string date)
        {
            try
            {
                var allByDate = new AdvertisementBO().GetAllByDate(this.ConnectionHandler, sectionPosition, date);
                
                return allByDate;
            }

            catch (KnownException ex)
             {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
             }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Insert(Advertisement obj, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (file != null)
                    obj.FileId =
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection).Insert(file);
                if (!new AdvertisementBO().Insert(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ذخیره آگهی وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Update(Advertisement obj, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (file != null)
                {
                    if (obj.FileId.HasValue)
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                            .Update(file, (Guid)obj.FileId);
                    else
                        obj.FileId =
                            FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                                .Insert(file);
                }
                if (!new AdvertisementBO().Update(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ویرایش آگهی وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
       
        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var obj = new AdvertisementBO().Get(this.ConnectionHandler, keys);
                if (!new AdvertisementBO().Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف آگهی وجود دارد");
                if (obj.FileId.HasValue)
                {
                    if (
                        !FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                            .Delete(obj.FileId))
                        throw new Exception("خطایی در حذف عکس  آگهی وجود دارد");
                }
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
      
        public string GetHtml4Position(string keyword)
        {
            try
            {
                var sectionPosition = new SectionPositionBO().GetByKeyWord(this.ConnectionHandler, keyword);
                //انتخاب تبلیغ های معتبر در  موقعیت 
                //ریختن لیست در صف
                if (sectionPosition != null)
                {
                    AdvertisementQueue.Queue = AdvertisementQueue.GetViewAbleAdvertisements(sectionPosition);

                    var advs = new List<Advertisement>();
                    for (var index = 1; index <= sectionPosition.AdsShowCount; index++)
                    {
                        var obj = AdvertisementQueue.POP(sectionPosition);
                        if (obj != null)
                            advs.Add(obj);
                        if (AdvertisementQueue.Queue.Count == 0) break;
                    }

                    var stringWriter = new StringWriter();
                    var writer = new Html32TextWriter(stringWriter);

                    writer.AddAttribute("src", "/Areas/Advertisements/Content/Advertisment.js");
                    writer.AddAttribute("Type", "text/javascript");
                    writer.RenderBeginTag(HtmlTextWriterTag.Script);
                    writer.RenderEndTag();
                    if (!sectionPosition.Orientation)
                    {
                        writer.AddAttribute("id", sectionPosition.KeyWord);
                        writer.AddAttribute("align", "center");
                        writer.RenderBeginTag(HtmlTextWriterTag.Table);
                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    }

                    foreach (var advertisement in advs)
                    {

                        writer.AddAttribute("onclick",
                                            string.Format("OnAdvClick('{0}');",
                                                          advertisement.Id));
                        if (!sectionPosition.Orientation)
                        {
                            writer.AddAttribute("Id", (advs.IndexOf(advertisement) + 1).ToString());
                            writer.AddAttribute("width", advertisement.width.ToString());
                            writer.AddAttribute("align", "center");
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        }
                        else
                        {
                            writer.AddAttribute("Id", (advs.IndexOf(advertisement) + 1).ToString());
                            writer.RenderBeginTag(HtmlTextWriterTag.Div);
                        }
                        if (advertisement.Timeout > 0)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
                            writer.RenderBeginTag(HtmlTextWriterTag.Script);
                            writer.WriteEncodedText(string.Format("SetInterval('{0}',{1},'{2}');",
                                                                  (advs.IndexOf(advertisement) + 1), advertisement.Timeout,
                                                                  keyword));
                            writer.RenderEndTag();
                        }
                        //تولید قالب تبلیغات بر حسب نوع آن
                        switch (advertisement.AdvertisementType.Type)
                        {
                            case "Text":

                                #region TEXT

                                if (string.IsNullOrEmpty(advertisement.NavigateUrl))
                                    writer.WriteEncodedText(advertisement.Text);
                                else
                                {
                                    writer.AddAttribute("href", advertisement.NavigateUrl);
                                    writer.AddAttribute("target", "_blank");
                                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                                    writer.WriteEncodedText(Utils.ConvertHtmlToString(advertisement.Text));
                                    writer.RenderEndTag();
                                }
                                break;

                                #endregion

                            case "SWF":

                                #region FLASH

                                writer.AddAttribute("codeBase",
                                                    "https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7.0.19.0");
                                writer.AddAttribute("classid", "clsid:D27CDB6E-AE6D-11cf-96B8-444553540000");
                                writer.AddAttribute("width", advertisement.width.ToString());
                                writer.AddAttribute("height", advertisement.Height.ToString());
                                writer.RenderBeginTag(HtmlTextWriterTag.Object);


                                writer.AddAttribute("NAME", "Movie");
                                writer.AddAttribute("VALUE", FileManagerContants.FileHandlerRoot + advertisement.FileId);
                                writer.RenderBeginTag(HtmlTextWriterTag.Param);

                                writer.AddAttribute("src", FileManagerContants.FileHandlerRoot + advertisement.FileId);
                                writer.AddAttribute("quality", "high");
                                writer.AddAttribute("wmode", "opaque");
                                writer.AddAttribute("pluginspage", "https://www.macromedia.com/go/getflashplayer");
                                writer.AddAttribute("type", "application/x-shockwave-flash");
                                writer.AddAttribute("width", advertisement.width.ToString());
                                writer.AddAttribute("height", advertisement.Height.ToString());
                                writer.RenderBeginTag(HtmlTextWriterTag.Embed);

                                writer.RenderEndTag();
                                writer.RenderEndTag();
                                writer.RenderEndTag();
                                break;

                                #endregion

                            case "Html":

                                #region HTML

                                writer.Write(Utils.ConvertHtmlToString(advertisement.Text));
                                break;

                                #endregion

                            case "Image":

                                #region IMAGE

                                if (!string.IsNullOrEmpty(advertisement.NavigateUrl))
                                {
                                    writer.AddAttribute("href", advertisement.NavigateUrl);
                                    writer.AddAttribute("target", "_blank");
                                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                                }
                                writer.AddAttribute("src", FileManagerContants.FileHandlerRoot + advertisement.FileId);
                                writer.AddAttribute("border", "0");
                                writer.AddAttribute("height", advertisement.Height.ToString());
                                writer.AddAttribute("width", advertisement.width.ToString());
                                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                                writer.RenderEndTag();
                                if (!string.IsNullOrEmpty(advertisement.NavigateUrl))
                                    writer.RenderEndTag();
                                break;

                                #endregion
                        }
                        //یک واحد به تعداد نمایش تبلیغ اضافه می شود
                        advertisement.VisitCount++;
                        writer.RenderEndTag();
                        if (!new AdvertisementBO().Update(this.ConnectionHandler, advertisement))
                            throw new Exception("خطایی در ویرایش آگهی وجود دارد");
                    }

                    if (!sectionPosition.Orientation)
                    {
                        writer.RenderEndTag();
                        writer.RenderEndTag();
                    }

                    var str = stringWriter.ToString();
                    return System.Net.WebUtility.HtmlDecode(str);
                }
                return "No Advertisment Mached With Your Keyword";
            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public string GetNewAdvertisement(string positionId, string sPkeyword)
        {
            try
            {
                var stringWriter = new StringWriter();
                var writer = new Html32TextWriter(stringWriter);
                //دریافت تبلیغ جدید از صف تبلیغات قابل اجرا
                var sectionPosition = new SectionPositionBO().GetByKeyWord(this.ConnectionHandler,
                    sPkeyword);
                if (sectionPosition != null)
                {
                    var advertisement = AdvertisementQueue.POP(sectionPosition);

                    if (advertisement.Timeout >= 0)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
                        writer.RenderBeginTag(HtmlTextWriterTag.Script);
                        writer.WriteEncodedText(string.Format("SetInterval('{0}',{1},'{2}');", positionId,
                                                              advertisement.Timeout, sectionPosition.KeyWord));
                        writer.RenderEndTag();
                    }
                    writer.AddAttribute("onclick", string.Format("OnAdvClick('{0}');", advertisement.Id));
                    //تولید قالب تبلیغات بر حسب نوع آن
                    switch (advertisement.AdvertisementType.Type)
                    {
                        case "Text":

                            #region TEXT

                            if (string.IsNullOrEmpty(advertisement.NavigateUrl))
                                writer.WriteEncodedText(advertisement.Text);
                            else
                            {
                                writer.AddAttribute("href", advertisement.NavigateUrl);
                                writer.AddAttribute("target", "_blank");
                                writer.RenderBeginTag(HtmlTextWriterTag.A);
                                writer.WriteEncodedText(Utils.ConvertHtmlToString(advertisement.Text));
                                writer.RenderEndTag();
                            }
                            break;

                            #endregion

                        case "Flash":

                            #region FLASH

                            writer.AddAttribute("codeBase",
                                                "https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7.0.19.0");
                            writer.AddAttribute("classid", "clsid:D27CDB6E-AE6D-11cf-96B8-444553540000");
                            writer.AddAttribute("width", advertisement.width.ToString());
                            writer.AddAttribute("height", advertisement.Height.ToString());
                            writer.RenderBeginTag(HtmlTextWriterTag.Object);


                            writer.AddAttribute("NAME", "Movie");
                            writer.AddAttribute("VALUE", FileManagerContants.FileHandlerRoot + advertisement.FileId);
                            writer.RenderBeginTag(HtmlTextWriterTag.Param);

                            writer.AddAttribute("src", FileManagerContants.FileHandlerRoot + advertisement.FileId);
                            writer.AddAttribute("quality", "high");
                            writer.AddAttribute("wmode", "opaque");
                            writer.AddAttribute("pluginspage", "https://www.macromedia.com/go/getflashplayer");
                            writer.AddAttribute("type", "application/x-shockwave-flash");
                            writer.AddAttribute("width", advertisement.width.ToString());
                            writer.AddAttribute("height", advertisement.Height.ToString());
                            writer.RenderBeginTag(HtmlTextWriterTag.Embed);

                            writer.RenderEndTag();
                            writer.RenderEndTag();
                            writer.RenderEndTag();
                            break;

                            #endregion

                        case "Html":

                            #region HTML

                            writer.Write(Utils.ConvertHtmlToString(advertisement.Text));
                            break;

                            #endregion

                        case "Image":

                            #region IMAGE

                            if (!string.IsNullOrEmpty(advertisement.NavigateUrl))
                            {
                                writer.AddAttribute("href", advertisement.NavigateUrl);
                                writer.AddAttribute("target", "_blank");
                                writer.RenderBeginTag(HtmlTextWriterTag.A);
                            }
                            writer.AddAttribute("src", FileManagerContants.FileHandlerRoot + advertisement.FileId);
                            writer.AddAttribute("border", "0");
                            writer.AddAttribute("height", advertisement.Height.ToString());
                            writer.AddAttribute("width", advertisement.width.ToString());
                            writer.RenderBeginTag(HtmlTextWriterTag.Img);
                            writer.RenderEndTag();
                            if (!string.IsNullOrEmpty(advertisement.NavigateUrl))
                                writer.RenderEndTag();
                            break;

                            #endregion
                    }
                    //یک واحد به تعداد نمایش تبلیغ اضافه می شود
                    advertisement.VisitCount++;
                    if (!new AdvertisementBO().Update(this.ConnectionHandler, advertisement))
                        throw new Exception("خطایی در ویرایش آگهی وجود دارد");
                    var str = stringWriter.ToString();
                    return System.Net.WebUtility.HtmlDecode(str);
                }
                return "No Advertisment Mached With Your Keyword";
            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool AdsAddClickCount(Guid AdsId)
        {
            try
            {
                var advertisement = new AdvertisementBO().Get(this.ConnectionHandler, AdsId);
                advertisement.ClickCount++;
                return new AdvertisementBO().Update(this.ConnectionHandler, advertisement);
            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
