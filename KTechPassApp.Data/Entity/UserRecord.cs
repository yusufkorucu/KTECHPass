using KTechPassApp.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTechPassApp.Data.Entity
{
    public class UserRecord
    {
        public int Id { get; set; } 
        public string  RecordName { get; set; } // nabız gibi
        public string RecordValue { get; set; }// 120 gibi
        public Stat Status { get; set; }
        public Importance ImportanceLevel { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public User? User { get; set; }
    }
}
