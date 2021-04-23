using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Demo.BLL.DTO
{
    public class SaleDTO
    {
        public int SaleId { get; set; }
        public int NumberSale { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public System.DateTime DateSale { get; set; }
        public decimal Summa { get; set; }
    }
}