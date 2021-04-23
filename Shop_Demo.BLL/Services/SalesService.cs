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
    public class SalesService : IEntityService<SaleDTO>
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public SalesService(IUnitOfWork unitOfWork)
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

        public List<SaleDTO> GetAll()
        {
            List<Sale> sales = unitOfWork.salesRepository.GetAll();
            //List<SaleDTO> salesDTO = mapper.Map<List<Sale>, List<SaleDTO>>(sales);
            List<SaleDTO> salesDTO = sales.Select(g => mapper.Map<SaleDTO>(g)).ToList();

            return salesDTO;
        }

        public SaleDTO GetById(int id)
        {
            Sale sale = unitOfWork.salesRepository.GetById(id);
            SaleDTO saleDTO = mapper.Map<SaleDTO>(sale);

            return saleDTO;
        }

        public List<SaleDTO> GetBy(Expression<Func<SaleDTO, bool>> predicate)
        {
            Expression<Func<Sale, bool>> p = mapper.Map<Expression<Func<Sale, bool>>>(predicate);
            List<Sale> sales = unitOfWork.salesRepository.GetBy(p);
            //List<SaleDTO> salesDTO = mapper.Map<List<Sale>, List<SaleDTO>>(sales);
            List<SaleDTO> salesDTO = sales.Select(g => mapper.Map<SaleDTO>(g)).ToList();

            return salesDTO;
        }

        public void CreateOrUpdate(SaleDTO item)
        {
            Sale sale = mapper.Map<Sale>(item);
            unitOfWork.salesRepository.CreateOrUpdate(sale);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            unitOfWork.salesRepository.Delete(id);
            unitOfWork.Save();
        }

        public void Delete(SaleDTO item)
        {
            Sale sale = mapper.Map<Sale>(item);
            unitOfWork.salesRepository.Delete(sale);
            unitOfWork.Save();
        }
    }
}
