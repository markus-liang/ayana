using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class StokAdjustController : BaseController
    {
        private WebContext db = new WebContext();

        //
        // GET: /StokAdjust/

        public ViewResult Index(int page)
        {
            int start = (page - 1) * PAGE_SIZE;
            var stokadjusts = db.StokAdjusts.OrderByDescending(s => s.Tanggal);
            int jml_item = stokadjusts.Count();

            ViewBag.NUM_OF_DATA = jml_item;
            ViewBag.NUM_OF_PAGE = jml_item / PAGE_SIZE + (jml_item % PAGE_SIZE > 0 ? 1 : 0);
            ViewBag.CUR_PAGE = page;
            ViewBag.START = start + 1;
            ViewBag.END = (start + PAGE_SIZE) < jml_item ? (start + PAGE_SIZE) : jml_item;

            return View(stokadjusts.Skip(start).Take(PAGE_SIZE).ToList());
        }

        //
        // GET: /StokAdjust/Details/5

        public ViewResult Details(int id)
        {
            StokAdjust stokadjust = db.StokAdjusts.Find(id);
            stokadjust.StokAdjustDetails = stokadjust.StokAdjustDetails.OrderBy(p => p.Barang.Nama).ToList();
            return View(stokadjust);
        }

        //
        // GET: /StokAdjust/Create

        public ActionResult Create()
        {
            ViewBag.Barangs = db.Barangs.Where(b => b.IsNonAktif == false).OrderBy(p => p.Nama).ToArray();
            return View();
        } 

        //
        // POST: /StokAdjust/Create

        [HttpPost]
        public ActionResult Create(StokAdjust stokadjust)
        {
            Barang[] Barangs = db.Barangs.Where(b => b.IsNonAktif == false).OrderBy(p => p.Nama).ToArray();

            stokadjust.Username = User.Identity.Name;
            stokadjust.Tanggal = DateTime.Now;
            stokadjust.StokAdjustDetails = new List<StokAdjustDetail>();

            int cnt_barang, iIDBarang, iQty, iReal;
            Int32.TryParse(Request["hid_cnt_barang"], out cnt_barang);

            for (int i = 0; i < cnt_barang; i++)
            {
                if (Int32.TryParse(Request["stokkode_" + i], out iIDBarang) && Int32.TryParse(Request["real_" + i], out iReal))
                {
                    Int32.TryParse(Request["qty_" + i], out iQty);

                    stokadjust.StokAdjustDetails.Add(
                        new StokAdjustDetail()
                        {
                            BarangID = iIDBarang,
                            JumlahDalamData = iQty,
                            JumlahReal = iReal
                        });

                    Barang barang = db.Barangs.Find(iIDBarang);
                    barang.Qty = iReal;
                }
            }

            if (ModelState.IsValid)
            {
                db.StokAdjusts.Add(stokadjust);
                db.SaveChanges();
                return RedirectToAction("Index", new { page = 1 });
            }

            return View(stokadjust);
        }

        /*
        [HttpPost]
        public ActionResult Create(StokAdjust stokadjust)
        {
            Barang[] Barangs = db.Barangs.ToArray();

            stokadjust.Username = User.Identity.Name;
            stokadjust.Tanggal = DateTime.Now;
            stokadjust.StokAdjustDetails = new List<StokAdjustDetail>();
            
            int real;
            foreach (Barang barang in Barangs)
            {
                Int32.TryParse(Request["real_" + barang.BarangID], out real);
                stokadjust.StokAdjustDetails.Add(
                    new StokAdjustDetail()
                    {
                        BarangID = barang.BarangID,
                        JumlahDalamData = barang.Qty,
                        JumlahReal = real
                    });
                barang.Qty = real;
            }

            if (ModelState.IsValid)
            {
                db.StokAdjusts.Add(stokadjust);
                db.SaveChanges();
                return RedirectToAction("Index", new { page = 1 });  
            }

            return View(stokadjust);
        }
*/
        
        //
        // GET: /StokAdjust/Edit/5
 
        public ActionResult Edit(int id)
        {
            StokAdjust stokadjust = db.StokAdjusts.Find(id);
            return View(stokadjust);
        }

        //
        // POST: /StokAdjust/Edit/5

        [HttpPost]
        public ActionResult Edit(StokAdjust stokadjust)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stokadjust).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { page = 1 });
            }
            return View(stokadjust);
        }

        //
        // GET: /StokAdjust/Delete/5
 
        public ActionResult Delete(int id)
        {
            StokAdjust stokadjust = db.StokAdjusts.Find(id);
            return View(stokadjust);
        }

        //
        // POST: /StokAdjust/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            StokAdjust stokadjust = db.StokAdjusts.Find(id);
            foreach (StokAdjustDetail detail in stokadjust.StokAdjustDetails)
            {
                detail.Barang.Qty += (detail.JumlahDalamData - detail.JumlahReal);
            }
            db.StokAdjusts.Remove(stokadjust);
            db.SaveChanges();
            return RedirectToAction("Index", new { page = 1 });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}