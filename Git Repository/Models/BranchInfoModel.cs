using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmunaERP.Models
{
    public class BranchInfoModel
    {
        public int BranchID { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string WebAddress { get; set; }
        public string Status { get; set; }
        
    }

}