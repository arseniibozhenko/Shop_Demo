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
    public class SalePosesController : ApiController
    {
        private IEntityService<SalePosDTO> salePosesService;

        public SalePosesController(IEntityService<SalePosDTO> salePosesService)
        {
            this.salePosesService = salePosesService;
        }

        //api/SalePoses
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<SalePosDTO> salePosesDTO = salePosesService.GetAll();

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

            SalePosDTO salePosDTO = salePosesService.GetById(id);
            if (salePosDTO != null)
            {
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
            salePosesService.CreateOrUpdate(salePosDTO);

            return Ok();
        }

        //api/SalePoses/id
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]SalePosDTO salePosDTO)
        {
            SalePosDTO salePos = salePosesService.GetById(id);
            if (salePos != null)
            {
                salePos.SalePosId = salePosDTO.SalePosId;
                salePos.CountGood = salePosDTO.CountGood;
                salePos.GoodId = salePosDTO.GoodId;
                salePos.SaleId = salePosDTO.SaleId;
                salePos.Summa = salePosDTO.Summa;

                salePosesService.CreateOrUpdate(salePos);
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

            salePosesService.Delete(id);

            return Ok();
        }
    }
}
