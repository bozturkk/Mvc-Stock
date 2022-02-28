using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;

namespace MvcStok.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        MvcDbStockEntities db = new MvcDbStockEntities();
        public ActionResult Index()
        {
            var products = db.TBLURUNLER.ToList();
            return View(products);
        }
        [HttpGet]
        public ActionResult NewProduct()
        {
            List<SelectListItem> categories = (from c in db.TBLKATEGORILER.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = c.KATEGORIAD, 
                                                   Value = c.KATEGORIID.ToString()
                                               }).ToList();
            ViewBag.dgr = categories;
            return View();
        }
        [HttpPost]
        public ActionResult NewProduct(TBLURUNLER pProduct)
        {
            var category = db.TBLKATEGORILER.Where(m => m.KATEGORIID == pProduct.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            pProduct.TBLKATEGORILER = category;

            db.TBLURUNLER.Add(pProduct);
            db.SaveChanges();
            return RedirectToAction("Index"); // kaydetme işlemi sonrası index sayfasına yönlendirir.
        }

        public ActionResult DeleteProduct(int id)
        {
            var product = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BringProduct(int id)
        {
            var product = db.TBLURUNLER.Find(id);
            List<SelectListItem> categories = (from c in db.TBLKATEGORILER.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = c.KATEGORIAD,
                                                   Value = c.KATEGORIID.ToString()
                                               }).ToList();
            ViewBag.dgr = categories;
            return View("BringProduct", product);
        }

        public ActionResult UpdateProduct(TBLURUNLER pProduct)
        {
            var product = db.TBLURUNLER.Find(pProduct.URUNID);
            product.URUNID = pProduct.URUNID;
            product.URUNAD = pProduct.URUNAD;
            //product.URUNKATEGORI = pProduct.URUNKATEGORI;
            var category = db.TBLKATEGORILER.Where(m => m.KATEGORIID == pProduct.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            product.URUNKATEGORI = category.KATEGORIID;
            product.FIYAT = pProduct.FIYAT;
            product.MARKA = pProduct.MARKA;
            product.STOK = pProduct.STOK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}