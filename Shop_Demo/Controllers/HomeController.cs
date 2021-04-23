using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_Demo.DAL.Context;

namespace Shop_Demo.Controllers
{
    public class HomeController : Controller
    {
        ShopAdoEntities db = new ShopAdoEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Goods(int id = 1)
        {
            //Номер страницы
            ViewBag.Id = id;

            //Количество товаров на одной странице
            const int GOODS_PER_PAGE = 5;
            //Список всех товаров
            List<Good> goods = db.Good.ToList();
            //Количество страниц с товарами
            int pageCount = (int)Math.Ceiling(goods.Count() / (double)GOODS_PER_PAGE);
            //Список товаров с пагинацией
            List<Good> newGoods = goods.Skip((id - 1) * GOODS_PER_PAGE).Take(GOODS_PER_PAGE).ToList();
            //Общее количество товаров
            ViewBag.GoodsCount = goods.Count();

            //Номера первого и последнего товаров в пагинации
            ViewBag.From = goods.IndexOf(newGoods.First()) + 1;
            ViewBag.To = goods.IndexOf(newGoods.Last()) + 1;

            ViewBag.Goods = newGoods;
            ViewBag.PageCount = pageCount;
            return View();
        }

        public ActionResult Manufacturers(int id = 1)
        {
            ViewBag.Id = id;

            //Количество производителей на одной странице
            const int MANUFACTURERS_PER_PAGE = 5;
            List<Manufacturer> manufacturers = db.Manufacturer.ToList();
            //Количество страниц с производителями
            int pageCount = (int)Math.Ceiling(manufacturers.Count() / (double)MANUFACTURERS_PER_PAGE);

            ViewBag.Manufacturers = manufacturers.Skip((id - 1) * MANUFACTURERS_PER_PAGE).Take(MANUFACTURERS_PER_PAGE);
            ViewBag.PageCount = pageCount;
            return View();
        }

        public ActionResult Categories(int id = 1)
        {
            ViewBag.Id = id;

            //Количество категорий на одной странице
            const int CATEGORIES_PER_PAGE = 5;
            List<Category> categories = db.Category.ToList();
            //Количество страниц с категориями
            int pageCount = (int)Math.Ceiling(categories.Count() / (double)CATEGORIES_PER_PAGE);

            ViewBag.Categories = categories.Skip((id - 1) * CATEGORIES_PER_PAGE).Take(CATEGORIES_PER_PAGE);
            ViewBag.PageCount = pageCount;
            return View();
        }

        public ActionResult Sales(int id = 1)
        {
            ViewBag.Id = id;

            //Количество продаж на одной странице
            const int SALES_PER_PAGE = 5;
            List<Sale> sales = db.Sale.ToList();
            //Количество страниц с продажами
            int pageCount = (int)Math.Ceiling(sales.Count() / (double)SALES_PER_PAGE);

            ViewBag.Sales = sales.Skip((id - 1) * SALES_PER_PAGE).Take(SALES_PER_PAGE);
            ViewBag.PageCount = pageCount;
            return View();
        }

        public ActionResult Photos(int id = 1)
        {
            ViewBag.Id = id;

            //Количество фотографий на одной странице
            const int PHOTOS_PER_PAGE = 5;
            List<Photo> photos = db.Photo.ToList();
            //Количество страниц с фотографиями
            int pageCount = (int)Math.Ceiling(photos.Count() / (double)PHOTOS_PER_PAGE);

            ViewBag.Photos = photos.Skip((id - 1) * PHOTOS_PER_PAGE).Take(PHOTOS_PER_PAGE);
            ViewBag.PageCount = pageCount;
            return View();
        }

        public ActionResult SalePoses(int id = 1)
        {
            ViewBag.Id = id;

            //Количество проданных позиций на одной странице
            const int SALEPOSES_PER_PAGE = 5;
            List<SalePos> saleposes = db.SalePos.ToList();
            //Количество страниц с проданными позициями
            int pageCount = (int)Math.Ceiling(saleposes.Count() / (double)SALEPOSES_PER_PAGE);

            ViewBag.SalePoses = saleposes.Skip((id - 1) * SALEPOSES_PER_PAGE).Take(SALEPOSES_PER_PAGE);
            ViewBag.PageCount = pageCount;
            return View();
        }
    }
}