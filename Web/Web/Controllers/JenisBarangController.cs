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
    public class JenisBarangController : BaseController
    {
        private WebContext db = new WebContext();

        //
        // GET: /JenisBarang/

        public ViewResult Index(int page)
        {
            int start = (page - 1) * PAGE_SIZE;
            var jenisbarangs = db.JenisBarangs.OrderBy(p => p.Nama);
            int jml_item = jenisbarangs.Count();

            ViewBag.NUM_OF_DATA = jml_item;
            ViewBag.NUM_OF_PAGE = jml_item / PAGE_SIZE + (jml_item % PAGE_SIZE > 0 ? 1 : 0);
            ViewBag.CUR_PAGE = page;
            ViewBag.START = start + 1;
            ViewBag.END = (start + PAGE_SIZE) < jml_item ? (start + PAGE_SIZE) : jml_item;

            return View(jenisbarangs.Skip(start).Take(PAGE_SIZE).ToList());
        }

        //
        // GET: /JenisBarang/Details/5

        public ViewResult Details(int id)
        {
            JenisBarang jenisbarang = db.JenisBarangs.Find(id);
            return View(jenisbarang);
        }

        //
        // GET: /JenisBarang/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /JenisBarang/Create

        [HttpPost]
        public ActionResult Create(JenisBarang jenisbarang)
        {
            if (ModelState.IsValid)
            {
                db.JenisBarangs.Add(jenisbarang);
                db.SaveChanges();
                return RedirectToAction("Index", new { @page = 1 });
            }

            return View(jenisbarang);
        }
        
        //
        // GET: /JenisBarang/Edit/5

        public ActionResult Edit(int id, int return_page)
        {
            JenisBarang jenisbarang = db.JenisBarangs.Find(id);
            ViewBag.ReturnPage = return_page;
            return View(jenisbarang);
        }

        //
        // POST: /JenisBarang/Edit/5

        [HttpPost]
        public ActionResult Edit(JenisBarang jenisbarang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jenisbarang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { @page = Request["return_page"] });
            }
            return View(jenisbarang);
        }

        //
        // GET: /JenisBarang/Delete/5
 
        public ActionResult Delete(int id)
        {
            JenisBarang jenisbarang = db.JenisBarangs.Find(id);
            return View(jenisbarang);
        }

        //
        // POST: /JenisBarang/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            JenisBarang jenisbarang = db.JenisBarangs.Find(id);
            db.JenisBarangs.Remove(jenisbarang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}