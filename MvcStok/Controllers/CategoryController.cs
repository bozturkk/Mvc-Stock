using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        MvcDbStockEntities db = new MvcDbStockEntities();
        public ActionResult Index(int Sayfa = 1)
        {
            //var categories = db.TBLKATEGORILER.ToList(); 
            var categories = db.TBLKATEGORILER.ToList().ToPagedList(Sayfa,4);
            return View(categories);
        }
        [HttpGet] //herhangibir işlem yapmazsam sadece view i döndür.
        public ActionResult NewCategory()
        {
            return View();
        }
        [HttpPost] //sayfaya herhangibir post işlemi yapıldığı zaman çalış.
        public ActionResult NewCategory(TBLKATEGORILER pCategory)
        {
            if(!ModelState.IsValid)
            {
                return View("NewCategory");
            }
            db.TBLKATEGORILER.Add(pCategory);
            db.SaveChanges();
            return RedirectToAction("Index"); ;
        }

        public ActionResult DeleteCategory(int id)
        {
            var category = db.TBLKATEGORILER.Find(id);
            db.TBLKATEGORILER.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BringCategory(int id)
        {
            var category = db.TBLKATEGORILER.Find(id);
            
            return View("BringCategory", category);
        }
        public ActionResult UpdateCategory(TBLKATEGORILER pCategory)
        {
            var category = db.TBLKATEGORILER.Find(pCategory.KATEGORIID);
            category.KATEGORIAD = pCategory.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}