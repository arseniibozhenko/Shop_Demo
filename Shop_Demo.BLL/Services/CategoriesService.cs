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
    public class CategoriesService : IEntityService<CategoryDTO>
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public CategoriesService(IUnitOfWork unitOfWork)
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

        public List<CategoryDTO> GetAll()
        {
            List<Category> categories = unitOfWork.categoriesRepository.GetAll();
            //List<CategoryDTO> categoriesDTO = mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            List<CategoryDTO> categoriesDTO = categories.Select(g => mapper.Map<CategoryDTO>(g)).ToList();

            return categoriesDTO;
        }

        public CategoryDTO GetById(int id)
        {
            Category category = unitOfWork.categoriesRepository.GetById(id);
            CategoryDTO categoryDTO = mapper.Map<CategoryDTO>(category);

            return categoryDTO;
        }

        public List<CategoryDTO> GetBy(Expression<Func<CategoryDTO, bool>> predicate)
        {
            Expression<Func<Category, bool>> p = mapper.Map<Expression<Func<Category, bool>>>(predicate);
            List<Category> categories = unitOfWork.categoriesRepository.GetBy(p);
            //List<CategoryDTO> categoriesDTO = mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            List<CategoryDTO> categoriesDTO = categories.Select(g => mapper.Map<CategoryDTO>(g)).ToList();

            return categoriesDTO;
        }

        public void CreateOrUpdate(CategoryDTO item)
        {
            Category category = mapper.Map<Category>(item);
            unitOfWork.categoriesRepository.CreateOrUpdate(category);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            unitOfWork.categoriesRepository.Delete(id);
            unitOfWork.Save();
        }

        public void Delete(CategoryDTO item)
        {
            Category category = mapper.Map<Category>(item);
            unitOfWork.categoriesRepository.Delete(category);
            unitOfWork.Save();
        }
    }
}
