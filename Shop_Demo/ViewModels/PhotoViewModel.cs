using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Demo.ViewModels
{
    public class PhotoViewModel
    {
        public int PhotoId { get; set; }
        public GoodViewModel Good { get; set; }
        public string PhotoPath { get; set; }
    }
}