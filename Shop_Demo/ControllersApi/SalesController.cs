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
    public class SalesController : ApiController
    {
        private IEntityService<SaleDTO> salesService;

        public SalesController(IEntityService<SaleDTO> salesService)
        {
            this.salesService = salesService;
        }

        //api/Sales
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<SaleDTO> salesDTO = salesService.GetAll();

            if (salesDTO.Count == 0)
            {
                return NotFound();
            }

            return Ok(salesDTO);
        }

        //api/Sales/id
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            SaleDTO saleDTO = salesService.GetById(id);
            if (saleDTO != null)
            {
                return Ok(saleDTO);
            }
            else
            {
                return NotFound();
            }
        }

        //api/Sales
        [HttpPost]
        public IHttpActionResult Post([FromBody]SaleDTO saleDTO)
        {
            salesService.CreateOrUpdate(saleDTO);

            return Ok();
        }

        //api/Sales/id
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]SaleDTO saleDTO)
        {
            SaleDTO sale = salesService.GetById(id);
            if (sale != null)
            {
                sale.SaleId = saleDTO.SaleId;
                sale.DateSale = saleDTO.DateSale;
                sale.NumberSale = saleDTO.NumberSale;
                sale.Summa = saleDTO.Summa;
                sale.UserEmail = saleDTO.UserEmail;
                sale.UserPhone = saleDTO.UserPhone;

                salesService.CreateOrUpdate(sale);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        //api/Sales/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            salesService.Delete(id);

            return Ok();
        }
    }
}
