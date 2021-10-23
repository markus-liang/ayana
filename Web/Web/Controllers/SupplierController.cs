using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class SupplierController : BaseController
    {
        private WebContext db = new WebContext();

        //
        // GET: /Supplier/

        public ViewResult Index(int page)
        {
            int start = (page - 1) * PAGE_SIZE;
            var suppliers = db.Suppliers.OrderBy(p => p.Nama);
            int jml_item = suppliers.Count();

            ViewBag.NUM_OF_DATA = jml_item;
            ViewBag.NUM_OF_PAGE = jml_item / PAGE_SIZE + (jml_item % PAGE_SIZE > 0 ? 1 : 0);
            ViewBag.CUR_PAGE = page;
            ViewBag.START = start + 1;
            ViewBag.END = (start + PAGE_SIZE) < jml_item ? (start + PAGE_SIZE) : jml_item;

            return View(suppliers.Skip(start).Take(PAGE_SIZE).ToList());
        }

        //
        // GET: /Supplier/Details/5

        public ViewResult Details(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            return View(supplier);
        }

        //
        // GET: /Supplier/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Supplier/Create

        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index", new { @page = 1 });  
            }

            return View(supplier);
        }
        
        //
        // GET: /Supplier/Edit/5

        public ActionResult Edit(int id, int return_page)
        {
            Supplier supplier = db.Suppliers.Find(id);
            ViewBag.ReturnPage = return_page;
            return View(supplier);
        }

        //
        // POST: /Supplier/Edit/5

        [HttpPost]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { @page = Request["return_page"] });
            }
            return View(supplier);
        }

        //
        // GET: /Supplier/Delete/5
 
        public ActionResult Delete(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            return View(supplier);
        }

        //
        // POST: /Supplier/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
            db.SaveChanges();
            return RedirectToAction("Index", new { @page = 1 });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}