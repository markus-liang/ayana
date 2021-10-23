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
    public class CustomerController : BaseController
    {
        private WebContext db = new WebContext();

        //
        // GET: /Customer/

        public ViewResult Index(int page)
        {
            int start = (page - 1) * PAGE_SIZE;
            var customers = db.Customers.Include(b => b.KategoriCustomer).OrderBy(p => p.Nama);
            int jml_item = customers.Count();

            ViewBag.NUM_OF_DATA = jml_item;
            ViewBag.NUM_OF_PAGE = jml_item / PAGE_SIZE + (jml_item % PAGE_SIZE > 0 ? 1 : 0);
            ViewBag.CUR_PAGE = page;
            ViewBag.START = start + 1;
            ViewBag.END = (start + PAGE_SIZE) < jml_item ? (start + PAGE_SIZE) : jml_item;

            return View(customers.Skip(start).Take(PAGE_SIZE).ToList());
        }

        //
        // GET: /Customer/Details/5

        public ViewResult Details(int id)
        {
            Customer customer = db.Customers.Find(id);
            return View(customer);
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            ViewBag.KategoriCustomerID = new SelectList(db.KategoriCustomers, "KategoriCustomerID", "Nama");
            return View();
        } 

        //
        // POST: /Customer/Create

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", new { @page = 1 });  
            }

            ViewBag.KategoriCustomerID = new SelectList(db.KategoriCustomers, "KategoriCustomerID", "Nama", customer.KategoriCustomerID);
            return View(customer);
        }
        
        //
        // GET: /Customer/Edit/5

        public ActionResult Edit(int id, int return_page)
        {
            Customer customer = db.Customers.Find(id);
            ViewBag.KategoriCustomerID = new SelectList(db.KategoriCustomers, "KategoriCustomerID", "Nama", customer.KategoriCustomerID);
            ViewBag.ReturnPage = return_page;
            return View(customer);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { @page = Request["return_page"] });
            }
            ViewBag.KategoriCustomerID = new SelectList(db.KategoriCustomers, "KategoriCustomerID", "Nama", customer.KategoriCustomerID);
            return View(customer);
        }

        //
        // GET: /Customer/Delete/5
 
        public ActionResult Delete(int id)
        {
            Customer customer = db.Customers.Find(id);
            return View(customer);
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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