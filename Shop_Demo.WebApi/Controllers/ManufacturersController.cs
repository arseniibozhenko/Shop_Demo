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
    public class ManufacturersController : ApiController
    {
        IRepository<Manufacturer> manufacturersRepository = new ManufacturersRepository(new ShopAdoEntities());

        //api/Manufacturers
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Manufacturer> manufacturers = manufacturersRepository.GetAll();
            List<ManufacturerDTO> manufacturersDTO = manufacturers.Select(m => new ManufacturerDTO()
            {
                ManufacturerId = m.ManufacturerId,
                ManufacturerName = m.ManufacturerName,
            }).ToList();

            if (manufacturersDTO.Count == 0)
            {
                return NotFound();
            }

            return Ok(manufacturersDTO);
        }

        //api/Manufacturers/id
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            Manufacturer manufacturer = manufacturersRepository.GetById(id);
            if (manufacturer != null)
            {
                ManufacturerDTO manufacturerDTO = new ManufacturerDTO()
                {
                    ManufacturerId = manufacturer.ManufacturerId,
                    ManufacturerName = manufacturer.ManufacturerName,
                };

                return Ok(manufacturerDTO);
            }
            else
            {
                return NotFound();
            }
        }

        //api/Manufacturers
        [HttpPost]
        public IHttpActionResult Post([FromBody]ManufacturerDTO manufacturerDTO)
        {
            Manufacturer manufacturer = new Manufacturer()
            {
                ManufacturerName = manufacturerDTO.ManufacturerName,
                ManufacturerId = manufacturerDTO.ManufacturerId,
            };

            manufacturersRepository.CreateOrUpdate(manufacturer);

            return Ok();
        }

        //api/Manufacturers/id
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]ManufacturerDTO manufacturerDTO)
        {
            Manufacturer manufacturer = manufacturersRepository.GetById(id);
            if (manufacturer != null)
            {
                manufacturer.ManufacturerName = manufacturerDTO.ManufacturerName;
                manufacturer.ManufacturerId = manufacturerDTO.ManufacturerId;

                manufacturersRepository.CreateOrUpdate(manufacturer);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        //api/Manufacturers/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            manufacturersRepository.Delete(id);

            return Ok();
        }

    }
}
