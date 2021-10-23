using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class PenjualanController : BaseController
    {
        private WebContext db = new WebContext();

        //
        // GET: /Penjualan/

        public ViewResult Index(int page)
        {
            int start = (page - 1) * PAGE_SIZE;
            var penjualans = db.Penjualans
                .Where(p => p.Tanggal >= PeriodeStart || p.StatusTransaksi == (int)StatusTransaksi.MenungguPembayaran)
                .Include(p => p.Customer).Include(p => p.TipePembayaran).OrderByDescending(p => p.Tanggal);
            int jml_item = penjualans.Count();

            ViewBag.NUM_OF_DATA = jml_item;
            ViewBag.NUM_OF_PAGE = jml_item / PAGE_SIZE + (jml_item % PAGE_SIZE > 0 ? 1 : 0);
            ViewBag.CUR_PAGE = page;
            ViewBag.START = start + 1;
            ViewBag.END = (start + PAGE_SIZE) < jml_item ? (start + PAGE_SIZE) : jml_item;

            List<Penjualan> Penjualans = penjualans.Skip(start).Take(PAGE_SIZE).ToList();
            List<ReturPenjualanDetail> ReturPenjualanDetails;
            Hashtable ht = new Hashtable();
            int total_retur;
            foreach (Penjualan p in Penjualans)
            {
                total_retur = 0;
                ReturPenjualanDetails = db.ReturPenjualanDetails.Where(q => q.PenjualanID == p.PenjualanID).ToList();
                foreach (ReturPenjualanDetail ret in ReturPenjualanDetails)
                {
                    // cari harga waktu pembelian
                    total_retur += ret.Jumlah * ret.Harga;
                }
                ht.Add(p.PenjualanID, total_retur);
            }
            ViewBag.RETUR = ht;
            return View(Penjualans);
        }

        //
        // GET: /Penjualan/Details/5

        public ViewResult Details(int id)
        {
            Penjualan penjualan = db.Penjualans.Find(id);
            List<ReturPenjualanDetail> ReturPenjualanDetails = db.ReturPenjualanDetails.Where(p => p.PenjualanID == id).ToList();
            ViewBag.RETURPENJUALANDETAILS = ReturPenjualanDetails;
            return View(penjualan);
        }

        //
        // GET: /Penjualan/Create

        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Nama");
            ViewBag.TipePembayaranID = new SelectList(db.TipePembayarans, "TipePembayaranID", "Nama");
            ViewBag.Barangs = db.Barangs.Where(b => b.IsNonAktif == false).OrderBy(p => p.Nama).ToArray();
            ViewBag.Customers = db.Customers.ToArray();
            return View();
        } 

        //
        // POST: /Penjualan/Create

        [HttpPost]
        public ActionResult Create(Penjualan penjualan)
        {
            int cnt_belanjaan, iTop, iIDBarang, iJumlah, iHarga;
            Int32.TryParse(Request["iToP"], out iTop);
            Int32.TryParse(Request["hid_cnt_belanjaan"], out cnt_belanjaan);
            Barang[] Barangs = db.Barangs.Where(b => b.IsNonAktif == false).OrderBy(p => p.Nama).ToArray();
            Customer[] Customers = db.Customers.ToArray();

            penjualan.UserName = User.Identity.Name;
            penjualan.Tanggal = DateTime.Now;
            penjualan.JatuhTempo = penjualan.Tanggal.AddDays(iTop);
 
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Nama", penjualan.CustomerID);
            ViewBag.TipePembayaranID = new SelectList(db.TipePembayarans, "TipePembayaranID", "Nama", penjualan.TipePembayaranID);
            ViewBag.Barangs = Barangs;
            ViewBag.Customers = Customers;
            List<string> list_item_qty_prc= new List<string>();

            if (ModelState.IsValid)
            {
                Customer Customer = db.Customers.Find(penjualan.CustomerID);
                penjualan.PenjualanDetails = new List<PenjualanDetail>();

                for (int i = 0; i < cnt_belanjaan; i++)
                {
                    if (Request["bljkode_" + i] != null)
                    {
                        Int32.TryParse(Request["bljkode_" + i], out iIDBarang);
                        Int32.TryParse(Request["bljjmlpesanan_" + i], out iJumlah);
                        Int32.TryParse(Request["bljharga_" + i], out iHarga);

                        Barang barang = db.Barangs.Find(iIDBarang);
                        barang.Qty -= iJumlah;

                        PenjualanDetail detail = new PenjualanDetail()
                        {
                            BarangID = iIDBarang,
                            Jumlah = iJumlah,
                            Harga = iHarga,
                            Barang = barang
                        };
                        penjualan.PenjualanDetails.Add(detail);

                        list_item_qty_prc.Add(barang.Nama);
                        list_item_qty_prc.Add(iJumlah.ToString());
                        list_item_qty_prc.Add(iHarga.ToString());
                    }
                }
                penjualan.StatusTransaksi = (int)StatusTransaksi.MenungguPembayaran;
                db.Penjualans.Add(penjualan);
                db.SaveChanges();

                string FAKTUR_TITLE = ConfigurationManager.AppSettings["FAKTUR_TITLE"];
                string FAKTUR_SUBTITLE = ConfigurationManager.AppSettings["FAKTUR_SUBTITLE"];
                string FAKTUR_FOOTER = ConfigurationManager.AppSettings["FAKTUR_FOOTER"];
                FAKTUR_TITLE = FAKTUR_TITLE.Trim() == "" ? "-" : FAKTUR_TITLE;
                FAKTUR_SUBTITLE = FAKTUR_SUBTITLE.Trim() == "" ? "-" : FAKTUR_SUBTITLE;
                FAKTUR_FOOTER = FAKTUR_FOOTER.Trim() == "" ? "-" : FAKTUR_FOOTER;
                print_faktur(FAKTUR_TITLE, FAKTUR_SUBTITLE, FAKTUR_FOOTER, penjualan.PenjualanID.ToString().PadLeft(6, '0'), penjualan.JatuhTempo.ToString("dd MMM yyyy"), list_item_qty_prc);

                if (iTop == 0)
                {
                    return RedirectToAction("Edit", new { id = penjualan.PenjualanID });
                }
                else
                {
                    return RedirectToAction("Index", new { page = 1 });
                }
            }


            return View(penjualan);
        }

        [HttpPost]
        public string print_faktur_by_id(int penjualanID)
        {
            string FAKTUR_TITLE = ConfigurationManager.AppSettings["FAKTUR_TITLE"];
            string FAKTUR_SUBTITLE = ConfigurationManager.AppSettings["FAKTUR_SUBTITLE"];
            string FAKTUR_FOOTER = ConfigurationManager.AppSettings["FAKTUR_FOOTER"];
            FAKTUR_TITLE = FAKTUR_TITLE.Trim() == "" ? "-" : FAKTUR_TITLE;
            FAKTUR_SUBTITLE = FAKTUR_SUBTITLE.Trim() == "" ? "-" : FAKTUR_SUBTITLE;
            FAKTUR_FOOTER = FAKTUR_FOOTER.Trim() == "" ? "-" : FAKTUR_FOOTER;

            Penjualan penjualan = db.Penjualans.Where(p => p.PenjualanID == penjualanID).First();
            List<string> list_item_qty_prc = new List<string>();
            foreach (PenjualanDetail detail in penjualan.PenjualanDetails)
            {
                list_item_qty_prc.Add(detail.Barang.Nama);
                list_item_qty_prc.Add(detail.Jumlah.ToString());
                list_item_qty_prc.Add(detail.Harga.ToString());
            }

            print_faktur(FAKTUR_TITLE, FAKTUR_SUBTITLE, FAKTUR_FOOTER, penjualan.PenjualanID.ToString().PadLeft(6, '0'),
                penjualan.JatuhTempo.ToString("dd MMM yyyy"), list_item_qty_prc);

            return "OK";
        }

        public void print_faktur(string FAKTUR_TITLE, string FAKTUR_SUBTITLE, string FAKTUR_FOOTER, string nofaktur, string due_date, List<string> list_item_qty_prc)
        {
            Process p = new Process();
            p.StartInfo.FileName = Path.GetFullPath(Path.Combine(Server.MapPath("~"), "..\\PrintFaktur.exe"));
            p.StartInfo.Arguments = "\"" + FAKTUR_TITLE + "\" \"" + FAKTUR_SUBTITLE + "\" \"" + FAKTUR_FOOTER + "\" \"" + nofaktur + "\" \"" + due_date + "\" ";
            for (int i = 0; i < list_item_qty_prc.Count; i++)
            {
                p.StartInfo.Arguments += "\"" + list_item_qty_prc[i] + "\" ";
            }
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
        }

        //
        // GET: /Penjualan/Edit/5
 
        public ActionResult Edit(int id)
        {
            Penjualan penjualan = db.Penjualans.Find(id);
            ViewBag.TipePembayaranID = new SelectList(db.TipePembayarans, "TipePembayaranID", "Nama", penjualan.TipePembayaranID);
            return View(penjualan);
        }

        //
        // POST: /Penjualan/Edit/5

        [HttpPost]
        public ActionResult Edit(Penjualan penjualan)
        {
            if (ModelState.IsValid)
            {
                penjualan.StatusTransaksi = (int)StatusTransaksi.Lunas;
                penjualan.TanggalBayar = DateTime.Now;
                TipePembayaran pembayaran = db.TipePembayarans.Find(penjualan.TipePembayaranID);
                if (pembayaran.Nama.ToUpper() == "CASH")
                {
                    JenisKasKecil jenis_kas = db.JenisKasKecils.Where(p => p.Nama == "DEBET").First();

                    KasKecil kas = new KasKecil();
                    kas.Tanggal = DateTime.Now;
                    kas.Total = penjualan.Total;
                    kas.JenisKasKecil = jenis_kas;
                    kas.Keterangan = penjualan_prefix_string + penjualan.PenjualanID.ToString().PadLeft(6, '0');
                    db.KasKecils.Add(kas);
                }

                db.Entry(penjualan).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { page = 1 });
            }
            ViewBag.TipePembayaranID = new SelectList(db.TipePembayarans, "TipePembayaranID", "Nama", penjualan.TipePembayaranID);
            return View(penjualan);
        }

        //
        // GET: /Penjualan/Delete/5
 
        public ActionResult Delete(int id)
        {
            Penjualan penjualan = db.Penjualans.Find(id);
            return View(penjualan);
        }

        //
        // POST: /Penjualan/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {    
            Penjualan penjualan = db.Penjualans.Find(id);
            foreach (PenjualanDetail detail in penjualan.PenjualanDetails)
            {
                detail.Barang.Qty += detail.Jumlah;
            }
            if (penjualan.StatusTransaksi == (int)StatusTransaksi.Lunas)
            {
                JenisKasKecil jenis_kas = db.JenisKasKecils.Where(p => p.Nama == "KREDIT").First();
                KasKecil kas = new KasKecil();
                kas.Tanggal = DateTime.Now;
                kas.JenisKasKecil = jenis_kas;
                kas.Total = penjualan.Total;
                kas.Keterangan = penjualan_batal_prefix_string + penjualan.PenjualanID.ToString().PadLeft(6, '0');

                db.KasKecils.Add(kas);
            }
            penjualan.StatusTransaksi = (int)StatusTransaksi.Dibatalkan;
            db.SaveChanges();
            return RedirectToAction("Index", new { page = 1 });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult GetPenjualanDetailByFaktur(string FakturID)
        {
            JsonResult result;
            List<ReturItem> temp = new List<ReturItem>();
            ReturItem ReturItem;

            try
            {
                int iFakturID;
                Int32.TryParse(FakturID, out iFakturID);
                var PenjualanDetails =
                    db.PenjualanDetails
                    .Where(p => p.PenjualanID == iFakturID && (p.Penjualan.StatusTransaksi == (int)StatusTransaksi.Lunas || p.Penjualan.StatusTransaksi == (int)StatusTransaksi.MenungguPembayaran))
                    .Include(p => p.Barang)
                    .Include(p => p.Penjualan).ToList();

                foreach (PenjualanDetail p in PenjualanDetails)
                {
                    ReturItem = new ReturItem()
                    {
                        FakturID = p.PenjualanID,
                        BarangID = p.BarangID,
                        ClientID = p.Penjualan.CustomerID,
                        NamaKlien = p.Penjualan.Customer.Nama,
                        Nama = p.Barang.Nama,
                        Jumlah = p.Jumlah,
                        Harga = p.Harga,
                        isLunas = (p.Penjualan.StatusTransaksi == (int)StatusTransaksi.Lunas)
                    };

                    var ReturPenjualanDetails =
                        db.ReturPenjualanDetails
                        .Where(q => q.PenjualanID == p.PenjualanID && q.BarangID == p.BarangID).ToList();
                    if (ReturPenjualanDetails.Count() > 0)
                    {
                        int jml = ReturPenjualanDetails.Sum(q => q.Jumlah);
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