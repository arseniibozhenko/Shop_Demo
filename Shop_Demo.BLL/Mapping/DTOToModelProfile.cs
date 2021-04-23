using AutoMapper;
using Shop_Demo.BLL.DTO;
using Shop_Demo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Demo.BLL.Mapping
{
    public class DTOToModelProfile : Profile
    {
        public DTOToModelProfile()
        {
            CreateMap<GoodDTO, Good>();
            CreateMap<ManufacturerDTO, Manufacturer>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<PhotoDTO, Photo>();
            CreateMap<SaleDTO, Sale>();
            CreateMap<SalePosDTO, SalePos>();
        }
    }
}
