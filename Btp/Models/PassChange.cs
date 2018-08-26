using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Btp.Models
{
    public class PassChange
    {
        public string UserLogin { get; set; }
        public string OldPass { get; set; }
        public string NewPass { get; set; }
        public string ConfPass { get; set; }
    }
}