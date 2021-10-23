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
    public class TipePembayaranController : BaseController
    {
        private WebContext db = new WebContext();

        //
        // GET: /TipePembayaran/

        public ViewResult Index()
        {
            return View(db.TipePembayarans.ToList());
        }

        //
        // GET: /TipePembayaran/Details/5

        public ViewResult Details(int id)
        {
            TipePembayaran tipepembayaran = db.TipePembayarans.Find(id);
            return View(tipepembayaran);
        }

        //
        // GET: /TipePembayaran/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /TipePembayaran/Create

        [HttpPost]
        public ActionResult Create(TipePembayaran tipepembayaran)
        {
            if (ModelState.IsValid)
            {
                db.TipePembayarans.Add(tipepembayaran);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(tipepembayaran);
        }
        
        //
        // GET: /TipePembayaran/Edit/5
 
        public ActionResult Edit(int id)
        {
            TipePembayaran tipepembayaran = db.TipePembayarans.Find(id);
            return View(tipepembayaran);
        }

        //
        // POST: /TipePembayaran/Edit/5

        [HttpPost]
        public ActionResult Edit(TipePembayaran tipepembayaran)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipepembayaran).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipepembayaran);
        }

        //
        // GET: /TipePembayaran/Delete/5
 
        public ActionResult Delete(int id)
        {
            TipePembayaran tipepembayaran = db.TipePembayarans.Find(id);
            return View(tipepembayaran);
        }

        //
        // POST: /TipePembayaran/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            TipePembayaran tipepembayaran = db.TipePembayarans.Find(id);
            db.TipePembayarans.Remove(tipepembayaran);
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