using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Demo.BLL.DTO
{
    public class GoodDTO
    {
        public int? GoodId { get; set; }
        public string GoodName { get; set; }
        public Nullable<int> ManufacturerId { get; set; }
        public ManufacturerDTO Manufacturer { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
        public decimal Price { get; set; }
        public decimal GoodCount { get; set; }
        public string Photo { get; set; }
    }
}