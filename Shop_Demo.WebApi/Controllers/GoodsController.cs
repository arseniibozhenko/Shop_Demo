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
    public class GoodsController : ApiController
    {
        IRepository<Good> goodsRepository = new GoodsRepository(new ShopAdoEntities());
        IRepository<Manufacturer> manufacturersRepository = new ManufacturersRepository(new ShopAdoEntities());
        IRepository<Category> categoriesRepository = new CategoriesRepository(new ShopAdoEntities());

        //api/Goods
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Good> goods = goodsRepository.GetAll();
            //List<GoodDTO> goodsDTO = new List<GoodDTO>();
            //foreach(Good g in goods)
            //{
            //    goodsDTO.Add(new GoodDTO()
            //    {
            //        GoodId = g.GoodId,
            //        GoodName = g.GoodName,
            //        ManufacturerId = g.ManufacturerId,
            //        CategoryId = g.CategoryId,
            //        Price = g.Price,
            //        GoodCount = g.GoodCount
            //    });
            //}

            List<GoodDTO> goodsDTO = goods.Select(g => new GoodDTO()
            {
                GoodId = g.GoodId,
                GoodName = g.GoodName,
                ManufacturerId = g.ManufacturerId,
                CategoryId = g.CategoryId,
                Price = g.Price,
                GoodCount = g.GoodCount
            }).ToList();

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

            Good good = goodsRepository.GetById(id);
            if (good != null)
            {
                GoodDTO goodDTO = new GoodDTO()
                {
                    GoodId = good.GoodId,
                    GoodName = good.GoodName,
                    ManufacturerId = good.ManufacturerId,
                    CategoryId = good.CategoryId,
                    Price = good.Price,
                    GoodCount = good.GoodCount
                };

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
            Good good = new Good()
            {
                GoodName = goodDTO.GoodName,
                ManufacturerId = goodDTO.ManufacturerId,
                CategoryId = goodDTO.CategoryId,
                Price = goodDTO.Price,
                GoodCount = goodDTO.GoodCount
            };

            goodsRepository.CreateOrUpdate(good);

            return Ok();
        }

        //api/Goods/id
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]GoodDTO goodDTO)
        {
            Good good = goodsRepository.GetById(id);
            if (good != null)
            {
                good.GoodName = goodDTO.GoodName;
                good.ManufacturerId = goodDTO.ManufacturerId;
                good.CategoryId = goodDTO.CategoryId;
                good.Price = goodDTO.Price;
                good.GoodCount = goodDTO.GoodCount;

                goodsRepository.CreateOrUpdate(good);
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

            goodsRepository.Delete(id);

            return Ok();
        }

        [HttpGet]
        [Route("api/v1/Goods/Manufacturers")]
        public IHttpActionResult GetManufacturers()
        {
            List<Manufacturer> manufacturers = manufacturersRepository.GetAll();
            List<ManufacturerDTO> manufacturersDTO = manufacturers.Select(m => new ManufacturerDTO()
            {
                ManufacturerId = m.ManufacturerId,
                ManufacturerName = m.ManufacturerName
            }).ToList();

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

            List<Good> goods = goodsRepository.GetBy(g => g.ManufacturerId == id);
            List<GoodDTO> goodsDTO = goods.Select(g => new GoodDTO()
            {
                GoodId = g.GoodId,
                GoodName = g.GoodName,
                ManufacturerId = g.ManufacturerId,
                CategoryId = g.CategoryId,
                Price = g.Price,
                GoodCount = g.GoodCount
            }).ToList();

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

        [HttpGet]
        [Route("api/v1/Goods/Categories/{id:int}")]
        public IHttpActionResult GetGoodsWithCategory(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            List<Good> goods = goodsRepository.GetBy(g => g.CategoryId == id);
            List<GoodDTO> goodsDTO = goods.Select(g => new GoodDTO()
            {
                GoodId = g.GoodId,
                GoodName = g.GoodName,
                ManufacturerId = g.ManufacturerId,
                CategoryId = g.CategoryId,
                Price = g.Price,
                GoodCount = g.GoodCount
            }).ToList();

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

            List<Category> categories = goodsRepository.GetBy(g => g.ManufacturerId == id).Select(g => g.Category)
                                                                                          .Where(g => g != null)
                                                                                          .Distinct().ToList();
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
    }
}
