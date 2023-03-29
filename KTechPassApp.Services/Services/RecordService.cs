using KTechPassApp.Data.Entity;
using KTechPassApp.ViewModels.General;
using KTechPassApp.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using KTechPassApp.ViewModels.Record;

namespace KTechPassApp.Services.Services
{
    public interface IRecordService
    {
        #region Interface
        ResponseModel CreateUserRecord(UserRecord UserRecordRequestModel);
        List<RecordVM> GetRecordList();
        RecordDetailVM GetRecordById(int id);
        ResponseModel UpdateUserRecord(RecordDetailVM record);
        public ResponseModel DeleteUserRecord(int id);
        #endregion

    }
    public class RecordService : IRecordService
    {
        #region Depency
        private PassContext db;
        IHttpContextAccessor httpContextAccessor;
        IUserService userService;

        public RecordService(PassContext _db, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            db = _db;
            this.httpContextAccessor = httpContextAccessor;
            this.userService = userService;
        }
        #endregion


        #region Services
        public ResponseModel CreateUserRecord(UserRecord UserRecordRequestModel)
        {
            ResponseModel responseModel = new ResponseModel();

            var user = userService.GetCurrentUser();


            if (user != null)
            {
                try
                {
                    User usr = db.Users.Where(x => x.Tckno == user.Tckno).FirstOrDefault();
                    UserRecord model = new UserRecord();
                    model.User = usr;
                    model.RecordName = UserRecordRequestModel.RecordName;
                    model.RecordValue = UserRecordRequestModel.RecordValue;
                    model.ImportanceLevel = UserRecordRequestModel.ImportanceLevel;
                    model.Status = UserRecordRequestModel.Status;
                    model.CreatedOn = DateTime.Now;
                    model.ModifiedOn = DateTime.Now;
                  


                    db.UserRecords.Add(model);
                    db.SaveChanges();
                    responseModel.Messsage = "Yeni Kayıt Başarı ile eklendi";
                    responseModel.IsSuccess = true;

                }
                catch (Exception ex)
                {

                    responseModel.IsSuccess = false;
                    responseModel.Messsage = "Error" + ex.Message;
                }
            }
            else
            {
                responseModel.IsSuccess = false;
                responseModel.Messsage = "Authorize Kullanıcı Bulunamadı! H-2";
            }

            return responseModel;
        }

        public List<RecordVM> GetRecordList()
        {
          
            List<RecordVM> responseModel = new List<RecordVM>();

            var user = userService.GetCurrentUser();
            if (user != null)
            {
                try
                {
                    List<UserRecord> record = db.UserRecords.Where(x => x.User.Email == user.Email).ToList();

                    foreach (var item in record)
                    {
                        RecordVM rcrd = new RecordVM();
                        rcrd.Id = item.Id;
                        rcrd.RecordName = item.RecordName;
                        //rcrd.RecordDescription = item.RecordDescription;

                        responseModel.Add(rcrd);
                    }
                    return responseModel;

                }
                catch (Exception ex)
                {
                    responseModel = null;

                }
            }
            else
            {
                responseModel = null;
            }

            return responseModel;
        }


        public RecordDetailVM GetRecordById(int id)
        {
            RecordDetailVM response = new RecordDetailVM();
            var user = userService.GetCurrentUser();
            if (user != null)
            {
                var record = db.UserRecords.Where(x => x.Id == id && x.User.Email == user.Email).FirstOrDefault();

                if (record != null)
                {
                    response.Id = record.Id;
                    response.RecordName = record.RecordName;
                    //response.RecordDescription = record.RecordDescription;
                    //response.RecordUrl = record.RecordUrl;
                    //response.RecordPassword = record.RecordPassword;

                }
                else
                {
                    return null;
                }

            }

            return response;

        }

        public ResponseModel UpdateUserRecord(RecordDetailVM record)
        {
            ResponseModel response = new ResponseModel();
            var user = userService.GetCurrentUser();
            if (user != null)
            {
                UserRecord userRecord = db.UserRecords.Where(x => x.Id == record.Id && x.User.Email == user.Email).FirstOrDefault();
                userRecord.RecordName = record.RecordName;
                //userRecord.RecordDescription = record.RecordDescription;
                //userRecord.RecordPassword = record.RecordPassword;
                //userRecord.RecordUrl = record.RecordUrl;

                db.Entry(userRecord).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                response.Messsage = "Bilgiler Başarı İle Güncellendi";
                response.IsSuccess = true;
            }
            else
            {
                response.Messsage = "Kullanıcı Bulunamadı H-2";
                response.IsSuccess = false;
            }
            return response;
        }


        public ResponseModel DeleteUserRecord(int id)
        {
            ResponseModel response = new ResponseModel();
            var user = userService.GetCurrentUser();
            if (user != null)
            {
                UserRecord userRecord = db.UserRecords.Where(x => x.Id == id && x.User.Email == user.Email).FirstOrDefault();
                db.UserRecords.Remove(userRecord);
                db.SaveChanges();
                response.Messsage = "Şifre Başarıyla Silindi";
                response.IsSuccess = true;
            }
            else
            {
                response.Messsage = "Kullanıcı Bulunamadı H-2";
                response.IsSuccess = false;
            }
            return response;
        }

        #endregion
    }
}
