using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;

namespace MvcStok.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        MvcDbStockEntities db = new MvcDbStockEntities();
       
        public ActionResult Index(string searchList)
        {
            var customer = from c in db.TBLMUSTERILER select c;
            if (!string.IsNullOrEmpty(searchList))
            {
                customer = customer.Where(m => m.MUSTERIAD.Contains(searchList));
            }
            return View(customer.ToList());
        }
       
        [HttpGet]
        public ActionResult NewCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewCustomer(TBLMUSTERILER pCustomer)
        {
            if(!ModelState.IsValid)
            {
                return View("NewCustomer");
            }
            db.TBLMUSTERILER.Add(pCustomer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCustomer(int id)
        {
            var customer = db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BringCustomer(int id)
        {
            var customer = db.TBLMUSTERILER.Find(id);
            return View("BringCustomer", customer);
        }
        public ActionResult UpdateCustomer(TBLMUSTERILER pCustomer)
        {
            var customer = db.TBLMUSTERILER.Find(pCustomer.MUSTERIID);
            customer.MUSTERIAD = pCustomer.MUSTERIAD;
            customer.MUSTERISOYAD = pCustomer.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}