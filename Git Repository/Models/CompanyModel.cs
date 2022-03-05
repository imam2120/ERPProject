using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmunaERP.Models
{
    public class CompanyModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Address  { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }

        public string WebAddress { get; set; }
    }

}