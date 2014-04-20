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

namespace SkyrimShop.Controllers
{
    public class ItemController : Controller
    {
        private ItemContext storeDB = new ItemContext();

        public ActionResult Index(string sortOrder, string SearchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type" : "type_desc";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";

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
                default:
                    items = items.OrderBy(i => i.ItemName);
                    break;
            }

            return View(items);
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

        // GET: /Item/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ItemID,ItemName,ItemType,ItemPrice,ItemDescription,QuantityItem")] Item item)
        {
            if (ModelState.IsValid)
            {
                storeDB.Items.Add(item);
                storeDB.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: /Item/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: /Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ItemID,ItemName,ItemType,ItemPrice,ItemDescription,QuantityItem")] Item item)
        {
            if (ModelState.IsValid)
            {
                storeDB.Entry(item).State = EntityState.Modified;
                storeDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: /Item/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: /Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = storeDB.Items.Find(id);
            storeDB.Items.Remove(item);
            storeDB.SaveChanges();
            return RedirectToAction("Index");
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
