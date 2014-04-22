using SkyrimShop.DAL;
using SkyrimShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkyrimShop.Controllers
{
    public class CheckoutController : Controller
    {  
            ItemContext storeDB = new ItemContext();
            const string PromoCode = "FREE";
            
        public ActionResult AddressAndPayment()
            {
                return View();
            }
       
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    
                    order.OrderDate = DateTime.Now;

                    //Save Order
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();
                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }
        public ActionResult Complete(int id)
        {
            
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
        
	}
}