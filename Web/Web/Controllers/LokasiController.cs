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
    public class LokasiController : BaseController
    {
        private WebContext db = new WebContext();

        //
        // GET: /Lokasi/

        public ViewResult Index(int page)
        {
            int start = (page - 1) * PAGE_SIZE;
            var lokasis = db.Lokasis.OrderBy(p => p.Nama);
            int jml_item = lokasis.Count();

            ViewBag.NUM_OF_DATA = jml_item;
            ViewBag.NUM_OF_PAGE = jml_item / PAGE_SIZE + (jml_item % PAGE_SIZE > 0 ? 1 : 0);
            ViewBag.CUR_PAGE = page;
            ViewBag.START = start + 1;
            ViewBag.END = (start + PAGE_SIZE) < jml_item ? (start + PAGE_SIZE) : jml_item;

            return View(lokasis.Skip(start).Take(PAGE_SIZE).ToList());
        }

        //
        // GET: /Lokasi/Details/5

        public ViewResult Details(int id)
        {
            Lokasi lokasi = db.Lokasis.Find(id);
            return View(lokasi);
        }

        //
        // GET: /Lokasi/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Lokasi/Create

        [HttpPost]
        public ActionResult Create(Lokasi lokasi)
        {
            if (ModelState.IsValid)
            {
                db.Lokasis.Add(lokasi);
                db.SaveChanges();
                return RedirectToAction("Index", new { @page = 1 });  
            }

            return View(lokasi);
        }
        
        //
        // GET: /Lokasi/Edit/5

        public ActionResult Edit(int id, int return_page)
        {
            Lokasi lokasi = db.Lokasis.Find(id);
            ViewBag.ReturnPage = return_page;
            return View(lokasi);
        }

        //
        // POST: /Lokasi/Edit/5

        [HttpPost]
        public ActionResult Edit(Lokasi lokasi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lokasi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { @page = Request["return_page"] });
            }
            return View(lokasi);
        }

        //
        // GET: /Lokasi/Delete/5
 
        public ActionResult Delete(int id)
        {
            Lokasi lokasi = db.Lokasis.Find(id);
            return View(lokasi);
        }

        //
        // POST: /Lokasi/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Lokasi lokasi = db.Lokasis.Find(id);
            db.Lokasis.Remove(lokasi);
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