using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Demo.BLL.DTO
{
    public class PhotoDTO
    {
        public int PhotoId { get; set; }
        public Nullable<int> GoodId { get; set; }
        public GoodDTO Good { get; set; }
        public string PhotoPath { get; set; }
    }
}