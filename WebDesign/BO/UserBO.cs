using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using Excel;
using Radyn.EnterpriseNode;
using Radyn.EnterpriseNode.DataStructure;
using Radyn.FormGenerator;
using Radyn.FormGenerator.ControlFactory.Controls;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.Tools;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using Radyn.WebDesign.DataStructure;
using Radyn.WebDesign.Definition;
using Control = Radyn.FormGenerator.ControlFactory.Base.Control;

namespace Radyn.WebDesign.BO
{
    internal class UserBO : BusinessBase<DataStructure.User>
    {


        internal DataTable ReportUserForms(IConnectionHandler connectionHandler, Guid formId, Guid WebId, string culture)
        {


            try
            {

                var table = new DataTable();
                var userList = new UserBO().Where(connectionHandler, x => x.WebId == WebId);
                var list = FormGeneratorComponent.Instance.FormDataFacade.Where(x => x.StructureId == formId && x.ObjectName == typeof(WebDesignUserForms).Name);
                var formStructure = FormGeneratorComponent.Instance.FormStructureFacade;
                var form = formStructure.Get(formId);

                table.Columns.Add(Resources.WebDesign.Username);
                foreach (Control control in form.Controls)
                {
                    if (control == null) continue;
                    if (control.GetType() == typeof(Label) || control.GetType() == typeof(FileUploader)) continue;
                    var columnName = control.GetCaption();

                    table.Columns.Add(columnName, control.DisplayValue != null ? control.DisplayValueType : typeof(string));
                }
                if (culture == "fa-IR")
                {
                    var ordinal = table.Columns.Count - 1;

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        table.Columns[0].SetOrdinal(ordinal);
                        ordinal--;
                    }
                }

                foreach (var user in userList)
                {

                    var firstOrDefualt = list.FirstOrDefault(c => c.RefId == user.Id.ToString());
                    var dictionary = firstOrDefualt != null ? Extentions.GetControlData(firstOrDefualt.Data) : null;
                    var row = table.NewRow();
                    var stringWriter = new StringWriter();
                    var writer = new Html32TextWriter(stringWriter);
                    row[Resources.WebDesign.Username] = user.Username;

                    foreach (Control control in form.Controls)
                    {
                        if (control.GetType() == typeof(Label) || control.GetType() == typeof(FileUploader)) continue;
                        control.Writer = writer;
                        control.FormState = FormState.DetailsMode;
                        control.Value = dictionary != null && dictionary.ContainsKey(control.Id) ? dictionary[control.Id] : string.Empty;
                        control.Generate();
                        var columnName = control.GetCaption();
                        row[columnName] = control.DisplayValue != null ? control.DisplayValue.ToString() : string.Empty;

                    }
                    table.Rows.Add(row);

                }

                return table;

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
        public IEnumerable<User> Search(IConnectionHandler connectionHandler, Guid WebDesignId, string txtSearch, User user,  EnterpriseNode.Tools.Enums.Gender gender,
          FormStructure formStructure)
        {
            PredicateBuilder<User> predicateBuilder = UserSerachPredicateBuilder(WebDesignId, txtSearch, user, gender, formStructure);

            return OrderByDescending(connectionHandler, x => x.RegisterDate, predicateBuilder.GetExpression());
        }
        
        public List<dynamic> SearchDynamic(IConnectionHandler connectionHandler, Guid WebDesignId, string txtSearch, User user,  EnterpriseNode.Tools.Enums.Gender gender,
         FormStructure formStructure)
        {
            PredicateBuilder<User> predicateBuilder = UserSerachPredicateBuilder(WebDesignId, txtSearch, user, gender, formStructure);
            Expression<Func<User, object>>[] selectcolumn = Selectcolumn();
            return Select(connectionHandler, selectcolumn, predicateBuilder.GetExpression(),
                new OrderByModel<User>() {Expression = x => x.RegisterDate, OrderType = OrderType.DESC});
        }
        public void InformUserRegister(IConnectionHandler connectionHandler, Guid WebDesignId, ModelView.InFormEntitiyList<User> valuePairs)
        {
            if (!valuePairs.Any())
            {
                return;
            }


            var  homa1 = new WebSiteBO().Get(connectionHandler, WebDesignId);
            Configuration config = homa1.Configuration;
            if (config.UserRegisterInformType == null) return;
           
            foreach (var user in valuePairs)
            {
                var status = ((Enums.UserStatus)user.obj.Status).GetDescriptionInLocalization();
                var homaCompleteUrl = homa1.GetWebSiteCompleteUrl();
                string sms = string.Format(user.SmsBody, homa1.Title, user.obj.EnterpriseNode.DescriptionFieldWithGender, status);
                string email = string.Format(user.EmailBody, homa1.Title, user.obj.EnterpriseNode.DescriptionFieldWithGender, homaCompleteUrl, status);
                


                Message.Tools.ModelView.MessageModel inform = new Message.Tools.ModelView.MessageModel()
                {

                    Email = user.obj.EnterpriseNode.Email,
                    Mobile = user.obj.EnterpriseNode.Cellphone,
                    EmailTitle = homa1.DescriptionField,
                    EmailBody = email,
                    SMSBody = sms

                };
                new WebSiteBO().SendInform((byte)config.UserRegisterInformType, inform, config, homa1.Title);
               
            }
        }
        private static Expression<Func<User, object>>[] Selectcolumn()
        {
            Expression<Func<User, object>>[] selectcolumn = new Expression<Func<User, object>>[]
            {
                x => x.Id,
                x => x.RegisterDate,
                x => x.EnterpriseNode.RealEnterpriseNode.FirstName + " " + x.EnterpriseNode.RealEnterpriseNode.LastName,
                x => x.EnterpriseNode.Email,
                x => x.Status,
                x => x.Username,
                
            };
            return selectcolumn;
        }

        private PredicateBuilder<User> UserSerachPredicateBuilder(Guid WebId, string txtSearch, User user, EnterpriseNode.Tools.Enums.Gender gender,
            FormStructure formStructure)
        {
            PredicateBuilder<User> predicateBuilder = new PredicateBuilder<User>();
            predicateBuilder.And(x => x.WebId == WebId);
            if (!string.IsNullOrEmpty(txtSearch))
            {
                txtSearch = txtSearch.ToLower();
                predicateBuilder.And((x => x.Username.Contains(txtSearch) || x.Number.ToString().Contains(txtSearch) || x.EnterpriseNode.RealEnterpriseNode.FirstName.Contains(txtSearch) || x.EnterpriseNode.RealEnterpriseNode.LastName.Contains(txtSearch)
                || x.EnterpriseNode.RealEnterpriseNode.NationalCode.Contains(txtSearch) || x.EnterpriseNode.RealEnterpriseNode.IDNumber.Contains(txtSearch) || x.EnterpriseNode.Address.Contains(txtSearch)
                || x.EnterpriseNode.Website.Contains(txtSearch) || x.EnterpriseNode.Email.Contains(txtSearch) || x.EnterpriseNode.Tel.Contains(txtSearch)));

            }
            if (gender != EnterpriseNode.Tools.Enums.Gender.None)
            {
                bool? genderw = (gender == EnterpriseNode.Tools.Enums.Gender.Man);
                predicateBuilder.And(x => x.EnterpriseNode.RealEnterpriseNode.Gender == genderw);
            }
            if (formStructure != null)
            {
                IEnumerable<string> @where = FormGeneratorComponent.Instance.FormDataFacade.Search(formStructure);
                if (@where.Any())
                {
                    predicateBuilder.And(x => x.Id.In(@where.Select(s => s.ToGuid())));
                }
            }


            if (user != null)
            {
              

                if (user.ParentId != null)
                {
                    predicateBuilder.And(x => x.ParentId == user.ParentId);
                }

                if (user.Status !=Enums.UserStatus.None)
                {
                    predicateBuilder.And(x => x.Status == user.Status);
                }

                if (!string.IsNullOrEmpty(user.RegisterDate))
                {
                    predicateBuilder.And(x => x.RegisterDate == user.RegisterDate);
                }

                
            }
            return predicateBuilder;
        }


       
        public ModelView.ModifyResult<User> UpdateList(IConnectionHandler connectionHandler,  List<User> userlist)
        {

           
            var modifyWithInform = new ModelView.ModifyResult<User>();
            List<User> @where = Where(connectionHandler, x => x.Id.In(userlist.Select(i => i.Id)));
            foreach (User user in userlist)
            {
                User user1 = @where.FirstOrDefault(x => x.Id == user.Id);
                if (user1 == null)
                {
                    continue;
                }
                user1.Status = user.Status;
                if (!Update(connectionHandler, user1))
                    throw new Exception("خطایی در ویرایش  کاربر وجود دارد");

                if (modifyWithInform.InformList.All(x => x.obj.Id != user1.Id))
                    modifyWithInform.AddInform(user1, Resources.WebDesign.UserChangeStatusEmail,
                        Resources.WebDesign.UserChangeStatusSMS);

                if (!user1.ParentId.HasValue || modifyWithInform.InformList.Any(x => x.obj.Id == user1.ParentId))
                    continue;

                User key = Get(connectionHandler, user1.ParentId);
                if (key != null)
                    modifyWithInform.AddInform(key, Resources.WebDesign.UserChangeStatusEmail,
                        Resources.WebDesign.UserChangeStatusSMS);
            }
            modifyWithInform.Result = true;
            modifyWithInform.SendInform = true;
            return modifyWithInform;
        }

        public override bool Delete(IConnectionHandler connectionHandler,  params object[] keys)
        {
            User obj = Get(connectionHandler, keys);
            bool users = Any(connectionHandler, article => article.ParentId == obj.Id);
            if (users)
            {
                throw new Exception("این کاربر دارای اشخاص زیر دستی است آن را نمیتوانید حذف کنید");
            }
            if (!base.Delete(connectionHandler, keys))
            {
                throw new Exception("خطا در حذف کاربر");
            }

            return true;
        }


        
       


        public IEnumerable<ModelView.ReportChartModel> ChartUserStatusCount(IConnectionHandler connectionHandler, Guid WebId)
        {
            List<ModelView.ReportChartModel> listout = new List<ModelView.ReportChartModel>();
            List<dynamic> list = GroupBy(connectionHandler, new Expression<Func<User, object>>[] { x => x.Status },
                new[]
                {
                    new GroupByModel<User>()
                    {
                        Expression = x => x.Status,
                        AggrigateFuntionType = AggrigateFuntionType.Count
                    }
                }, x => x.WebId == WebId);
            IEnumerable<KeyValuePair<byte, string>> enums = EnumUtils.ConvertEnumToIEnumerableInLocalization<Enums.UserStatus>().Select(
                keyValuePair =>
                    new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Enums.UserStatus>(),
                        keyValuePair.Value));
            foreach (KeyValuePair<byte, string> item in enums)
            {
                dynamic first = list.FirstOrDefault(x => (x.Status is byte) && (byte)x.Status == item.Key);
                listout.Add(new ModelView.ReportChartModel()
                {
                    Count = first?.CountStatus ?? 0,
                    Value = ((Enums.UserStatus)item.Key).GetDescriptionInLocalization()
                });
            }
            return listout;
        }

