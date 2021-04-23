using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Demo.ViewModels
{
    public class SalePosViewModel
    {
        public int SalePosId { get; set; }
        public int SaleId { get; set; }
        public int GoodId { get; set; }
        public int CountGood { get; set; }
        public decimal Summa { get; set; }
    }
}