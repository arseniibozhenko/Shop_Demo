using Shop_Demo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_Demo.DAL.Repositories;
using Shop_Demo.ViewModels;
using Shop_Demo.BLL.Services;
using Shop_Demo.BLL.DTO;
using AutoMapper;
using Shop_Demo.Mapping;

namespace Shop_Demo.Controllers
{
    public class ManufacturersController : Controller
    {
        private IEntityService<ManufacturerDTO> manufacturersService;

        //Количество производителей на одной странице
        private const int MANUFACTURERS_PER_PAGE = 5;

        private IMapper mapper;

        public ManufacturersController(IEntityService<ManufacturerDTO> manufacturersService)
        {
            this.manufacturersService = manufacturersService;

            //Настройка AutoMapper
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<ViewModelToDTOProfile>();
                cfg.AddProfile<DTOToViewModelProfile>();
            });
            mapper = config.CreateMapper();
        }

        public ActionResult Index(int id = 1)
        {
            return View();
        }

        public PartialViewResult Table(int id = 1)
        {
            //Номер страницы
            ViewBag.Id = id;

            //Список всех производителей
            List<ManufacturerViewModel> manufacturers = mapper.Map<List<ManufacturerViewModel>>(manufacturersService.GetAll());
            //Количество страниц с производителями
            int pageCount = (int)Math.Ceiling(manufacturers.Count() / (double)MANUFACTURERS_PER_PAGE);
            //Список производителей с пагинацией
            IEnumerable<ManufacturerViewModel> newManufacturers = manufacturers.Skip((id - 1) * MANUFACTURERS_PER_PAGE).Take(MANUFACTURERS_PER_PAGE).ToList();
            //Общее количество производителей
            ViewBag.ManufacturersCount = manufacturers.Count();

            //Номера первого и последнего производителей в пагинации
            ViewBag.From = manufacturers.IndexOf(newManufacturers.First()) + 1;
            ViewBag.To = manufacturers.IndexOf(newManufacturers.Last()) + 1;

            ViewBag.PageCount = pageCount;
            return PartialView(newManufacturers);
        }

        [HttpGet]
        public ActionResult Remove(int id = 1)
        {
            ManufacturerDTO manufacturerDTO = manufacturersService.GetById(id);
            ManufacturerViewModel manufacturer = mapper.Map<ManufacturerViewModel>(manufacturerDTO);
            return PartialView(manufacturer);
        }

        [HttpPost, ActionName("Remove")]
        public ActionResult RemoveConfirmed(int id = 1)
        {
            //Удаление 
            manufacturersService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create(int id = 0)
        {
            //Создание модели
            ManufacturerViewModel model = new ManufacturerViewModel();

            //Получение id
            ViewBag.Id = id;

            if (id == 0)
            {
                return PartialView(model);
            }
            else
            {
                //Поиск производителя
                ManufacturerDTO manufacturerDTO = manufacturersService.GetById(id);

                //Перенос на модель представления
                model.ManufacturerId = manufacturerDTO.ManufacturerId;
                model.ManufacturerName = manufacturerDTO.ManufacturerName;

                return PartialView(model);
            }
        }

        [HttpPost]
        public ActionResult Create(ManufacturerViewModel model)
        {
            ManufacturerDTO newManufacturer = new ManufacturerDTO()
            {
                ManufacturerId = (model.ManufacturerId.HasValue ? model.ManufacturerId.Value : 0),
                ManufacturerName = model.ManufacturerName,
            };

            //Добавление
            manufacturersService.CreateOrUpdate(newManufacturer);

            return RedirectToAction("Index");
        }
    }
}