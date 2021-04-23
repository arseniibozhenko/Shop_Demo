using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Demo.ViewModels
{
    public class GoodCreateEditViewModel
    {
        public int? GoodId { get; set; }
        public string GoodName { get; set; }
        public Nullable<int> ManufacturerId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal GoodCount { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Manufacturers { get; set; }
    }
}