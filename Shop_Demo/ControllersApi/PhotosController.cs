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
    public class PhotosController : ApiController
    {
        private IEntityService<PhotoDTO> photosService;

        public PhotosController(IEntityService<PhotoDTO> photosService)
        {
            this.photosService = photosService;
        }

        //api/Photos
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<PhotoDTO> photosDTO = photosService.GetAll();

            if (photosDTO.Count == 0)
            {
                return NotFound();
            }

            return Ok(photosDTO);
        }

        //api/Photos/id
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            PhotoDTO photoDTO = photosService.GetById(id);
            if (photoDTO != null)
            {
                return Ok(photoDTO);
            }
            else
            {
                return NotFound();
            }
        }

        //api/Photos
        [HttpPost]
        public IHttpActionResult Post([FromBody]PhotoDTO photoDTO)
        {
            photosService.CreateOrUpdate(photoDTO);

            return Ok();
        }

        //api/Photos/id
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]PhotoDTO photoDTO)
        {
            PhotoDTO photo = photosService.GetById(id);
            if (photo != null)
            {
                photo.PhotoId = photoDTO.PhotoId;
                photo.GoodId = photoDTO.GoodId;
                photo.PhotoPath = photoDTO.PhotoPath;

                photosService.CreateOrUpdate(photo);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        //api/Photos/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            photosService.Delete(id);

            return Ok();
        }
    }
}
