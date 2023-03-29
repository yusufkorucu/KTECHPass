using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTechPassApp.Data.Entity;


namespace KTechPassApp.ViewModels.Record
{
    public class RecordListVM
    {
        public List<RecordVM> RecordList { get; set; }
    }
    public class RecordVM
    {
        public int Id { get; set; }
        public string RecordName { get; set; }
        public string RecordDescription { get; set; }
    }

    public class RecordDetailVM
    {
        public int Id { get; set; }
        public string RecordName { get; set; }
        public string RecordDescription { get; set; }
        public string RecordUrl { get; set; }
        public string RecordPassword { get; set; }
    }



}
