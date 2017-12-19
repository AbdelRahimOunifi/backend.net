using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECApiRT.Models
{
    public class cart
    {
        public int cartID { get; set; }
        public int productID { get; set; }
        public int UserId { get; set; }
        public int count { get; set; }
        public string productName { get; set; }
    }
}
