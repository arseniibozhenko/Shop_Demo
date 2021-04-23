using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Demo.ViewModels
{
    public class GoodViewModel
    {
        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public ManufacturerViewModel Manufacturer { get; set; }
        public CategoryViewModel Category { get; set; }
        public decimal Price { get; set; }
        public decimal GoodCount { get; set; }
        public string Photo { get; set; }
    }
}