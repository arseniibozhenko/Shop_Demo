using Shop_Demo.DAL.Context;
using Shop_Demo.DAL.Repositories;
using Shop_Demo.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop_Demo.WebApi.Controllers
{
    public class CategoriesController : ApiController
    {
        IRepository<Category> categoriesRepository = new CategoriesRepository(new ShopAdoEntities());

        //api/Categories
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Category> categories = categoriesRepository.GetAll();
            List<CategoryDTO> categoriesDTO = categories.Select(c => new CategoryDTO()
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }).ToList();

            if (categoriesDTO.Count == 0)
            {
                return NotFound();
            }

            return Ok(categoriesDTO);
        }

        //api/Categories/id
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            Category category = categoriesRepository.GetById(id);
            if (category != null)
            {
                CategoryDTO categoryDTO = new CategoryDTO()
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                };

                return Ok(categoryDTO);
            }
            else
            {
                return NotFound();
            }
        }

        //api/Categories
        [HttpPost]
        public IHttpActionResult Post([FromBody]CategoryDTO categoryDTO)
        {
            Category category = new Category()
            {
                CategoryId = categoryDTO.CategoryId,
                CategoryName = categoryDTO.CategoryName
            };

            categoriesRepository.CreateOrUpdate(category);

            return Ok();
        }

        //api/Categories/id
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]CategoryDTO categoryDTO)
        {
            Category category = categoriesRepository.GetById(id);
            if (category != null)
            {
                category.CategoryId = categoryDTO.CategoryId;
                category.CategoryName = categoryDTO.CategoryName;

                categoriesRepository.CreateOrUpdate(category);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        //api/Categories/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            categoriesRepository.Delete(id);

            return Ok();
        }
    }
}
