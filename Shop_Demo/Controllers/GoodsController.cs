using AutoMapper;
using LinqKit;
using Shop_Demo.BLL.DTO;
using Shop_Demo.BLL.Services;
using Shop_Demo.DAL.Context;
using Shop_Demo.DAL.Repositories;
using Shop_Demo.Mapping;
using Shop_Demo.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Demo.Controllers
{
    [AllowAnonymous]
    public class GoodsController : Controller
    {
        private IEntityService<GoodDTO> goodsService;
        private IEntityService<ManufacturerDTO> manufacturersService;
        private IEntityService<CategoryDTO> categoriesService;
        private IEntityService<PhotoDTO> photosService;

        //Количество товаров на одной странице
        private const int GOODS_PER_PAGE = 5;

        //private static List<Good> goods;

        //Создание модели для представления и фильтрации
        private static GoodsViewModel goodsModel = new GoodsViewModel()
        {
            PriceFilter = new PriceFilter()
        };

        private IMapper mapper;

        public GoodsController(IEntityService<GoodDTO> goodsService, IEntityService<ManufacturerDTO> manufacturersService, IEntityService<CategoryDTO> categoriesService, IEntityService<PhotoDTO> photosService)
        {
            this.goodsService = goodsService;
            this.manufacturersService = manufacturersService;
            this.categoriesService = categoriesService;
            this.photosService = photosService;

            //Настройка AutoMapper
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<ViewModelToDTOProfile>();
                cfg.AddProfile<DTOToViewModelProfile>();
            });
            mapper = config.CreateMapper();
        }

        [HttpGet]
        public ActionResult Index(string Sort, string Search, int ManufacturerId = 0, int CategoryId = 0, decimal From = -1, decimal To = 0)
        {
            //goods = goodsRepository.GetAll();
            //if (ManufacturerId != 0)
            //{
            //    goods = goods.Where(g => g.ManufacturerId == ManufacturerId).ToList();
            //}
            //if (CategoryId != 0)
            //{
            //    goods = goods.Where(g => g.CategoryId == CategoryId).ToList();
            //}
            //goodsModel.ManufacturerId = ManufacturerId;
            //goodsModel.CategoryId = CategoryId;

            //goods = goodsRepository.GetAll();
            var predicate = PredicateBuilder.New<GoodDTO>(true);
            if (!String.IsNullOrEmpty(Search))
            {
                predicate.And(g => g.GoodName.Contains(Search));
            }
            if (ManufacturerId != 0)
            {
                predicate.And(g => g.ManufacturerId == ManufacturerId);
            }
            if (CategoryId != 0)
            {
                predicate.And(g => g.CategoryId == CategoryId);
            }

            //Для отображения фильтров
            goodsModel.ManufacturerId = ManufacturerId;
            goodsModel.CategoryId = CategoryId;
            goodsModel.SortItem = Sort;
            goodsModel.Search = Search;

            //Получение списка товаров с фильтрами
            List<GoodDTO> goodsDTO = goodsService.GetBy(predicate);
            goodsModel.Goods = mapper.Map<IEnumerable<GoodViewModel>>(goodsDTO);

            //Получение минимальной и максимальной цен без учета фильтрации по цене
            goodsModel.PriceFilter.Min = Convert.ToDecimal(Convert.ToDouble(goodsModel.Goods.OrderBy(g => g.Price).FirstOrDefault().Price));
            goodsModel.PriceFilter.Max = Convert.ToDecimal(Convert.ToDouble(goodsModel.Goods.OrderBy(g => g.Price).LastOrDefault().Price));

            //Фильтр по цене
            if (From != -1 && To != 0)
            {
                predicate.And(g => g.Price >= From);
                predicate.And(g => g.Price <= To);
            }

            //Получение списка товаров с фильтрами и ценой
            goodsDTO = goodsService.GetBy(predicate);
            goodsModel.Goods = mapper.Map<IEnumerable<GoodViewModel>>(goodsDTO);

            //Сортировка
            switch (Sort)
            {
                case "Newest":
                    goodsModel.Goods = goodsModel.Goods.OrderByDescending(g => g.GoodId);
                    break;
                case "Cheap":
                    goodsModel.Goods = goodsModel.Goods.OrderBy(g => g.Price);
                    break;
                case "Expensive":
                    goodsModel.Goods = goodsModel.Goods.OrderByDescending(g => g.Price);
                    break;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(GoodsViewModel model)
        {
            //Post метод для генерации более удобного адреса, при get методе идут null значения
            //if (model.PriceFilter.From == Convert.ToDecimal(Convert.ToDouble(model.PriceFilter.Min)))
            //{
            //    model.PriceFilter.From = null;
            //}
            //if (model.PriceFilter.To == Convert.ToDecimal(Convert.ToDouble(model.PriceFilter.Max)))
            //{
            //    model.PriceFilter.To = null;
            //}
            return RedirectToAction("Index", new { Sort = model.SortItem, model.Search, model.ManufacturerId, model.CategoryId, model.PriceFilter?.From, model.PriceFilter?.To } );
        }

        [HttpGet]
        public PartialViewResult Table(int id = 1)
        {
            //Номер страницы
            ViewBag.Id = id;

            //Список всех товаров
            List<GoodViewModel> goods = goodsModel.Goods.ToList();
            //Количество страниц с товарами
            int pageCount = (int)Math.Ceiling(goods.Count() / (double)GOODS_PER_PAGE);
            //Список товаров с пагинацией
            IEnumerable<GoodViewModel> newGoods = goods.Skip((id - 1) * GOODS_PER_PAGE).Take(GOODS_PER_PAGE).ToList();
            //Общее количество товаров
            ViewBag.GoodsCount = goodsModel.Goods.Count();

            //Номера первого и последнего товаров в пагинации
            ViewBag.From = goods.IndexOf(newGoods.FirstOrDefault()) + 1;
            ViewBag.To = goods.IndexOf(newGoods.LastOrDefault()) + 1;

            ViewBag.PageCount = pageCount;

            return PartialView(newGoods);
        }
        
        [HttpGet]
        [Authorize]
        [Authorize(Roles="Admin")]
        public ActionResult Remove(int id = 1)
        {
            //Удаление 
            GoodDTO goodDTO = goodsService.GetById(id);
            GoodViewModel good = mapper.Map<GoodViewModel>(goodDTO);
            return PartialView(good);
        }

        [HttpPost, ActionName("Remove")]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveConfirmed(int id = 1)
        {
            //Удаление 
            goodsService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int id = 0)
        {
            //Создание модели
            GoodCreateEditViewModel model = new GoodCreateEditViewModel();

            //Получение списков
            SelectList manufacturers = new SelectList(manufacturersService.GetAll(), "ManufacturerId", "ManufacturerName");
            model.Manufacturers = manufacturers;
            SelectList categories = new SelectList(categoriesService.GetAll(), "CategoryId", "CategoryName");
            model.Categories = categories;

            //Получение id
            ViewBag.Id = id;

            if (id == 0)
            {
                return PartialView(model);
            }
            else
            {
                //Поиск товара
                GoodDTO goodDTO = goodsService.GetById(id);

                //Перенос на модель представления
                model.GoodId = goodDTO.GoodId;
                model.GoodName = goodDTO.GoodName;
                model.Price = Convert.ToDecimal(Convert.ToDouble(goodDTO.Price));
                model.GoodCount = Convert.ToDecimal(Convert.ToDouble(goodDTO.GoodCount));
                model.ManufacturerId = goodDTO.ManufacturerId;
                model.CategoryId = goodDTO.CategoryId;

                return PartialView(model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(GoodCreateEditViewModel model, IEnumerable<HttpPostedFileBase> photoUpload)
        {
            GoodDTO newGood = new GoodDTO()
            {
                GoodId = (model.GoodId.HasValue ? model.GoodId.Value : 0),
                GoodName = model.GoodName,
                ManufacturerId = model.ManufacturerId,
                CategoryId = model.CategoryId,
                Price = model.Price,
                GoodCount = model.GoodCount
            };

            //Добавление
            goodsService.CreateOrUpdate(newGood);

            //Получение последнего добавленого товара
            GoodDTO targetGood = newGood.GoodId == 0 ? goodsService.GetAll().Last() : newGood;

            //Путь для сохранения
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Files\UploadFiles\GoodPhotos\");
            int i = 1;
            foreach (var photo in photoUpload)
            {
                if (photo != null)
                {
                    string filename = $"Good-{targetGood.GoodId}_Photo-{i++}{Path.GetExtension(photo.FileName)}";
                    string photoPath = Path.Combine(path, filename);
                    photo.SaveAs(photoPath);

                    //Добавление фото
                    photosService.CreateOrUpdate(new PhotoDTO() { GoodId = targetGood.GoodId, PhotoPath = Path.Combine(@"\Files\UploadFiles\GoodPhotos\", filename) });
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Filter()
        {
            //Получение списков
            SelectList manufacturers = new SelectList(manufacturersService.GetAll(), "ManufacturerId", "ManufacturerName");
            goodsModel.Manufacturers = manufacturers;
            SelectList categories = new SelectList(categoriesService.GetAll(), "CategoryId", "CategoryName");
            goodsModel.Categories = categories;

            //Список для сортировки
            SelectList sortItems = new SelectList(new List<string>()
            {
                "Newest",
                "Cheap",
                "Expensive"
            });
            goodsModel.SortItems = sortItems;

            //Получение минимальной и максимальной цен с учетом фильтрации по цене
            goodsModel.PriceFilter.From = Convert.ToDecimal(Convert.ToDouble(goodsModel.Goods.OrderBy(g => g.Price).FirstOrDefault().Price));
            goodsModel.PriceFilter.To = Convert.ToDecimal(Convert.ToDouble(goodsModel.Goods.OrderBy(g => g.Price).LastOrDefault().Price));

            return PartialView(goodsModel);
        }

        [HttpGet]
        public ActionResult Search()
        {
            return PartialView(goodsModel);
        }

        [HttpGet]
        public FileResult GetCsv()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"Files\CsvFiles\Goods.csv");
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default))
                {
                    List<GoodViewModel> goods = goodsModel.Goods.ToList();

                    sw.WriteLine("GoodId, Image, GoodName, Manufacturer, Category, Price, GoodCount");
                    foreach (var good in goods)
                    {
                        sw.WriteLine($"{good.GoodId}, " +
                                     $"\"{good?.Photo ?? "No photo"}\", " +
                                     $"\"{good.GoodName}\", " +
                                     $"\"{good?.Manufacturer?.ManufacturerName ?? "No data"}\", " +
                                     $"\"{good?.Category?.CategoryName ?? "No data"}\", " +
                                     $"\"{Convert.ToDouble(good.Price)}\", " +
                                     $"\"{Convert.ToDouble(good.GoodCount)}\"");
                    }
                }
            }

            return File(path, "application/csv", "Goods.csv");
        }
    }
}