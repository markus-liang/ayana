using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class PembelianController : BaseController
    {
        private WebContext db = new WebContext();

        public ViewResult Index(int page)
        {
            int start = (page - 1) * PAGE_SIZE;
            var pembelians = db.Pembelians
                .Where(p => p.Tanggal >= PeriodeStart || p.StatusTransaksi == (int)StatusTransaksi.MenungguPembayaran)
                .Include(p => p.Supplier).Include(p => p.TipePembayaran).OrderByDescending(p => p.Tanggal);
            int jml_item = pembelians.Count();

            ViewBag.NUM_OF_DATA = jml_item;
            ViewBag.NUM_OF_PAGE = jml_item / PAGE_SIZE + (jml_item % PAGE_SIZE > 0 ? 1 : 0);
            ViewBag.CUR_PAGE = page;
            ViewBag.START = start + 1;
            ViewBag.END = (start + PAGE_SIZE) < jml_item ? (start + PAGE_SIZE) : jml_item;

            List<Pembelian> Pembelians = pembelians.Skip(start).Take(PAGE_SIZE).ToList();
            List<ReturPembelianDetail> ReturPembelianDetails;
            Hashtable ht = new Hashtable();
            int total_retur;
            foreach (Pembelian p in Pembelians)
            {
                total_retur = 0;
                ReturPembelianDetails = db.ReturPembelianDetails.Where(q => q.PembelianID == p.PembelianID).ToList();
                foreach (ReturPembelianDetail ret in ReturPembelianDetails)
                {
                    // cari harga waktu pembelian
                    total_retur += ret.Jumlah * ret.Harga;
                }
                ht.Add(p.PembelianID, total_retur);
            }
            ViewBag.RETUR = ht;
            return View(Pembelians);
        }

        //
        // GET: /Pembelian/Details/5

        public ViewResult Details(int id)
        {
            Pembelian pembelian = db.Pembelians.Find(id);
            List<ReturPembelianDetail> ReturPembelianDetails = db.ReturPembelianDetails.Where(p => p.PembelianID == id).ToList();
            ViewBag.RETURPEMBELIANDETAILS = ReturPembelianDetails;
            return View(pembelian);
        }

        //
        // GET: /Pembelian/Create

        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Nama");
            ViewBag.TipePembayaranID = new SelectList(db.TipePembayarans, "TipePembayaranID", "Nama");
            ViewBag.Barangs = db.Barangs.Where(b => b.IsNonAktif == false).OrderBy(p => p.Nama).ToArray();
            ViewBag.Suppliers = db.Suppliers.ToArray();
            return View();
        } 

        //
        // POST: /Pembelian/Create

        [HttpPost]
        public ActionResult Create(Pembelian pembelian)
        {
            int cnt_belanjaan, iTop, iIDBarang, iJumlah, iHarga;
            Int32.TryParse(Request["iToP"], out iTop);
            Int32.TryParse(Request["hid_cnt_belanjaan"], out cnt_belanjaan);
            Barang[] Barangs = db.Barangs.Where(b => b.IsNonAktif == false).OrderBy(p => p.Nama).ToArray();
            Supplier[] Suppliers = db.Suppliers.ToArray();

            pembelian.UserName = User.Identity.Name;
            pembelian.Tanggal = DateTime.Now;
            pembelian.JatuhTempo = pembelian.Tanggal.AddDays(iTop);

            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Nama", pembelian.SupplierID);
            ViewBag.TipePembayaranID = new SelectList(db.TipePembayarans, "TipePembayaranID", "Nama", pembelian.TipePembayaranID);
            ViewBag.Barangs = Barangs;
            ViewBag.Suppliers = Suppliers;

            if (ModelState.IsValid)
            {
                pembelian.PembelianDetails = new List<PembelianDetail>();

                for (int i = 0; i < cnt_belanjaan; i++)
                {
                    if (Request["bljkode_" + i] != null)
                    {
                        Int32.TryParse(Request["bljkode_" + i], out iIDBarang);
                        Int32.TryParse(Request["bljjmlpesanan_" + i], out iJumlah);
                        Int32.TryParse(Request["bljharga_" + i], out iHarga);

                        Barang barang = db.Barangs.Find(iIDBarang);
                        barang.HargaBeli = iHarga;
                        barang.HargaRataan = ((barang.HargaRataan * barang.Qty) + (iJumlah * iHarga)) / (barang.Qty + iJumlah);
                        barang.Qty += iJumlah;

                        PembelianDetail detail = new PembelianDetail()
                        {
                            BarangID = iIDBarang,
                            Jumlah = iJumlah,
                            Harga = iHarga,
                            Barang = barang
                        };
                        pembelian.PembelianDetails.Add(detail);
                    }
                }
                pembelian.StatusTransaksi = (int)StatusTransaksi.MenungguPembayaran;
                db.Pembelians.Add(pembelian);
                db.SaveChanges();
                if (iTop == 0)
                {
                    return RedirectToAction("Edit", new { id = pembelian.PembelianID });
                }
                else
                {
                    return RedirectToAction("Index", new { page = 1 });
                }
            }

            return View(pembelian);
        }
        
        //
        // GET: /Pembelian/Edit/5
 
        public ActionResult Edit(int id)
        {
            Pembelian pembelian = db.Pembelians.Find(id);
            ViewBag.TipePembayaranID = new SelectList(db.TipePembayarans, "TipePembayaranID", "Nama", pembelian.TipePembayaranID);

            List<ReturPembelianDetail> ReturPembelianDetails = db.ReturPembelianDetails.Where(p => p.PembelianID == id).ToList();
            ViewBag.RETURPEMBELIANDETAILS = ReturPembelianDetails;
            
            return View(pembelian);
        }

        //
        // POST: /Pembelian/Edit/5

        [HttpPost]
        public ActionResult Edit(Pembelian pembelian)
        {
            if (ModelState.IsValid)
            {
                pembelian.StatusTransaksi = (int)StatusTransaksi.Lunas;
                pembelian.TanggalBayar = DateTime.Now;
                TipePembayaran pembayaran = db.TipePembayarans.Find(pembelian.TipePembayaranID);
                if (pembayaran.Nama.ToUpper() == "CASH")
                {
                    JenisKasKecil jenis_kas = db.JenisKasKecils.Where(p => p.Nama == "KREDIT").First();

                    KasKecil kas = new KasKecil();
                    kas.Tanggal = DateTime.Now;
                    kas.Total = pembelian.Total;
                    kas.JenisKasKecil = jenis_kas;
                    kas.Keterangan = pembelian_prefix_string + pembelian.PembelianID.ToString().PadLeft(6, '0');
                    db.KasKecils.Add(kas);
                }

                db.Entry(pembelian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { page = 1 });
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Nama", pembelian.SupplierID);
            ViewBag.TipePembayaranID = new SelectList(db.TipePembayarans, "TipePembayaranID", "Nama", pembelian.TipePembayaranID);

            List<ReturPembelianDetail> ReturPembelianDetails = db.ReturPembelianDetails.Where(p => p.PembelianID == pembelian.PembelianID).ToList();
            ViewBag.RETURPEMBELIANDETAILS = ReturPembelianDetails;

            return View(pembelian);
        }

        //
        // GET: /Pembelian/Delete/5
 
        public ActionResult Delete(int id)
        {
            Pembelian pembelian = db.Pembelians.Find(id);
            return View(pembelian);
        }

        //
        // POST: /Pembelian/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Pembelian pembelian = db.Pembelians.Find(id);
            foreach (PembelianDetail detail in pembelian.PembelianDetails)
            {
                int pembagi = (detail.Barang.Qty - detail.Jumlah) == 0 ? 1 : (detail.Barang.Qty - detail.Jumlah);
                detail.Barang.HargaRataan = ((detail.Barang.HargaRataan * detail.Barang.Qty) - (detail.Jumlah * detail.Harga)) / pembagi;
                detail.Barang.Qty -= detail.Jumlah;
            }
            if (pembelian.StatusTransaksi == (int)StatusTransaksi.Lunas)
            {
                JenisKasKecil jenis_kas = db.JenisKasKecils.Where(p => p.Nama == "DEBET").First();
                KasKecil kas = new KasKecil();
                kas.Tanggal = DateTime.Now;
                kas.JenisKasKecil = jenis_kas;
                kas.Total = pembelian.Total;
                kas.Keterangan = pembelian_batal_prefix_string + pembelian.PembelianID.ToString().PadLeft(6, '0');

                db.KasKecils.Add(kas);
            }
            pembelian.StatusTransaksi = (int)StatusTransaksi.Dibatalkan;
            db.SaveChanges();
            return RedirectToAction("Index", new { page = 1 });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult GetPembelianDetailByFaktur(string FakturID)
        {
            JsonResult result;
            List<ReturItem> temp = new List<ReturItem>();
            ReturItem ReturItem;

            try
            {
                int iFakturID;
                Int32.TryParse(FakturID, out iFakturID);
                var PembelianDetails =
                    db.PembelianDetails
                    .Where(p => p.PembelianID == iFakturID && (p.Pembelian.StatusTransaksi == (int)StatusTransaksi.Lunas || p.Pembelian.StatusTransaksi == (int)StatusTransaksi.MenungguPembayaran))
                    .Include(p => p.Barang)
                    .Include(p => p.Pembelian).ToList();
                
                foreach (PembelianDetail p in PembelianDetails)
                {
                    ReturItem = new ReturItem()
                    { 
                        FakturID = p.PembelianID,
                        BarangID = p.BarangID,
                        ClientID = p.Pembelian.SupplierID,
                        NamaKlien= p.Pembelian.Supplier.Nama,
                        Nama = p.Barang.Nama,
                        Jumlah = p.Jumlah,
                        Harga = p.Harga,
                        isLunas = (p.Pembelian.StatusTransaksi == (int)StatusTransaksi.Lunas)
                    };

                    var ReturPembelianDetails =
                        db.ReturPembelianDetails
                        .Where(q => q.PembelianID == p.PembelianID && q.BarangID == p.BarangID).ToList();
                    if (ReturPembelianDetails.Count() > 0)
                    {
                        int jml = ReturPembelianDetails.Sum(q => q.Jumlah);
                        ReturItem.Jumlah -= jml;
                    }
                    temp.Add(ReturItem);
                }
                result = Json(new { status = "OK", list = temp }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = "ERROR", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
    }
}