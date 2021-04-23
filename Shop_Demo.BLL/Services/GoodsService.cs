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
    public class GoodsService : IEntityService<GoodDTO>
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public GoodsService(IUnitOfWork unitOfWork)
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

        public List<GoodDTO> GetAll()
        {
            List<Good> goods = unitOfWork.goodsRepository.GetAll();
            //List<GoodDTO> goodsDTO = mapper.Map<List<Good>, List<GoodDTO>>(goods);
            List<GoodDTO> goodsDTO = goods.Select(g => mapper.Map<GoodDTO>(g)).ToList();

            return goodsDTO;
        }

        public GoodDTO GetById(int id)
        {
            Good good = unitOfWork.goodsRepository.GetById(id);
            GoodDTO goodDTO = mapper.Map<GoodDTO>(good);

            return goodDTO;
        }

        public List<GoodDTO> GetBy(Expression<Func<GoodDTO, bool>> predicate)
        {
            Expression<Func<Good, bool>> p = mapper.Map<Expression<Func<Good, bool>>>(predicate);
            List<Good> goods = unitOfWork.goodsRepository.GetBy(p);
            //List<GoodDTO> goodsDTO = mapper.Map<List<Good>, List<GoodDTO>>(goods);
            List<GoodDTO> goodsDTO = goods.Select(g => mapper.Map<GoodDTO>(g)).ToList();

            return goodsDTO;
        }

        public void CreateOrUpdate(GoodDTO item)
        {
            Good good = mapper.Map<Good>(item);
            unitOfWork.goodsRepository.CreateOrUpdate(good);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            unitOfWork.goodsRepository.Delete(id);
            unitOfWork.Save();
        }

        public void Delete(GoodDTO item)
        {
            Good good = mapper.Map<Good>(item);
            unitOfWork.goodsRepository.Delete(good);
            unitOfWork.Save();
        }


    }
}
