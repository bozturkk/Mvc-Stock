using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        MvcDbStockEntities db = new MvcDbStockEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewSale()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewSale(TBLSATISLAR pSale)
        {
            db.TBLSATISLAR.Add(pSale);
            db.SaveChanges();
            return View("Index");
        }
    }
}