        public IEnumerable<ModelView.ReportChartModel> ChartUserMothCount(IConnectionHandler connectionHandler, Guid WebId, string year)
        {


            List<ModelView.ReportChartModel> listout = new List<ModelView.ReportChartModel>();
            List<dynamic> list = GroupBy(connectionHandler, new Expression<Func<User, object>>[] { x => x.RegisterDate.Substring(5, 2) },
                new[]
                {
                    new GroupByModel<User>()
                    {
                        Expression = x => x.RegisterDate.Substring(5,2),
                        AggrigateFuntionType = AggrigateFuntionType.Count
                    }
                }, x => x.WebId == WebId && x.RegisterDate.Substring(0, 4) == year);
            IEnumerable<KeyValuePair<byte, string>> months = EnumUtils.ConvertEnumToIEnumerableInLocalization<Common.Definition.Enums.PersianMonth>().Select(
                keyValuePair =>
                    new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Common.Definition.Enums.PersianMonth>(),
                        keyValuePair.Value));
            foreach (KeyValuePair<byte, string> item in months)
            {
                dynamic first = list.FirstOrDefault(x => (x.RegisterDate is string) && ((string)x.RegisterDate).ToByte() == item.Key);
                listout.Add(new ModelView.ReportChartModel()
                {
                    Count = first?.CountRegisterDate ?? 0,
                    Value = ((Common.Definition.Enums.PersianMonth)item.Key).GetDescriptionInLocalization()
                });
            }
            return listout;
        }
        public IEnumerable<ModelView.ReportChartModel> ChartUserDayCount(IConnectionHandler connectionHandler, Guid WebId, string moth, string year)
        {
            List<ModelView.ReportChartModel> listout = new List<ModelView.ReportChartModel>();
            List<dynamic> list = GroupBy(connectionHandler, new Expression<Func<User, object>>[] { x => x.RegisterDate.Substring(8, 2) },
                new[]
                {
                    new GroupByModel<User>()
                    {
                        Expression = x => x.RegisterDate.Substring(8,2),
                        AggrigateFuntionType = AggrigateFuntionType.Count
                    }
                }, x => x.WebId == WebId && x.RegisterDate.Substring(0, 4) == year && x.RegisterDate.Substring(5, 2) == moth);
            for (int x = 1; x <= (moth.CompareTo("06") <= 0 ? 31 : 30); x++)
            {
                string number = x > 10 ? x.ToString() : "0" + x;
                dynamic first = list.FirstOrDefault(i => (i.RegisterDate is string) && (string)i.RegisterDate == number);
                listout.Add(new ModelView.ReportChartModel()
                {
                    Count = first?.CountRegisterDate ?? 0,
                    Value = number
                });
            }
            return listout;
        }
     







