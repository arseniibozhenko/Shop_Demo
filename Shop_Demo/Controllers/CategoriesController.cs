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
    public class CategoriesController : Controller
    {
        private IEntityService<CategoryDTO> categoriesService;

        //Количество категорий на одной странице
        private const int CATEGORIES_PER_PAGE = 5;

        private IMapper mapper;

        public CategoriesController(IEntityService<CategoryDTO> categoriesService)
        {
            this.categoriesService = categoriesService;

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

            //Список всех категорий
            List<CategoryViewModel> categories = mapper.Map<List<CategoryViewModel>>(categoriesService.GetAll());
            //Количество страниц с категориями
            int pageCount = (int)Math.Ceiling(categories.Count() / (double)CATEGORIES_PER_PAGE);
            //Список категорий с пагинацией
            IEnumerable<CategoryViewModel> newCategories = categories.Skip((id - 1) * CATEGORIES_PER_PAGE).Take(CATEGORIES_PER_PAGE).ToList();
            //Общее количество категорий
            ViewBag.CategoriesCount = categories.Count();

            //Номера первого и последнего категорий в пагинации
            ViewBag.From = categories.IndexOf(newCategories.First()) + 1;
            ViewBag.To = categories.IndexOf(newCategories.Last()) + 1;

            ViewBag.PageCount = pageCount;
            return PartialView(newCategories);
        }

        [HttpGet]
        public ActionResult Remove(int id = 1)
        {
            CategoryDTO categoryDTO = categoriesService.GetById(id);
            CategoryViewModel category = mapper.Map<CategoryViewModel>(categoryDTO);
            return PartialView(category);
        }

        [HttpPost, ActionName("Remove")]
        public ActionResult RemoveConfirmed(int id = 1)
        {
            //Удаление 
            categoriesService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create(int id = 0)
        {
            //Создание модели
            CategoryViewModel model = new CategoryViewModel();

            //Получение id
            ViewBag.Id = id;

            if (id == 0)
            {
                return PartialView(model);
            }
            else
            {
                //Поиск категории
                CategoryDTO categoryDTO = categoriesService.GetById(id);

                //Перенос на модель представления
                model.CategoryId = categoryDTO.CategoryId;
                model.CategoryName = categoryDTO.CategoryName;

                return PartialView(model);
            }
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            CategoryDTO newCategory = new CategoryDTO()
            {
                CategoryId = (model.CategoryId.HasValue ? model.CategoryId.Value : 0),
                CategoryName = model.CategoryName,
            };

            //Добавление
            categoriesService.CreateOrUpdate(newCategory);

            return RedirectToAction("Index");
        }
    }
}