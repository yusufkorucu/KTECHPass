using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTechPassApp.Data.Entity;


namespace KTechPassApp.ViewModels.User
{
    public class UserVM
    {

    }
    public class UserClaim
    {
        public string subject { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }
    public class UserAuthViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Tckno { get; set; }
    }
   

}
