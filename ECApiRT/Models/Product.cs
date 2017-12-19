using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECApiRT.Models
{
    public class Product
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public string productCode { get; set; }
        public string productType { get; set; }
        public string releaseDate { get; set; }
        public int price { get; set; }
        public string description { get; set; }
        public int starRating { get; set; }
        public string imageUrl { get; set; }
        public string prodUrl { get; set; }
    }
}
