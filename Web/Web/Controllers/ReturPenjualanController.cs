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
    public class ReturPenjualanController : BaseController
    {
        private WebContext db = new WebContext();

        public ViewResult Index(int page)
        {
            int start = (page - 1) * PAGE_SIZE;
            var retur_penjualan = db.ReturPenjualans
                .Where(p => p.Tanggal >= PeriodeStart)
                .Include(p => p.ReturPenjualanDetails)
                .Include(p => p.Customer)
                .OrderByDescending(p => p.Tanggal);
            int jml_item = retur_penjualan.Count();

            ViewBag.NUM_OF_DATA = jml_item;
            ViewBag.NUM_OF_PAGE = jml_item / PAGE_SIZE + (jml_item % PAGE_SIZE > 0 ? 1 : 0);
            ViewBag.CUR_PAGE = page;
            ViewBag.START = start + 1;
            ViewBag.END = (start + PAGE_SIZE) < jml_item ? (start + PAGE_SIZE) : jml_item;

            return View(retur_penjualan.Skip(start).Take(PAGE_SIZE).ToList());
        }

        public ViewResult IndexByFaktur(int PenjualanID)
        {
            List<ReturPenjualanDetail> ReturPenjualanDetails = db.ReturPenjualanDetails
                .Where(p => p.PenjualanID == PenjualanID).ToList();
            List<int> retur_ids = ReturPenjualanDetails.Select(p => p.ReturPenjualanID).ToList();
            List<ReturPenjualan> ReturPenjualans = db.ReturPenjualans.Where(p => retur_ids.Contains(p.ReturPenjualanID)).ToList();
            ViewBag.PENJUALANID = PenjualanID;
            return View(ReturPenjualans);
        }

        public ViewResult Details(int id, int? fid)
        {
            ReturPenjualan returpenjualan = db.ReturPenjualans.Find(id);
            if (fid != null)
            {
                ViewBag.PENJUALANID = fid;
            }
            return View(returpenjualan);
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(ReturPenjualan returpenjualan)
        {
            int row_cnt, iFakturID, iBarangID, iJumlah, iHarga, iCustomerID;
            bool isLunas;
            Int32.TryParse(Request["hid_row_cnt"], out row_cnt);
            Penjualan penjualan;
            Barang barang;

            returpenjualan.UserName = User.Identity.Name;
            returpenjualan.Tanggal = DateTime.Now;
            returpenjualan.StatusTransaksi = (int)StatusTransaksi.MenungguPembayaran;

            if (ModelState.IsValid)
            {
                returpenjualan.ReturPenjualanDetails = new List<ReturPenjualanDetail>();

                for (int i = 0; i < row_cnt; i++)
                {
                    if (Int32.TryParse(Request["klien_" + i], out iCustomerID))
                    {
                        returpenjualan.CustomerID = iCustomerID;
                    }

                    if (Int32.TryParse(Request["faktur_" + i], out iFakturID))
                    {
                        Int32.TryParse(Request["barang_" + i], out iBarangID);
                        Int32.TryParse(Request["jumlah_" + i], out iJumlah);
                        Int32.TryParse(Request["harga_" + i], out iHarga);
                        isLunas = Boolean.Parse(Request["status_" + i]);

                        // buat data retur 
                        returpenjualan.ReturPenjualanDetails.Add(new ReturPenjualanDetail()
                        {
                            PenjualanID = iFakturID,
                            BarangID = iBarangID,
                            Jumlah = iJumlah,
                            Harga = iHarga,
                            isPaid = isLunas
                        });

                        // cari penjualan dan barang terkait
                        penjualan = db.Penjualans.Find(iFakturID);
                        barang = penjualan.PenjualanDetails.Where(p => p.BarangID == iBarangID).First().Barang;
                        barang.Qty += iJumlah;
                    }
                }

                db.ReturPenjualans.Add(returpenjualan);
                db.SaveChanges();

                return RedirectToAction("Edit", new { id = returpenjualan.ReturPenjualanID });
            }

            return View(returpenjualan);
        }

        public ActionResult Edit(int id, int? fid)
        {
            ReturPenjualan returpenjualan = db.ReturPenjualans.Find(id);
            ViewBag.TipePembayaranID = new SelectList(db.TipePembayarans, "TipePembayaranID", "Nama", returpenjualan.TipePembayaranID);

            if (fid != null)
            {
                ViewBag.PENJUALANID = fid;
            }
            return View(returpenjualan);
        }

        [HttpPost]
        public ActionResult Edit(ReturPenjualan returpenjualan, int? fid)
        {
            if (fid != null)
            {
                ViewBag.PENJUALANID = fid;
            }

            if (ModelState.IsValid)
            {
                returpenjualan.StatusTransaksi = (int)StatusTransaksi.Lunas;
                returpenjualan.TanggalBayar = DateTime.Now;
                TipePembayaran pembayaran = db.TipePembayarans.Find(returpenjualan.TipePembayaranID);
                int total_bayar = 0;

                List<ReturPenjualanDetail> returPenjualanDetails = db.ReturPenjualanDetails.Where(p => p.ReturPenjualanID == returpenjualan.ReturPenjualanID).ToList();
                foreach (ReturPenjualanDetail detail in returPenjualanDetails)
                {
                    if (detail.isPaid)
                    {
                        total_bayar += (detail.Harga * detail.Jumlah);
                    }
                }

                if (total_bayar > 0 && pembayaran.Nama.ToUpper() == "CASH")
                {
                    JenisKasKecil jenis_kas = db.JenisKasKecils.Where(p => p.Nama == "KREDIT").First();
                    KasKecil kas = new KasKecil();
                    kas.Tanggal = DateTime.Now;
                    kas.JenisKasKecil = jenis_kas;
                    kas.Total = total_bayar;
                    kas.Keterangan = penjualan_retur_prefix_string + returpenjualan.ReturPenjualanID.ToString().PadLeft(6, '0');

                    db.KasKecils.Add(kas);
                }

                db.Entry(returpenjualan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { page = 1 });
            }
            ViewBag.TipePembayaranID = new SelectList(db.TipePembayarans, "TipePembayaranID", "Nama", returpenjualan.TipePembayaranID);

            return View(returpenjualan);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}