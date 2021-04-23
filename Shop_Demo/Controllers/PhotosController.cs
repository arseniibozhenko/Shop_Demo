using Shop_Demo.DAL.Context;
using Shop_Demo.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Demo.Controllers
{
    public class PhotosController : Controller
    {
        private IRepository<Photo> photosRepository;

        //Количество фотографий на одной странице
        const int PHOTOS_PER_PAGE = 5;

        public PhotosController(IRepository<Photo> photosRepository)
        {
            this.photosRepository = photosRepository;
        }

        public ActionResult Index(int id = 1)
        {
            return View();
        }

        public PartialViewResult Table(int id = 1)
        {
            //Номер страницы
            ViewBag.Id = id;

            //Список всех фотографий
            List<Photo> photos = photosRepository.GetAll();
            //Количество страниц с фотографиями
            int pageCount = (int)Math.Ceiling(photos.Count() / (double)PHOTOS_PER_PAGE);
            //Список фотографий с пагинацией
            IEnumerable<Photo> newPhotos = photos.Skip((id - 1) * PHOTOS_PER_PAGE).Take(PHOTOS_PER_PAGE).ToList();
            //Общее количество фотографий
            ViewBag.PhotosCount = photos.Count();

            //Номера первой и последней фотографий в пагинации
            ViewBag.From = newPhotos.Count() == 0 ? 0 : photos.IndexOf(newPhotos.First()) + 1;
            ViewBag.To = newPhotos.Count() == 0 ? 0 : photos.IndexOf(newPhotos.Last()) + 1;

            ViewBag.PageCount = pageCount;
            return PartialView(newPhotos);
        }
    }
}