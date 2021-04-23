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
    public class SalePosesController : ApiController
    {
        IRepository<SalePos> salePosesRepository = new SalePosesRepository(new ShopAdoEntities());

        //api/SalePoses
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<SalePos> salePoses = salePosesRepository.GetAll();
            List<SalePosDTO> salePosesDTO = salePoses.Select(s => new SalePosDTO()
            {
                SalePosId = s.SalePosId,
                CountGood = s.CountGood,
                GoodId = s.GoodId,
                SaleId = s.SaleId,
                Summa = s.Summa
            }).ToList();

            if (salePosesDTO.Count == 0)
            {
                return NotFound();
            }

            return Ok(salePosesDTO);
        }

        //api/SalePoses/id
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            SalePos salePos = salePosesRepository.GetById(id);
            if (salePos != null)
            {
                SalePosDTO salePosDTO = new SalePosDTO()
                {
                    SalePosId = salePos.SalePosId,
                    CountGood = salePos.CountGood,
                    GoodId = salePos.GoodId,
                    SaleId = salePos.SaleId,
                    Summa = salePos.Summa
                };

                return Ok(salePosDTO);
            }
            else
            {
                return NotFound();
            }
        }

        //api/SalePoses
        [HttpPost]
        public IHttpActionResult Post([FromBody]SalePosDTO salePosDTO)
        {
            SalePos salePos = new SalePos()
            {
                SalePosId = salePosDTO.SalePosId,
                CountGood = salePosDTO.CountGood,
                GoodId = salePosDTO.GoodId,
                SaleId = salePosDTO.SaleId,
                Summa = salePosDTO.Summa
            };

            salePosesRepository.CreateOrUpdate(salePos);

            return Ok();
        }

        //api/SalePoses/id
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]SalePosDTO salePosDTO)
        {
            SalePos salePos = salePosesRepository.GetById(id);
            if (salePos != null)
            {
                salePos.SalePosId = salePosDTO.SalePosId;
                salePos.CountGood = salePosDTO.CountGood;
                salePos.GoodId = salePosDTO.GoodId;
                salePos.SaleId = salePosDTO.SaleId;
                salePos.Summa = salePosDTO.Summa;

                salePosesRepository.CreateOrUpdate(salePos);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        //api/SalePoses/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            salePosesRepository.Delete(id);

            return Ok();
        }
    }
}
