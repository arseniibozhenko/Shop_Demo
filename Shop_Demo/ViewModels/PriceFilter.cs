using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Demo.ViewModels
{
    public class PriceFilter
    {
        public decimal? From { get; set; }
        public decimal? To { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
    }
}