using Shop_Demo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Demo.ViewModels
{
    public class GoodsViewModel
    {
        public IEnumerable<GoodViewModel> Goods { get; set; }
        public int? ManufacturerId { get; set; }
        public IEnumerable<SelectListItem> Manufacturers { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public PriceFilter PriceFilter { get; set; }
        public string SortItem { get; set; }
        public IEnumerable<SelectListItem> SortItems { get; set; }
        public string Search { get; set; }
    }
}