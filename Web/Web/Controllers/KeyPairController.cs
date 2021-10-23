using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{ 
    [Authorize]
    public class KeyPairController : BaseController
    {
        private WebContext db = new WebContext();

        //
        // GET: /KeyPair/

        public ViewResult Index()
        {
            var pairs = db.KeyPairs.ToList();
            foreach (KeyPair pair in pairs)
            {
                pair.Value = fromBase64(pair.Value);
            }
            return View(pairs);
        }

        //
        // GET: /KeyPair/Details/5

        public ViewResult Details(string id)
        {
            KeyPair keypair = db.KeyPairs.Find(id);
            keypair.Value = fromBase64(keypair.Value);
            return View(keypair);
        }

        //
        // GET: /KeyPair/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /KeyPair/Create

        [HttpPost]
        public ActionResult Create(KeyPair keypair)
        {
            if (ModelState.IsValid)
            {
                keypair.Value = toBase64(keypair.Value);
                db.KeyPairs.Add(keypair);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(keypair);
        }
        
        //
        // GET: /KeyPair/Edit/5
 
        public ActionResult Edit(string id)
        {
            KeyPair keypair = db.KeyPairs.Find(id);
            keypair.Value = fromBase64(keypair.Value);
            return View(keypair);
        }

        //
        // POST: /KeyPair/Edit/5

        [HttpPost]
        public ActionResult Edit(KeyPair keypair)
        {
            if (ModelState.IsValid)
            {
                keypair.Value = toBase64(keypair.Value);
                db.Entry(keypair).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(keypair);
        }

        //
        // GET: /KeyPair/Delete/5
 
        public ActionResult Delete(string id)
        {
            KeyPair keypair = db.KeyPairs.Find(id);
            keypair.Value = fromBase64(keypair.Value);
            return View(keypair);
        }

        //
        // POST: /KeyPair/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {            
            KeyPair keypair = db.KeyPairs.Find(id);
            db.KeyPairs.Remove(keypair);
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