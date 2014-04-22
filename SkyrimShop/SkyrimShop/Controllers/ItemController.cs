using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkyrimShop.Models;
using SkyrimShop.DAL;
using PagedList;

namespace SkyrimShop.Controllers
{
    public class ItemController : Controller
    {
        private ItemContext storeDB = new ItemContext();

        public ActionResult Index(string sortOrder, string currentFilter, string SearchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.ClassSortParm = sortOrder == "class" ? "class_desc" : "class";

            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            ViewBag.CurrentFilter = SearchString;

            var items = from i in storeDB.Items
                        select i;
            if (!String.IsNullOrEmpty(SearchString))
            {
                items = items.Where(i => i.ItemName.ToUpper().Contains(SearchString.ToUpper())
                                       || i.ItemType.ToUpper().Contains(SearchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(i => i.ItemName);
                    break;
                case "price":
                    items = items.OrderBy(i => i.ItemPrice);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(i => i.ItemPrice);
                    break;
                case "type":
                    items = items.OrderBy(i => i.ItemType);
                    break;
                case "type_desc":
                    items = items.OrderByDescending(i => i.ItemType);
                    break;
                case "class":
                    items = items.OrderBy(i => i.ItemClass);
                    break;
                case "class_desc":
                    items = items.OrderByDescending(i => i.ItemClass);
                    break;
                default:
                    items = items.OrderBy(i => i.ItemName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }



        // GET: /Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = storeDB.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                storeDB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