        public Dictionary<User, List<string>> ImportFromExcel(IConnectionHandler connectionHandler, HttpPostedFileBase fileBase, Guid WebId, Guid? parentId)
        {
            try
            {
                Dictionary<User, List<string>> keyValuePairs = new Dictionary<User, List<string>>();
                if (fileBase == null)
                {
                    return keyValuePairs;
                }

                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fileBase.InputStream);
                excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();
                if (result == null)
                {
                    return keyValuePairs;
                }

                UserBO userBo = new UserBO();
                foreach (DataTable table in result.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        List<string> resultStatus = new List<string>();
                        User user = new User()
                        {
                            Id = Guid.NewGuid(),
                            WebId = WebId,
                            Status = Enums.UserStatus.Register,
                            EnterpriseNode =
                                new EnterpriseNode.DataStructure.EnterpriseNode()
                                {
                                    RealEnterpriseNode = new RealEnterpriseNode()
                                }
                        };

                        if (string.IsNullOrEmpty(row[0].ToString()))
                        {
                            resultStatus.Add(Resources.WebDesign.PleaseEnterYourEmail);
                        }
                        else
                        {
                            user.EnterpriseNode.Email = row[0].ToString();
                            user.Username = row[0].ToString();
                            if (!Utility.Utils.IsEmail(user.EnterpriseNode.Email))
                            {
                                resultStatus.Add(Resources.WebDesign.UnValid_Enter_Email);
                            }

                            User byUserName = userBo.FirstOrDefault(connectionHandler, x => x.EnterpriseNode.Email == user.EnterpriseNode.Email & x.WebId == WebId);
                            if (byUserName != null)
                            {
                                resultStatus.Add(Resources.WebDesign.UserNameIsRepeate);
                                user = byUserName;
                                user.State = Framework.ObjectState.Dirty;
                                if (parentId != null && user.ParentId != parentId)
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(user.Username))
                                {
                                    user.Password = StringUtils.HashPassword(user.Username.Substring(0, 5));
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(row[1].ToString()))
                        {
                            resultStatus.Add(Resources.WebDesign.Please_Enter_YourName);
                            user.EnterpriseNode.RealEnterpriseNode.FirstName = string.Empty;
                        }
                        else
                        {
                            user.EnterpriseNode.RealEnterpriseNode.FirstName = row[1].ToString();
                        }

                        if (string.IsNullOrEmpty(row[2].ToString()))
                        {
                            resultStatus.Add(Resources.WebDesign.Please_Enter_YourLastName);
                            user.EnterpriseNode.RealEnterpriseNode.LastName = string.Empty;
                        }
                        else
                        {
                            user.EnterpriseNode.RealEnterpriseNode.LastName = row[2].ToString();
                        }

                        if (string.IsNullOrEmpty(row[3].ToString()))
                        {
                            resultStatus.Add(Resources.WebDesign.Please_Enter_YourGender);
                            user.EnterpriseNode.RealEnterpriseNode.Gender = null;
                        }
                        else
                        {
                            switch (row[3].ToString().ToLower())
                            {
                                case "men":
                                case "مرد":
                                    user.EnterpriseNode.RealEnterpriseNode.Gender = true;
                                    break;
                                case "women":
                                case "زن":
                                    user.EnterpriseNode.RealEnterpriseNode.Gender = false;
                                    break;
                                default:
                                    user.EnterpriseNode.RealEnterpriseNode.Gender = null;
                                    break;
                            }
                        }
                        if (string.IsNullOrEmpty(row[4].ToString()))
                        {
                            resultStatus.Add(Resources.WebDesign.Please_Enter_YourMobile);
                            user.EnterpriseNode.Cellphone = string.Empty;
                        }
                        else
                        {
                            user.EnterpriseNode.Cellphone = row[4].ToString();
                        }

                        if (!string.IsNullOrEmpty(row[5].ToString()))
                        {
                            long NationalCode = row[5].ToString().ToLong();
                            string national = string.Format("{0:D10}", NationalCode);
                            if (!Radyn.Utility.Utils.ValidNationalID(national))
                            {
                                resultStatus.Add("کد ملی صحیح نمیباشد");
                            }
                            else
                            {
                                user.EnterpriseNode.RealEnterpriseNode.NationalCode = national;
                            }
                        }
                        else
                        {
                            user.EnterpriseNode.RealEnterpriseNode.NationalCode = string.Empty;
                        }

                        user.EnterpriseNode.Address = !string.IsNullOrEmpty(row[6].ToString()) ? row[6].ToString() : string.Empty;

                        keyValuePairs.Add(user, resultStatus);
                    }
                }
                return keyValuePairs;
            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace); Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace); throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace); throw new KnownException(ex.Message, ex);
            }
        }

        protected override void CheckConstraint(IConnectionHandler connectionHandler, User item)
        {
            base.CheckConstraint(connectionHandler, item);
            if (item.EnterpriseNode != null)
            {
                if (item.EnterpriseNode.Email != null && !string.IsNullOrEmpty(item.EnterpriseNode.Email.Trim()) &&
                    Any(connectionHandler, x => x.EnterpriseNode.Email == item.EnterpriseNode.Email && x.WebId == item.WebId && x.Id != item.Id))
                {
                    throw new KnownException(string.Format("ایمیل {0} قبلا در سامانه ثبت شده است", item.EnterpriseNode.Email));
                }

                if (item.EnterpriseNode.RealEnterpriseNode != null && item.EnterpriseNode.RealEnterpriseNode.NationalCode != null && !string.IsNullOrEmpty(item.EnterpriseNode.RealEnterpriseNode.NationalCode.Trim()) &&
                    Any(connectionHandler, x => x.EnterpriseNode.RealEnterpriseNode.NationalCode == item.EnterpriseNode.RealEnterpriseNode.NationalCode && x.WebId == item.WebId && x.Id != item.Id))
                {
                    throw new KnownException(string.Format("کد ملی {0} قبلا در سامانه ثبت شده است", item.EnterpriseNode.RealEnterpriseNode.NationalCode));
                }
            }
            if (item.Username != null && !string.IsNullOrEmpty(item.Username.Trim()) &&
                 Any(connectionHandler, x => x.Username.ToLower() == item.Username.ToLower() && x.WebId == item.WebId && x.Id != item.Id))
            {
                throw new KnownException(string.Format("نام کاربری {0} قبلا در سامانه ثبت شده است", item.Username));
            }
        }

        public override bool Insert(IConnectionHandler connectionHandler, User user)
        {
            user.RegisterDate = DateTime.Now.ShamsiDate();
            if (!string.IsNullOrEmpty(user.Password)) user.Password = StringUtils.HashPassword(user.Password);
            return base.Insert(connectionHandler, user);

        }

        internal bool Insert(IConnectionHandler connectionHandler1, IConnectionHandler enterpriseNodeconnection, User user, HttpPostedFileBase fileBase)
        {
            Guid id = user.Id;
            BOUtility.GetGuidForId(ref id);
            user.Id = id;
            user.EnterpriseNode.Id = user.Id;
            if (!EnterpriseNodeComponent.Instance.EnterpriseNodeTransactionalFacade(enterpriseNodeconnection).Insert(user.EnterpriseNode, fileBase))
            {
                throw new Exception(Resources.WebDesign.ErrorInSaveUser);
            }

            return this.Insert(connectionHandler1, user);
        }
       

        public override bool Update(IConnectionHandler connectionHandler, User user)
        {

            var oldpass = new UserBO().SelectFirstOrDefault(connectionHandler, x => x.Password, x => x.Id == user.Id);
            user.Password =
                (user.Password != null && !string.IsNullOrEmpty(user.Password.Trim()) &&
                 (oldpass == null || (oldpass.ToLower() != user.Password.ToLower())))
                    ? StringUtils.HashPassword(user.Password)
                    : oldpass;

            return base.Update(connectionHandler, user);
        }

        internal bool Update(IConnectionHandler connectionHandler1, IConnectionHandler enterpriseNodeconnection, User user, System.Web.HttpPostedFileBase fileBase = null)
        {

            if (!EnterpriseNodeComponent.Instance.EnterpriseNodeTransactionalFacade(enterpriseNodeconnection).Update(user.EnterpriseNode, fileBase))
            {
                throw new Exception(Resources.WebDesign.ErrorInSaveUser);
            }

            if (!this.Update(connectionHandler1, user))
            {
                throw new Exception(Resources.WebDesign.ErrorInSaveUser);
            }

            return true;
        }



        internal bool Insert(IConnectionHandler connectionHandler1, IConnectionHandler enterpriseNodeconnection, IConnectionHandler formgeneratorconnection, User user,  FormGenerator.DataStructure.FormStructure formModel, System.Web.HttpPostedFileBase fileBase)
        {
            if (!Insert(connectionHandler1, enterpriseNodeconnection, user, fileBase))
            {
                throw new Exception(Resources.WebDesign.ErrorInSaveUser);
            }

            formModel.RefId = user.Id.ToString();
            if (!FormGeneratorComponent.Instance.FormDataTransactionalFacade(formgeneratorconnection).ModifyFormData(formModel))
            {
                throw new Exception(Resources.WebDesign.ErrorInSaveUser);
            }

            return true;
        }
        internal bool Update(IConnectionHandler connectionHandler1, IConnectionHandler enterpriseNodeconnection, IConnectionHandler formgeneratorconnection, User user,  FormGenerator.DataStructure.FormStructure formModel, System.Web.HttpPostedFileBase fileBase)
        {
            if (!this.Update(connectionHandler1, enterpriseNodeconnection, user, fileBase))
            {
                throw new Exception(Resources.WebDesign.ErrorInSaveUser);
            }

            if (!FormGeneratorComponent.Instance.FormDataTransactionalFacade(formgeneratorconnection).ModifyFormData(formModel))
            {
                throw new Exception(Resources.WebDesign.ErrorInSaveUser);
            }

            return true;
        }

        internal bool Update(IConnectionHandler connectionHandler1, IConnectionHandler enterpriseNodeconnection, IConnectionHandler formgeneratorconnection, User user,  FormGenerator.DataStructure.FormStructure formModel)
        {
            if (!this.Update(connectionHandler1, enterpriseNodeconnection, user, null))
            {
                throw new Exception(Resources.WebDesign.ErrorInSaveUser);
            }

            if (!FormGeneratorComponent.Instance.FormDataTransactionalFacade(formgeneratorconnection).ModifyFormData(formModel))
            {
                throw new Exception(Resources.WebDesign.ErrorInSaveUser);
            }

            return true;
        }
        
        



       

        

       
    }
}
