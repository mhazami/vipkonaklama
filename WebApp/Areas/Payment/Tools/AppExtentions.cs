using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Tools;
using Radyn.Utility;

namespace Radyn.WebApp.Areas.Payment.Tools
{
    public static class AppExtentions
    {

        public static int GetUserTempCount(Guid userId)
        {
            return PaymentComponenets.Instance.TransactionFacade.GetUserTempCount(userId);
        }
        public static List<DiscountType> FillTransactionDiscount(FormCollection collection)
        {
            var transactionDiscountAttaches =
                   (List<DiscountType>)
                       System.Web.HttpContext.Current.Session[Constants.TransactionDiscountAttach];
            var enumerable = collection.AllKeys.FirstOrDefault(s => s.Equals("CheckSelectDiscount"));
            if (enumerable != null)
            {
                var strings = collection[enumerable].Split(',');
                foreach (var variable in strings)
                {
                    var find = transactionDiscountAttaches.Find(x => x.Id == variable.ToGuid());
                    if (find != null)
                        find.UserCode = collection["UserCode-" + variable];
                    else
                    {
                        var discountType = new DiscountType(){Id = variable.ToGuid(),UserCode =collection["UserCode-" + variable] };
                        transactionDiscountAttaches.Add(discountType);
                    }
                }
            }

            return transactionDiscountAttaches ?? new List<DiscountType>();
        }
       
        public static List<DiscountTypeAutoCode> FillDiscountAutoCode(DiscountType discountType, FormCollection collection)
        {
            if (!discountType.IsAutoCode) return null;

            var list = new List<DiscountTypeAutoCode>();
            var enumerable = collection.AllKeys.FirstOrDefault(s => s.Equals("AutoCodeModelId"));
            if (enumerable != null)
            {
                var strings = collection[enumerable].Split(',');
                foreach (var variable in strings)
                {
                    list.Add(new DiscountTypeAutoCode()
                    {
                        Id = variable.ToGuid(),
                        Code = collection["AutoCode-" + variable],
                        Used = (variable.ToGuid() != Guid.Empty && collection["Used-" + variable].ToBool()),
                        DiscountTypeId = collection["DiscountTypeId-" + variable].ToGuid()
                    });
                }

            }
            return list;
        }
        public static List<DiscountTypeSection> FillDiscountTypeSection(FormCollection collection, string modelName)
        {
            var sectiontypes = new List<DiscountTypeSection>();
            var enumerable = collection.AllKeys.FirstOrDefault(s => s.Equals("DiscountTypeId"));
            if (enumerable != null)
            {
                var strings = collection[enumerable].Split(',');
                foreach (var variable in strings)
                {
                    var firstOrDefault = collection.AllKeys.FirstOrDefault(s => s.Equals("Section-" + variable));
                    if (firstOrDefault != null)
                    {
                        var variableds = collection[firstOrDefault].Split(',');
                        foreach (var value in variableds)
                        {
                            var discountTypeSection = new DiscountTypeSection
                            {
                                Section = value.ToByte(),
                                MoudelName = modelName,
                                DiscountTypeId = variable.ToGuid()
                            };
                            sectiontypes.Add(discountTypeSection);
                        }

                    }
                }

            }
            return sectiontypes;

        }



        public static Dictionary<byte, string> GetBankWithImages(byte? bankId)
        {
            var dictionary = new Dictionary<byte, string>();
            if (bankId == null) return dictionary;
            var @enum = (Radyn.PaymentGateway.Tools.Enums.Bank)bankId;
            switch (@enum)
            {
                case Radyn.PaymentGateway.Tools.Enums.Bank.Mellat:
                    {
                        dictionary.Add((byte)@enum, "/Areas/Payment/Content/Images/Mellat.png");
                        break;
                    }
                case Radyn.PaymentGateway.Tools.Enums.Bank.Melli:
                    {
                        dictionary.Add((byte)@enum, "/Areas/Payment/Content/Images/Melli.png");
                        break;
                    }
                case Radyn.PaymentGateway.Tools.Enums.Bank.Radyn:
                    {
                        dictionary.Add((byte)@enum, "/Areas/Payment/Content/Images/Radyn.JPG");
                        break;
                    }
                case Radyn.PaymentGateway.Tools.Enums.Bank.Pasargad:
                    {
                        dictionary.Add((byte)@enum, "/Areas/Payment/Content/Images/Pasargad.jpg");
                        break;
                    }
                case Radyn.PaymentGateway.Tools.Enums.Bank.Saderat:
                    {
                        dictionary.Add((byte)@enum, "/Areas/Payment/Content/Images/Saderat.jpg");
                        break;
                    }
                case Radyn.PaymentGateway.Tools.Enums.Bank.Saman:
                    {
                        dictionary.Add((byte)@enum, "/Areas/Payment/Content/Images/Saman.jpg");
                        break;
                    }
                case Radyn.PaymentGateway.Tools.Enums.Bank.EghtesadeNovin:
                    {
                        dictionary.Add((byte)@enum, "/Areas/Payment/Content/Images/EghtesadeNovin.png");
                        break;
                    }
                case Radyn.PaymentGateway.Tools.Enums.Bank.IranKish:
                    {
                        dictionary.Add((byte)@enum, "/Areas/Payment/Content/Images/tejarat_new.png");
                        break;
                    }
                case Radyn.PaymentGateway.Tools.Enums.Bank.Mabna:
                    {
                        dictionary.Add((byte)@enum, "/Areas/Payment/Content/Images/mabna.png");
                        break;
                    }
                case Radyn.PaymentGateway.Tools.Enums.Bank.Ghavamin:
                    {
                        dictionary.Add((byte)@enum, "/Areas/Payment/Content/Images/ghavamin.png");
                        break;
                    }
                case Radyn.PaymentGateway.Tools.Enums.Bank.ZarinPal:
                    {
                        dictionary.Add((byte)@enum, "/Areas/Payment/Content/Images/zarinpal-logo.png");
                        break;
                    }
            }

            return dictionary;
        }
    }
}