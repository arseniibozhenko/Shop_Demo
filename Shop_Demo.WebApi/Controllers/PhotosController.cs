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
    public class PhotosController : ApiController
    {
        IRepository<Photo> photosRepository = new PhotosRepository(new ShopAdoEntities());

        //api/Photos
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Photo> photos = photosRepository.GetAll();
            List<PhotoDTO> photosDTO = photos.Select(p => new PhotoDTO()
            {
                PhotoId = p.PhotoId,
                GoodId = p.GoodId,
                PhotoPath = p.PhotoPath
            }).ToList();

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

            Photo photo = photosRepository.GetById(id);
            if (photo != null)
            {
                PhotoDTO photoDTO = new PhotoDTO()
                {
                    PhotoId = photo.PhotoId,
                    GoodId = photo.GoodId,
                    PhotoPath = photo.PhotoPath
                };

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
            Photo photo = new Photo()
            {
                PhotoId = photoDTO.PhotoId,
                GoodId = photoDTO.GoodId,
                PhotoPath = photoDTO.PhotoPath
            };

            photosRepository.CreateOrUpdate(photo);

            return Ok();
        }

        //api/Photos/id
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]PhotoDTO photoDTO)
        {
            Photo photo = photosRepository.GetById(id);
            if (photo != null)
            {
                photo.PhotoId = photoDTO.PhotoId;
                photo.GoodId = photoDTO.GoodId;
                photo.PhotoPath = photoDTO.PhotoPath;

                photosRepository.CreateOrUpdate(photo);
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

            photosRepository.Delete(id);

            return Ok();
        }
    }
}
