using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECApiRT.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserLName { get; set; }
        public string UserPassword { get; set; }
        public string EMail { get; set; }
        public string Privilege { get; set; }
    }
}
