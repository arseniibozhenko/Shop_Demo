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
    public class SalesController : ApiController
    {
        IRepository<Sale> salesRepository = new SalesRepository(new ShopAdoEntities());

        //api/Sales
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Sale> sales = salesRepository.GetAll();
            List<SaleDTO> salesDTO = sales.Select(s => new SaleDTO()
            {
                SaleId = s.SaleId,
                DateSale = s.DateSale,
                NumberSale = s.NumberSale,
                Summa = s.Summa,
                UserEmail = s.UserEmail,
                UserPhone = s.UserPhone
            }).ToList();

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

            Sale sale = salesRepository.GetById(id);
            if (sale != null)
            {
                SaleDTO saleDTO = new SaleDTO()
                {
                    SaleId = sale.SaleId,
                    DateSale = sale.DateSale,
                    NumberSale = sale.NumberSale,
                    Summa = sale.Summa,
                    UserEmail = sale.UserEmail,
                    UserPhone = sale.UserPhone
                };

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
            Sale sale = new Sale()
            {
                SaleId = saleDTO.SaleId,
                DateSale = saleDTO.DateSale,
                NumberSale = saleDTO.NumberSale,
                Summa = saleDTO.Summa,
                UserEmail = saleDTO.UserEmail,
                UserPhone = saleDTO.UserPhone
            };

            salesRepository.CreateOrUpdate(sale);

            return Ok();
        }

        //api/Sales/id
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]SaleDTO saleDTO)
        {
            Sale sale = salesRepository.GetById(id);
            if (sale != null)
            {
                sale.SaleId = saleDTO.SaleId;
                sale.DateSale = saleDTO.DateSale;
                sale.NumberSale = saleDTO.NumberSale;
                sale.Summa = saleDTO.Summa;
                sale.UserEmail = saleDTO.UserEmail;
                sale.UserPhone = saleDTO.UserPhone;

                salesRepository.CreateOrUpdate(sale);
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

            salesRepository.Delete(id);

            return Ok();
        }
    }
}
