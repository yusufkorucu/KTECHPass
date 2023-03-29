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

namespace KTechPassApp.Services.Services
{
    public interface IUserService
    {
        ResponseModel CreateUser(User userRequesModel);
        UserAuthViewModel GetCurrentUser();
        User TryLogin(LoginRequestVM request);
        User Authenticate(LoginRequestVM userLogin);
    }
    public class UserService : IUserService
    {
        private PassContext db;
        IHttpContextAccessor httpContextAccessor;

        public UserService(PassContext _db, IHttpContextAccessor httpContextAccessor)
        {
            db = _db;
            this.httpContextAccessor = httpContextAccessor;
        }

        public ResponseModel CreateUser(User userRequesModel)
        {

             //Bu kısımda validasyonlar sonucunda serviste işlem yaptırmak 
             //gerekli ilgili arkadaş bu kısımlara tüm servis endpointlerinde dikkat etmeli
            ResponseModel responseModel = new ResponseModel();
            var existUser = db.Users.Where(x => x.Tckno == userRequesModel.Tckno).FirstOrDefault();
            if (existUser==null)
            {
                try
                {
                    responseModel.Messsage = "Yeni Kullanici Basari ile eklendi";
                    responseModel.IsSuccess = true;
                    db.Users.Add(userRequesModel);
                    db.SaveChanges();

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
                responseModel.Messsage = "Aynı Kimlik Numarsao İle  Farklı Bir Kullanıcı Mevcut! H-1";
            }
            
            return responseModel;
        }




        public List<UserClaim> GetCurrentUserClaims()
        {
            var context = httpContextAccessor.HttpContext;
            ClaimsIdentity Identity = null;


            if (context != null && context.User != null && context.User.Identity != null)
            {
                Identity = context.User.Identity as ClaimsIdentity;

                var claims = (from c in Identity.Claims
                              select new UserClaim
                              {
                                  subject = c.Subject.Name,
                                  type = c.Type,
                                  value = c.Value
                              }).ToList();

                return claims;
            }

            return null;
        }

    
        public UserAuthViewModel GetCurrentUser()
        {
            UserAuthViewModel userAuth = null;

            var userClaims = GetCurrentUserClaims();

            if (userClaims != null && userClaims.Count() > 0)
            {
                string currentUsername = userClaims.Where(x => x.type == ClaimTypes.Email).FirstOrDefault().value;

                if (!string.IsNullOrWhiteSpace(currentUsername))
                {
                    User user = (from u in db.Users
                                 where
                                   u.Email == currentUsername
                                 select u
                    ).FirstOrDefault();

                    if (user != null)
                    {
                        MapUserToUserAuthViewModel(ref userAuth, user);
                    }
                }
            }

            return userAuth;
        }

        public void MapUserToUserAuthViewModel(ref UserAuthViewModel userAuth, User user)
        {
            userAuth = new UserAuthViewModel
            {
                Id = user.Id,
                Email=user.Email,
                Password=user.Password,
                Name = user.Name,
                Tckno=user.Tckno
            };
        }

        public User TryLogin(LoginRequestVM request)
        {
            ResponseModel response = new ResponseModel();
            var user= db.Users.Where(x => x.Tckno == request.Tckno && x.Password == request.Password).FirstOrDefault();
            UserAuthViewModel userAuth = new UserAuthViewModel();
            if (user!=null)
            {
                response.IsSuccess = true;
                response.Messsage = "Başarılı giriş";
                MapUserToUserAuthViewModel(ref userAuth, user);

            }
            else
            {
                response.IsSuccess = false;
                response.Messsage = "TCKNO yada Şifre Hatalı";
            }
            return user;
        }


        public User Authenticate(LoginRequestVM userLogin)
        {
            var currentUser = db.Users.FirstOrDefault(o => o.Tckno == userLogin.Tckno && o.Password == userLogin.Password);

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}
