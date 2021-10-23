using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{ 
    public class ReturPembelianController : BaseController
    {
        private WebContext db = new WebContext();

        public ViewResult Index(int page)
        {
            int start = (page - 1) * PAGE_SIZE;
            var retur_pembelian = db.ReturPembelians
                .Where(p => p.Tanggal >= PeriodeStart)
                .Include(p => p.ReturPembelianDetails)
                .Include(p => p.Supplier)
                .OrderByDescending(p => p.Tanggal);
            int jml_item = retur_pembelian.Count();

            ViewBag.NUM_OF_DATA = jml_item;
            ViewBag.NUM_OF_PAGE = jml_item / PAGE_SIZE + (jml_item % PAGE_SIZE > 0 ? 1 : 0);
            ViewBag.CUR_PAGE = page;
            ViewBag.START = start + 1;
            ViewBag.END = (start + PAGE_SIZE) < jml_item ? (start + PAGE_SIZE) : jml_item;

            return View(retur_pembelian.Skip(start).Take(PAGE_SIZE).ToList());
        }

        public ViewResult IndexByFaktur(int PembelianID)
        {
            List<ReturPembelianDetail> ReturPembelianDetails = db.ReturPembelianDetails
                .Where(p => p.PembelianID == PembelianID).ToList();
            List<int> retur_ids = ReturPembelianDetails.Select(p => p.ReturPembelianID).ToList();
            List<ReturPembelian> ReturPembelians = db.ReturPembelians.Where(p => retur_ids.Contains(p.ReturPembelianID)).ToList();
            ViewBag.PEMBELIANID = PembelianID;
            return View(ReturPembelians);
        }

        public ViewResult Details(int id, int? fid)
        {
            ReturPembelian returpembelian = db.ReturPembelians.Find(id);
            if (fid != null)
            {
                ViewBag.PEMBELIANID = fid;
            }
            return View(returpembelian);
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(ReturPembelian returpembelian)
        {
            int row_cnt, pembagi, iFakturID, iBarangID, iJumlah, iHarga, iSupplierID;
            bool isLunas;
            Int32.TryParse(Request["hid_row_cnt"], out row_cnt);
            Pembelian pembelian;
            Barang barang;

            returpembelian.UserName = User.Identity.Name;
            returpembelian.Tanggal = DateTime.Now;
            returpembelian.StatusTransaksi = (int)StatusTransaksi.MenungguPembayaran;

            if (ModelState.IsValid)
            {
                returpembelian.ReturPembelianDetails = new List<ReturPembelianDetail>();

                for (int i = 0; i < row_cnt; i++)
                {
                    if (Int32.TryParse(Request["klien_" + i], out iSupplierID))
                    {
                        returpembelian.SupplierID = iSupplierID;
                    }

                    if (Int32.TryParse(Request["faktur_" + i], out iFakturID))
                    {
                        Int32.TryParse(Request["barang_" + i], out iBarangID);
                        Int32.TryParse(Request["jumlah_" + i], out iJumlah);
                        Int32.TryParse(Request["harga_" + i], out iHarga);
                        isLunas = Boolean.Parse(Request["status_" + i]);

                        // buat data retur 
                        returpembelian.ReturPembelianDetails.Add(new ReturPembelianDetail(){
                            PembelianID = iFakturID,
                            BarangID = iBarangID,
                            Jumlah = iJumlah,
                            Harga = iHarga,
                            isPaid = isLunas
                        });

                        // cari pembelian dan barang terkait
                        pembelian = db.Pembelians.Find(iFakturID);
                        barang = pembelian.PembelianDetails.Where(p => p.BarangID == iBarangID).First().Barang;

                        // hitung data master
                        pembagi = (barang.Qty - iJumlah) == 0 ? 1 : (barang.Qty - iJumlah);
                        barang.HargaRataan =
                            ((barang.HargaRataan * barang.Qty) - (iJumlah * iHarga)) / pembagi;
                        barang.Qty -= iJumlah;
                    }
                }

                db.ReturPembelians.Add(returpembelian);
                db.SaveChanges();

                return RedirectToAction("Edit", new { id = returpembelian.ReturPembelianID });
            }

            return View(returpembelian);
        }

        public ActionResult Edit(int id, int? fid)
        {
            ReturPembelian returpembelian = db.ReturPembelians.Find(id);
            ViewBag.TipePembayaranID = new SelectList(db.TipePembayarans, "TipePembayaranID", "Nama", returpembelian.TipePembayaranID);

            if (fid != null)
            {
                ViewBag.PEMBELIANID = fid;
            }
            return View(returpembelian);
        }

        [HttpPost]
        public ActionResult Edit(ReturPembelian returpembelian, int? fid)
        {
            if (fid != null)
            {
                ViewBag.PEMBELIANID = fid;
            }

            if (ModelState.IsValid)
            {
                returpembelian.StatusTransaksi = (int)StatusTransaksi.Lunas;
                returpembelian.TanggalBayar = DateTime.Now;
                TipePembayaran pembayaran = db.TipePembayarans.Find(returpembelian.TipePembayaranID);
                int total_bayar = 0;

                List<ReturPembelianDetail> returPembelianDetails = db.ReturPembelianDetails.Where(p => p.ReturPembelianID == returpembelian.ReturPembelianID).ToList();
                foreach (ReturPembelianDetail detail in returPembelianDetails)
                {
                    if (detail.isPaid)
                    {
                        total_bayar += (detail.Harga * detail.Jumlah);
                    }
                }

                if (total_bayar > 0 && pembayaran.Nama.ToUpper() == "CASH")
                {
                    JenisKasKecil jenis_kas = db.JenisKasKecils.Where(p => p.Nama == "DEBET").First();
                    KasKecil kas = new KasKecil();
                    kas.Tanggal = DateTime.Now;
                    kas.JenisKasKecil = jenis_kas;
                    kas.Total = total_bayar;
                    kas.Keterangan = pembelian_retur_prefix_string + returpembelian.ReturPembelianID.ToString().PadLeft(6, '0');

                    db.KasKecils.Add(kas);
                }

                db.Entry(returpembelian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { page = 1 });
            }
            ViewBag.TipePembayaranID = new SelectList(db.TipePembayarans, "TipePembayaranID", "Nama", returpembelian.TipePembayaranID);

            return View(returpembelian);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}