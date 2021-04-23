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
    public class ManufacturersController : ApiController
    {
        private IEntityService<ManufacturerDTO> manufacturersService;

        public ManufacturersController(IEntityService<ManufacturerDTO> manufacturersService)
        {
            this.manufacturersService = manufacturersService;
        }

        //api/Manufacturers
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<ManufacturerDTO> manufacturersDTO = manufacturersService.GetAll();

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

            ManufacturerDTO manufacturerDTO = manufacturersService.GetById(id);
            if (manufacturerDTO != null)
            {
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
            manufacturersService.CreateOrUpdate(manufacturerDTO);

            return Ok();
        }

        //api/Manufacturers/id
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]ManufacturerDTO manufacturerDTO)
        {
            ManufacturerDTO manufacturer = manufacturersService.GetById(id);
            if (manufacturer != null)
            {
                manufacturer.ManufacturerId = manufacturerDTO.ManufacturerId;
                manufacturer.ManufacturerName = manufacturerDTO.ManufacturerName;

                manufacturersService.CreateOrUpdate(manufacturer);
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

            manufacturersService.Delete(id);

            return Ok();
        }
    }
}
