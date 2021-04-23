using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Demo.WebApi.DTO
{
    public class GoodDTO
    {
        public int? GoodId { get; set; }
        public string GoodName { get; set; }
        public Nullable<int> ManufacturerId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal GoodCount { get; set; }
    }
}