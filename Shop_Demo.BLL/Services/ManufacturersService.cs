using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Shop_Demo.BLL.DTO;
using Shop_Demo.BLL.Mapping;
using Shop_Demo.DAL.Context;
using Shop_Demo.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Demo.BLL.Services
{
    public class ManufacturersService : IEntityService<ManufacturerDTO>
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public ManufacturersService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            //Настройка AutoMapper
            var config = new MapperConfiguration(cfg => {
                cfg.AddExpressionMapping();
                cfg.AddProfile<ModelToDTOProfile>();
                cfg.AddProfile<DTOToModelProfile>();
            });
            mapper = config.CreateMapper();
        }

        public List<ManufacturerDTO> GetAll()
        {
            List<Manufacturer> manufacturers = unitOfWork.manufacturersRepository.GetAll();
            //List<ManufacturerDTO> manufacturersDTO = mapper.Map<List<Manufacturer>, List<ManufacturerDTO>>(manufacturers);
            List<ManufacturerDTO> manufacturersDTO = manufacturers.Select(g => mapper.Map<ManufacturerDTO>(g)).ToList();

            return manufacturersDTO;
        }

        public ManufacturerDTO GetById(int id)
        {
            Manufacturer manufacturer = unitOfWork.manufacturersRepository.GetById(id);
            ManufacturerDTO manufacturerDTO = mapper.Map<ManufacturerDTO>(manufacturer);

            return manufacturerDTO;
        }

        public List<ManufacturerDTO> GetBy(Expression<Func<ManufacturerDTO, bool>> predicate)
        {
            Expression<Func<Manufacturer, bool>> p = mapper.Map<Expression<Func<Manufacturer, bool>>>(predicate);
            List<Manufacturer> manufacturers = unitOfWork.manufacturersRepository.GetBy(p);
            //List<ManufacturerDTO> manufacturersDTO = mapper.Map<List<Manufacturer>, List<ManufacturerDTO>>(manufacturers);
            List<ManufacturerDTO> manufacturersDTO = manufacturers.Select(g => mapper.Map<ManufacturerDTO>(g)).ToList();

            return manufacturersDTO;
        }

        public void CreateOrUpdate(ManufacturerDTO item)
        {
            Manufacturer manufacturer = mapper.Map<Manufacturer>(item);
            unitOfWork.manufacturersRepository.CreateOrUpdate(manufacturer);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            unitOfWork.manufacturersRepository.Delete(id);
            unitOfWork.Save();
        }

        public void Delete(ManufacturerDTO item)
        {
            Manufacturer manufacturer = mapper.Map<Manufacturer>(item);
            unitOfWork.manufacturersRepository.Delete(manufacturer);
            unitOfWork.Save();
        }
    }
}
