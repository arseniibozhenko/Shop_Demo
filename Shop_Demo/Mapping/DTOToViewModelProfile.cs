using AutoMapper;
using Shop_Demo.BLL.DTO;
using Shop_Demo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Demo.Mapping
{
    public class DTOToViewModelProfile : Profile
    {
        public DTOToViewModelProfile()
        {
            CreateMap<GoodDTO, GoodViewModel>();
            CreateMap<ManufacturerDTO, ManufacturerViewModel>();
            CreateMap<CategoryDTO, CategoryViewModel>();
            CreateMap<GoodDTO, GoodCreateEditViewModel>();
        }
    }
}
