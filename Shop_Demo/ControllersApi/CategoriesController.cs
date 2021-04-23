using Shop_Demo.BLL.DTO;
using Shop_Demo.BLL.Services;
using Shop_Demo.DAL.Context;
using Shop_Demo.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop_Demo.ControllersApi
{
    public class CategoriesController : ApiController
    {
        private IEntityService<CategoryDTO> categoriesService;

        public CategoriesController(IEntityService<CategoryDTO> categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        //api/Categories
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<CategoryDTO> categoriesDTO = categoriesService.GetAll();

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

            CategoryDTO categoryDTO = categoriesService.GetById(id);
            if (categoryDTO != null)
            {
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
            categoriesService.CreateOrUpdate(categoryDTO);

            return Ok();
        }

        //api/Categories/id
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]CategoryDTO categoryDTO)
        {
            CategoryDTO category = categoriesService.GetById(id);
            if (category != null)
            {
                category.CategoryId = categoryDTO.CategoryId;
                category.CategoryName = categoryDTO.CategoryName;

                categoriesService.CreateOrUpdate(category);
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

            categoriesService.Delete(id);

            return Ok();
        }
    }
}
