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
    public class GoodsController : ApiController
    {
        private IEntityService<GoodDTO> goodsService;
        private IEntityService<ManufacturerDTO> manufacturersService;
        private IEntityService<CategoryDTO> categoriesService;

        public GoodsController(IEntityService<GoodDTO> goodsService, IEntityService<ManufacturerDTO> manufacturersService, IEntityService<CategoryDTO> categoriesService)
        {
            this.goodsService = goodsService;
            this.manufacturersService = manufacturersService;
            this.categoriesService = categoriesService;
        }

        //api/Goods
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<GoodDTO> goodsDTO = goodsService.GetAll();

            if (goodsDTO.Count == 0)
            {
                return NotFound();
            }

            return Ok(goodsDTO);
        }

        //api/Goods/id
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            GoodDTO goodDTO = goodsService.GetById(id);
            if (goodDTO != null)
            {
                return Ok(goodDTO);
            }
            else
            {
                return NotFound();
            }
        }

        //api/Goods
        [HttpPost]
        public IHttpActionResult Post([FromBody]GoodDTO goodDTO)
        {
            goodsService.CreateOrUpdate(goodDTO);

            return Ok();
        }

        //api/Goods/id
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]GoodDTO goodDTO)
        {
            GoodDTO good = goodsService.GetById(id);
            if (good != null)
            {
                good.GoodName = goodDTO.GoodName;
                good.ManufacturerId = goodDTO.ManufacturerId;
                good.CategoryId = goodDTO.CategoryId;
                good.Price = goodDTO.Price;
                good.GoodCount = goodDTO.GoodCount;

                goodsService.CreateOrUpdate(good);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        //api/Goods/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            goodsService.Delete(id);

            return Ok();
        }

        [HttpGet]
        [Route("api/v1/Goods/Manufacturers")]
        public IHttpActionResult GetManufacturers()
        {
            List<ManufacturerDTO> manufacturersDTO = manufacturersService.GetAll();

            if (manufacturersDTO.Count == 0)
            {
                return NotFound();
            }

            return Ok(manufacturersDTO);
        }

        [HttpGet]
        [Route("api/v1/Goods/Manufacturers/{id:int}")]
        public IHttpActionResult GetGoodsWithManufacturer(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            List<GoodDTO> goodsDTO = goodsService.GetBy(g => g.ManufacturerId == id);

            if (goodsDTO.Count == 0)
            {
                return NotFound();
            }

            return Ok(goodsDTO);
        }

        [HttpGet]
        [Route("api/v1/Goods/Categories")]
        public IHttpActionResult GetCategories()
        {
            List<CategoryDTO> categoriesDTO = categoriesService.GetAll();

            if (categoriesDTO.Count == 0)
            {
                return NotFound();
            }

            return Ok(categoriesDTO);
        }

        [HttpGet]
        [Route("api/v1/Goods/Categories/{id:int}")]
        public IHttpActionResult GetGoodsWithCategory(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            List<GoodDTO> goodsDTO = goodsService.GetBy(g => g.CategoryId == id);

            if (goodsDTO.Count == 0)
            {
                return NotFound();
            }

            return Ok(goodsDTO);
        }

        [HttpGet]
        [Route("api/v1/Goods/Manufacturers/{id}/Categories")]
        public IHttpActionResult GetManufacturersWithCategory(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            List<CategoryDTO> categoriesDTO = goodsService.GetBy(g => g.ManufacturerId == id).Select(g => g.Category)
                                                                                             .Where(g => g != null)
                                                                                             .Distinct().ToList();

            if (categoriesDTO.Count == 0)
            {
                return NotFound();
            }

            return Ok(categoriesDTO);
        }
    }
}
