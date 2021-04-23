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
    public class SalePosesService : IEntityService<SalePosDTO>
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public SalePosesService(IUnitOfWork unitOfWork)
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

        public List<SalePosDTO> GetAll()
        {
            List<SalePos> salePoses = unitOfWork.salePosesRepository.GetAll();
            //List<SalePosDTO> salePosesDTO = mapper.Map<List<SalePos>, List<SalePosDTO>>(salePoses);
            List<SalePosDTO> salePosesDTO = salePoses.Select(g => mapper.Map<SalePosDTO>(g)).ToList();

            return salePosesDTO;
        }

        public SalePosDTO GetById(int id)
        {
            SalePos salePos = unitOfWork.salePosesRepository.GetById(id);
            SalePosDTO salePosDTO = mapper.Map<SalePosDTO>(salePos);

            return salePosDTO;
        }

        public List<SalePosDTO> GetBy(Expression<Func<SalePosDTO, bool>> predicate)
        {
            Expression<Func<SalePos, bool>> p = mapper.Map<Expression<Func<SalePos, bool>>>(predicate);
            List<SalePos> salePoses = unitOfWork.salePosesRepository.GetBy(p);
            //List<SalePosDTO> salePosesDTO = mapper.Map<List<SalePos>, List<SalePosDTO>>(salePoses);
            List<SalePosDTO> salePosesDTO = salePoses.Select(g => mapper.Map<SalePosDTO>(g)).ToList();

            return salePosesDTO;
        }

        public void CreateOrUpdate(SalePosDTO item)
        {
            SalePos salePos = mapper.Map<SalePos>(item);
            unitOfWork.salePosesRepository.CreateOrUpdate(salePos);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            unitOfWork.salePosesRepository.Delete(id);
            unitOfWork.Save();
        }

        public void Delete(SalePosDTO item)
        {
            SalePos salePos = mapper.Map<SalePos>(item);
            unitOfWork.salePosesRepository.Delete(salePos);
            unitOfWork.Save();
        }
    }
}
