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
    public class ViewModelToDTOProfile : Profile
    {
        public ViewModelToDTOProfile()
        {
            CreateMap<GoodViewModel, GoodDTO>();
            CreateMap<ManufacturerViewModel, ManufacturerDTO>();
            CreateMap<CategoryViewModel, CategoryDTO>();
            CreateMap<GoodCreateEditViewModel, GoodDTO>();
        }
    }
}
