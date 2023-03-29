using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTechPassApp.ViewModels.General
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }

        public string Messsage { get; set; }

    }
    public class LoginResponseModel :ResponseModel
    {

        public string AccessToken { get; set; }

    }
}
