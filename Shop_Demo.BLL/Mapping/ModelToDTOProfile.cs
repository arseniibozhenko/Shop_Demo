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
    public class ModelToDTOProfile : Profile
    {
        public ModelToDTOProfile()
        {
            CreateMap<Good, GoodDTO>().ForMember(g => g.Photo, opt => opt.MapFrom(src => src.Photo.FirstOrDefault().PhotoPath));
            CreateMap<Manufacturer, ManufacturerDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Photo, PhotoDTO>();
            CreateMap<Sale, SaleDTO>();
            CreateMap<SalePos, SalePosDTO>();
        }
    }
}
