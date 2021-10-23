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
    public class JenisKasKecilController : Controller
    {
        private WebContext db = new WebContext();

        //
        // GET: /JenisKasKecil/

        public ViewResult Index()
        {
            var kaskecils = db.KasKecils.Include(k => k.JenisKasKecil);
            return View(kaskecils.ToList());
        }

        //
        // GET: /JenisKasKecil/Details/5

        public ViewResult Details(int id)
        {
            KasKecil kaskecil = db.KasKecils.Find(id);
            return View(kaskecil);
        }

        //
        // GET: /JenisKasKecil/Create

        public ActionResult Create()
        {
            ViewBag.JenisKasKecilID = new SelectList(db.JenisKasKecils, "JenisKasKecilID", "Nama");
            return View();
        } 

        //
        // POST: /JenisKasKecil/Create

        [HttpPost]
        public ActionResult Create(KasKecil kaskecil)
        {
            if (ModelState.IsValid)
            {
                db.KasKecils.Add(kaskecil);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.JenisKasKecilID = new SelectList(db.JenisKasKecils, "JenisKasKecilID", "Nama", kaskecil.JenisKasKecilID);
            return View(kaskecil);
        }
        
        //
        // GET: /JenisKasKecil/Edit/5
 
        public ActionResult Edit(int id)
        {
            KasKecil kaskecil = db.KasKecils.Find(id);
            ViewBag.JenisKasKecilID = new SelectList(db.JenisKasKecils, "JenisKasKecilID", "Nama", kaskecil.JenisKasKecilID);
            return View(kaskecil);
        }

        //
        // POST: /JenisKasKecil/Edit/5

        [HttpPost]
        public ActionResult Edit(KasKecil kaskecil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kaskecil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JenisKasKecilID = new SelectList(db.JenisKasKecils, "JenisKasKecilID", "Nama", kaskecil.JenisKasKecilID);
            return View(kaskecil);
        }

        //
        // GET: /JenisKasKecil/Delete/5
 
        public ActionResult Delete(int id)
        {
            KasKecil kaskecil = db.KasKecils.Find(id);
            return View(kaskecil);
        }

        //
        // POST: /JenisKasKecil/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            KasKecil kaskecil = db.KasKecils.Find(id);
            db.KasKecils.Remove(kaskecil);
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