using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class KasKecilController : BaseController
    {
        private WebContext db = new WebContext();

        //
        // GET: /KasKecil/

        public ViewResult Index(int page)
        {
            int start = (page - 1) * PAGE_SIZE;
            var kaskecils = GetKasKecilInActivePeriode();
            int jml_item = kaskecils.Count();

            ViewBag.NUM_OF_DATA = jml_item;
            ViewBag.NUM_OF_PAGE = jml_item / PAGE_SIZE + (jml_item % PAGE_SIZE > 0 ? 1 : 0);
            ViewBag.CUR_PAGE = page;
            ViewBag.START = start + 1;
            ViewBag.END = (start + PAGE_SIZE) < jml_item ? (start + PAGE_SIZE) : jml_item;

            return View(kaskecils.Skip(start).Take(PAGE_SIZE).ToList().OrderBy(p => p.Tanggal));
        }

        public ViewResult IndexPerPeriode(int awal, int akhir)
        {
            List<KasKecil> KasKecils = db.KasKecils.Include(p => p.JenisKasKecil).Where(p => p.KasKecilID >= awal && p.KasKecilID <= akhir).ToList();
            if (KasKecils[0].Keterangan != day_opening_string || KasKecils[KasKecils.Count() - 1].Keterangan != day_closing_string)
            {
                KasKecils = new List<KasKecil>();
            }
            return View(KasKecils);
        }

        //
        // GET: /KasKecil/Details/5

        public ViewResult Details(int id)
        {
            KasKecil kaskecil = db.KasKecils.Find(id);
            return View(kaskecil);
        }

        //
        // GET: /KasKecil/OpenDay
        public ActionResult DayOpening()
        {
            if (ViewBag.ISDAYOPEN == false)
            {
                return View();
            }
            else
            {
                return Redirect("/KasKecil/Index?page=1");
            }
        }

        //
        // POST: /KasKecil/Create
        [HttpPost]
        public ActionResult DayOpening(KasKecil kaskecil)
        {
            JenisKasKecil jenis_kas = db.JenisKasKecils.Where(p => p.Nama == "DEBET").First();

            kaskecil.Tanggal = DateTime.Now;
            kaskecil.Keterangan = day_opening_string;
            kaskecil.JenisKasKecil = jenis_kas;

            if (ModelState.IsValid)
            {
                db.KasKecils.Add(kaskecil);
                db.SaveChanges();
                return RedirectToAction("Index", new { @page = 1 });
            }

            return View(kaskecil);
        }

        public ViewResult DayClosing()
        {
            var today = DateTime.Now.Date;
            ViewBag.Kas = GetKasKecilInActivePeriode();
            return View();
        }

        [HttpPost]
        public ActionResult DayClosingConfirmed()
        {
            var today = DateTime.Now;
            int awal, akhir, pengeluaran, pemasukan;
            awal = akhir = pengeluaran = pemasukan = 0;

            var KasKecils = GetKasKecilInActivePeriode();

            foreach (Web.Models.KasKecil kas in KasKecils)
            {
                if (kas.Keterangan == Web.Controllers.BaseController.day_opening_string)
                {
                    awal = kas.Total;
                }
                else if (kas.JenisKasKecil.Nama == "DEBET")
                {
                    pemasukan += kas.Total;
                }
                else if (kas.JenisKasKecil.Nama == "KREDIT")
                {
                    pengeluaran += kas.Total;
                }
            }
            akhir = awal + pemasukan - pengeluaran;

            JenisKasKecil jenis_kas = db.JenisKasKecils.Where(p => p.Nama == "KREDIT").First();
            KasKecil temp = new KasKecil();
            temp.Tanggal = today;
            temp.Keterangan = day_closing_string;
            temp.Total = akhir;
            temp.JenisKasKecil = jenis_kas;
            db.KasKecils.Add(temp);
            db.SaveChanges();

            return RedirectToAction("Index", new { @page = 1 });
        }

        //
        // GET: /KasKecil/OpenDay
        public ActionResult DayReopen()
        {
            var today = DateTime.Now.Date;
            if (db.KasKecils.Count() == 0)
            {
                return Redirect("DayOpening");
            }
            else
            {
                KasKecil kas_kecil = db.KasKecils.Where(p => p.Tanggal < DateTime.Now && p.Keterangan == day_closing_string).OrderByDescending(p => p.Tanggal).First();

                //kas_kecil.Status = false;
                db.KasKecils.Remove(kas_kecil);

                db.SaveChanges();

                return RedirectToAction("Index", new { @page = 1 });
            }
        }

        //
        // GET: /KasKecil/Create
        public ActionResult Create()
        {
            ViewBag.JenisKasKecilID = new SelectList(db.JenisKasKecils, "JenisKasKecilID", "Nama");
            return View();
        }

        //
        // POST: /KasKecil/Create
        [HttpPost]
        public ActionResult Create(KasKecil kaskecil)
        {
            kaskecil.Tanggal = DateTime.Now;
            if (kaskecil.Keterangan == day_opening_string ||
                kaskecil.Keterangan == day_closing_string ||
                kaskecil.Keterangan == pembelian_prefix_string ||
                kaskecil.Keterangan == pembelian_batal_prefix_string ||
                kaskecil.Keterangan == pembelian_retur_prefix_string ||
                kaskecil.Keterangan == penjualan_prefix_string ||
                kaskecil.Keterangan == penjualan_batal_prefix_string ||
                kaskecil.Keterangan == penjualan_retur_prefix_string
            )
            {
                kaskecil.Keterangan = "_" + kaskecil.Keterangan;
            }
            if (ModelState.IsValid)
            {
                kaskecil.Keterangan = kaskecil.Keterangan == null ? "" : kaskecil.Keterangan;
                db.KasKecils.Add(kaskecil);
                db.SaveChanges();
                return RedirectToAction("Index", new { @page = 1 });
            }

            ViewBag.JenisKasKecilID = new SelectList(db.JenisKasKecils, "JenisKasKecilID", "Nama");

            return View(kaskecil);
        }
        
        //
        // GET: /KasKecil/Edit/5
 
        public ActionResult Edit(int id)
        {
            KasKecil kaskecil = db.KasKecils.Find(id);
            return View(kaskecil);
        }

        //
        // POST: /KasKecil/Edit/5

        [HttpPost]
        public ActionResult Edit(KasKecil kaskecil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kaskecil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { @page = 1 });
            }
            return View(kaskecil);
        }

        //
        // GET: /KasKecil/Delete/5
 
        public ActionResult Delete(int id)
        {
            KasKecil kaskecil = db.KasKecils.Find(id);
            return View(kaskecil);
        }

        //
        // POST: /KasKecil/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            KasKecil kaskecil_default = db.KasKecils.Find(id);
            kaskecil_default.Status = false;
            KasKecil kaskecil_new = new KasKecil()
            {
                JenisKasKecilID = kaskecil_default.JenisKasKecilID == 1 ? 2 : 1,
                Keterangan = "Pembatalan " + kaskecil_default.Keterangan,
                Tanggal = DateTime.Now,
                Total = kaskecil_default.Total,
                Status = false
            };
            db.KasKecils.Add(kaskecil_new);
            db.Entry(kaskecil_default).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { @page = 1 });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public IEnumerable<KasKecil> GetKasKecilInActivePeriode()
        {
            var KasKecils = db.KasKecils.Include(p => p.JenisKasKecil).Where(p => p.Tanggal >= PeriodeStart);
            return KasKecils;
        }
    }
}