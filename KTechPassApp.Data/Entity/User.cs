using KTechPassApp.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTechPassApp.Data.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Tckno { get; set; }
        public string  Email { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<UserRecord>? Records { get; set; }

      
    }
}
