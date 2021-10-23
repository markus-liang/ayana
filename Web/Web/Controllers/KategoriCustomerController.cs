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
    public class KategoriCustomerController : BaseController
    {
        private WebContext db = new WebContext();

        //
        // GET: /KategoriCustomer/

        public ViewResult Index()
        {
            return View(db.KategoriCustomers.ToList());
        }

        //
        // GET: /KategoriCustomer/Details/5

        public ViewResult Details(int id)
        {
            KategoriCustomer kategoricustomer = db.KategoriCustomers.Find(id);
            return View(kategoricustomer);
        }

        //
        // GET: /KategoriCustomer/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /KategoriCustomer/Create

        [HttpPost]
        public ActionResult Create(KategoriCustomer kategoricustomer)
        {
            if (ModelState.IsValid)
            {
                db.KategoriCustomers.Add(kategoricustomer);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(kategoricustomer);
        }
        
        //
        // GET: /KategoriCustomer/Edit/5
 
        public ActionResult Edit(int id)
        {
            KategoriCustomer kategoricustomer = db.KategoriCustomers.Find(id);
            return View(kategoricustomer);
        }

        //
        // POST: /KategoriCustomer/Edit/5

        [HttpPost]
        public ActionResult Edit(KategoriCustomer kategoricustomer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategoricustomer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategoricustomer);
        }

        //
        // GET: /KategoriCustomer/Delete/5
 
        public ActionResult Delete(int id)
        {
            KategoriCustomer kategoricustomer = db.KategoriCustomers.Find(id);
            return View(kategoricustomer);
        }

        //
        // POST: /KategoriCustomer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            KategoriCustomer kategoricustomer = db.KategoriCustomers.Find(id);
            db.KategoriCustomers.Remove(kategoricustomer);
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