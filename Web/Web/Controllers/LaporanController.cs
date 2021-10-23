using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class LaporanController : BaseController
    {
        private WebContext db = new WebContext();

        //
        // GET: /Laporan/

        public ActionResult Mutasi()
        {
            ViewBag.CUR_PAGE = 1;
            return View();
        }

        [HttpPost]
        public ActionResult Mutasi(DateTime dari, DateTime sampai, int page)
        {
            DateTime temp;
            if (dari > sampai)
            {
                temp = dari;
                dari = sampai;
                sampai = temp;
            }
            ViewBag.DARI = dari.ToString("dd MMM yyyy");
            ViewBag.SAMPAI = sampai.ToString("dd MMM yyyy"); ;
            ViewBag.CUR_PAGE = page;

            int start = (page - 1) * PAGE_SIZE;
            var barangs = db.Barangs.OrderBy(p => p.Nama);

            List<Barang> Barangs = barangs.ToList();
            List<Mutasi> listmutasi = new List<Mutasi>();

            List<PenjualanDetail> PenjualanDetails;
            List<PembelianDetail> PembelianDetails;
            List<StokAdjustDetail> StokAdjustDetails;
            List<ReturPembelianDetail> ReturPembelianDetails;
            List<ReturPenjualanDetail> ReturPenjualanDetails;
            
            Mutasi mutasi;

            foreach (Barang barang in Barangs)
            {
                mutasi = new Mutasi();
                mutasi.Barang = barang;
                mutasi.BarangID = barang.BarangID;
                mutasi.StokAkhir = barang.Qty;
                mutasi.StokAwal = barang.Qty;
                mutasi.Masuk = 0;
                mutasi.Keluar = 0;
                mutasi.BatalMasuk = 0;
                mutasi.BatalKeluar = 0;
                mutasi.Adjustment = 0;
                listmutasi.Add(mutasi);
            }

            for (DateTime cnt = DateTime.Now.Date; cnt.Date >= dari.Date; cnt = cnt.AddDays(-1))
            {
                PembelianDetails = db.PembelianDetails.Where(p => p.Pembelian.Tanggal.Year == cnt.Year &&
                    p.Pembelian.Tanggal.Month == cnt.Month && p.Pembelian.Tanggal.Day == cnt.Day).ToList();
                foreach (PembelianDetail detail in PembelianDetails)
                {
                    mutasi = listmutasi.Find(p => p.BarangID == detail.BarangID);
                    if (mutasi != null && cnt.Date <= sampai.Date && cnt.Date >= dari.Date)
                    {
                        mutasi.Masuk += detail.Jumlah;
                        if (detail.Pembelian.StatusTransaksi == (int)StatusTransaksi.Dibatalkan)
                        {
                            mutasi.BatalMasuk += detail.Jumlah;
                        }
                    }
                }

                PenjualanDetails = db.PenjualanDetails.Where(p => p.Penjualan.Tanggal.Year == cnt.Year &&
                    p.Penjualan.Tanggal.Month == cnt.Month && p.Penjualan.Tanggal.Day == cnt.Day).ToList();
                foreach (PenjualanDetail detail in PenjualanDetails)
                {
                    mutasi = listmutasi.Find(p => p.BarangID == detail.BarangID);
                    if (mutasi != null && cnt.Date <= sampai.Date && cnt.Date >= dari.Date)
                    {
                        mutasi.Keluar += detail.Jumlah;
                        if (detail.Penjualan.StatusTransaksi == (int)StatusTransaksi.Dibatalkan)
                        {
                            mutasi.BatalKeluar += detail.Jumlah;
                        }
                    }
                }

                StokAdjustDetails = db.StokAdjustDetails.Where(p => p.StokAdjust.Tanggal.Year == cnt.Year &&
                    p.StokAdjust.Tanggal.Month == cnt.Month && p.StokAdjust.Tanggal.Day == cnt.Day).ToList();
                foreach (StokAdjustDetail detail in StokAdjustDetails)
                {
                    mutasi = listmutasi.Find(p => p.BarangID == detail.BarangID);
                    if (mutasi != null && cnt.Date <= sampai.Date && cnt.Date >= dari.Date)
                    {
                        mutasi.Adjustment += (detail.JumlahReal - detail.JumlahDalamData);
                    }
                }

                ReturPembelianDetails = db.ReturPembelianDetails.Where(p => p.ReturPembelian.Tanggal.Year == cnt.Year &&
                    p.ReturPembelian.Tanggal.Month == cnt.Month && p.ReturPembelian.Tanggal.Day == cnt.Day).ToList();
                foreach (ReturPembelianDetail detail in ReturPembelianDetails)
                {
                    mutasi = listmutasi.Find(p => p.BarangID == detail.BarangID);
                    if (mutasi != null && cnt.Date <= sampai.Date && cnt.Date >= dari.Date)
                    {
                        mutasi.BatalMasuk += detail.Jumlah;
                    }
                }

                ReturPenjualanDetails = db.ReturPenjualanDetails.Where(p => p.ReturPenjualan.Tanggal.Year == cnt.Year &&
                    p.ReturPenjualan.Tanggal.Month == cnt.Month && p.ReturPenjualan.Tanggal.Day == cnt.Day).ToList();
                foreach (ReturPenjualanDetail detail in ReturPenjualanDetails)
                {
                    mutasi = listmutasi.Find(p => p.BarangID == detail.BarangID);
                    if (mutasi != null && cnt.Date <= sampai.Date && cnt.Date >= dari.Date)
                    {
                        mutasi.BatalKeluar += detail.Jumlah;
                    }
                }
            }

            List<Mutasi> toRemove = new List<Mutasi>();
            foreach (Mutasi mt in listmutasi)
            {
                mt.StokAwal = mt.StokAkhir + mt.Keluar - mt.Masuk + mt.BatalMasuk - mt.BatalKeluar - mt.Adjustment;
                if (mt.Keluar == 0 && mt.Masuk == 0 && mt.BatalKeluar == 0 && mt.BatalMasuk == 0 && mt.Adjustment == 0)
                {
                    toRemove.Add(mt);
                }
            }

            foreach (Mutasi mt in toRemove)
            {
                listmutasi.Remove(mt);
            }

            int jml_item = listmutasi.Count();

            ViewBag.NUM_OF_DATA = jml_item;
            ViewBag.NUM_OF_PAGE = jml_item / PAGE_SIZE + (jml_item % PAGE_SIZE > 0 ? 1 : 0);
            ViewBag.CUR_PAGE = page;
            ViewBag.START = start + 1;
            ViewBag.END = (start + PAGE_SIZE) < jml_item ? (start + PAGE_SIZE) : jml_item;
            
            return View(listmutasi.Skip(start).Take(PAGE_SIZE));
        }

        public ActionResult Hutang()
        {
            List<Pembelian> Pembelians = db.Pembelians.Where(p => p.StatusTransaksi == (int)StatusTransaksi.MenungguPembayaran).ToList();
            List<Hutang> Hutangs = new List<Hutang>();
            Hutang hutang;
            foreach (Pembelian pembelian in Pembelians)
            {
                hutang = new Hutang()
                {
                    SupplierID = pembelian.SupplierID,
                    Supplier = pembelian.Supplier,
                    Total = pembelian.Total,
                    PembelianID = pembelian.PembelianID,
                    JatuhTempo = pembelian.JatuhTempo
                };
                Hutangs.Add(hutang);
            }
            return View(Hutangs);
        }

        public ActionResult Piutang()
        {
            List<Penjualan> Penjualans = db.Penjualans.Where(p => p.StatusTransaksi == (int)StatusTransaksi.MenungguPembayaran).ToList();
            List<Piutang> Piutangs = new List<Piutang>();
            Piutang piutang;
            foreach (Penjualan penjualan in Penjualans)
            {
                piutang = new Piutang()
                {
                    CustomerID = penjualan.CustomerID,
                    Customer = penjualan.Customer,
                    Total = penjualan.Total,
                    PenjualanID = penjualan.PenjualanID,
                    JatuhTempo = penjualan.JatuhTempo
                };
                Piutangs.Add(piutang);
            }
            return View(Piutangs);
        }

        public ActionResult HistoryPembelian(int page)
        {
            int start = (page - 1) * PAGE_SIZE;
            var pembelians =
                db.Pembelians
                .Include(p => p.Supplier)
                .Include(p => p.TipePembayaran)
                .OrderByDescending(p => p.Tanggal);
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

        public ActionResult HistoryPembelianDetails(int id)
        {
            Pembelian pembelian = db.Pembelians.Find(id);
            List<ReturPembelianDetail> ReturPembelianDetails = db.ReturPembelianDetails.Where(p => p.PembelianID == id).ToList();
            ViewBag.RETURPEMBELIANDETAILS = ReturPembelianDetails;
            return View(pembelian);
        }

        public ActionResult HistoryReturPembelian(int page)
        {
            int start = (page - 1) * PAGE_SIZE;
            var returpembelians =
                db.ReturPembelians
                .Include(p => p.Supplier)
                .Include(p => p.TipePembayaran)
                .OrderByDescending(p => p.Tanggal);
            int jml_item = returpembelians.Count();

            ViewBag.NUM_OF_DATA = jml_item;
            ViewBag.NUM_OF_PAGE = jml_item / PAGE_SIZE + (jml_item % PAGE_SIZE > 0 ? 1 : 0);
            ViewBag.CUR_PAGE = page;
            ViewBag.START = start + 1;
            ViewBag.END = (start + PAGE_SIZE) < jml_item ? (start + PAGE_SIZE) : jml_item;

            List<ReturPembelian> ReturPembelians = returpembelians.Skip(start).Take(PAGE_SIZE).ToList();

            return View(ReturPembelians);
        }

        public ActionResult HistoryReturPembelianDetails(int id)
        {
            ReturPembelian returpembelian = db.ReturPembelians.Find(id);
            return View(returpembelian);
        }

        public ActionResult HistoryPenjualan(int page)
        {
            int start = (page - 1) * PAGE_SIZE;
            var penjualans =
                db.Penjualans
                .Include(p => p.Customer)
                .Include(p => p.TipePembayaran)
                .OrderByDescending(p => p.Tanggal);
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

        public ActionResult HistoryPenjualanDetails(int id)
        {
            Penjualan penjualan = db.Penjualans.Find(id);
            List<ReturPenjualanDetail> ReturPenjualanDetails = db.ReturPenjualanDetails.Where(p => p.PenjualanID == id).ToList();
            ViewBag.RETURPENJUALANDETAILS = ReturPenjualanDetails;
            return View(penjualan);
        }

        public ActionResult HistoryReturPenjualan(int page)
        {
            int start = (page - 1) * PAGE_SIZE;
            var returpenjualans =
                db.ReturPenjualans
                .Include(p => p.Customer)
                .Include(p => p.TipePembayaran)
                .OrderByDescending(p => p.Tanggal);
            int jml_item = returpenjualans.Count();

            ViewBag.NUM_OF_DATA = jml_item;
            ViewBag.NUM_OF_PAGE = jml_item / PAGE_SIZE + (jml_item % PAGE_SIZE > 0 ? 1 : 0);
            ViewBag.CUR_PAGE = page;
            ViewBag.START = start + 1;
            ViewBag.END = (start + PAGE_SIZE) < jml_item ? (start + PAGE_SIZE) : jml_item;

            List<ReturPenjualan> ReturPenjualans = returpenjualans.Skip(start).Take(PAGE_SIZE).ToList();

            return View(ReturPenjualans);
        }

        public ActionResult HistoryReturPenjualanDetails(int id)
        {
            ReturPenjualan returpenjualan = db.ReturPenjualans.Find(id);
            return View(returpenjualan);
        }

        public ActionResult Periode(int page)
        {
            List<Pembelian> Pembelians = db.Pembelians.ToList();
            List<Penjualan> Penjualans = db.Penjualans.ToList();
            
            List<ReturPembelian> ReturPembelians = db.ReturPembelians.ToList();
            List<ReturPembelianDetail> ReturPembelianDetails = db.ReturPembelianDetails.ToList();
            List<ReturPembelian> tempReturPembelians = new List<ReturPembelian>();
            List<ReturPembelianDetail> tempReturPembelianDetails = new List<ReturPembelianDetail>();

            List<ReturPenjualan> ReturPenjualans = db.ReturPenjualans.ToList();
            List<ReturPenjualanDetail> ReturPenjualanDetails = db.ReturPenjualanDetails.ToList();
            List<ReturPenjualan> tempReturPenjualans = new List<ReturPenjualan>();
            List<ReturPenjualanDetail> tempReturPenjualanDetails = new List<ReturPenjualanDetail>();

            List<LaporanPeriode> LaporanPeriodes = new List<LaporanPeriode>();
            KasKecil[] kasKecils = db.KasKecils.OrderBy(p => p.Tanggal).ToArray();
            LaporanPeriode periode = null;
            int total, temp_jml;

            foreach (KasKecil kas in kasKecils)
            {
                if(kas.Keterangan == day_opening_string)
                {
                    periode = new LaporanPeriode();
                    periode.Mulai = kas.Tanggal;
                    periode.KasAwal = kas.Total;
                    periode.KasAkhir = kas.Total;
                    periode.Penerimaan = 0;
                    periode.Pengeluaran = 0;
                    periode.TotalPenjualan = 0;
                    periode.PenjualanBatal = 0;
                    periode.TotalPembelian = 0;
                    periode.PembelianBatal = 0;
                    periode.KasAwalID = kas.KasKecilID;
                }
                else if (kas.Keterangan == day_closing_string)
                {
                    periode.Selesai = kas.Tanggal;
                    //cari total penjualan dan pembelian di periode;
                    total = (
                        from g in Pembelians where 
                            g.Tanggal >= periode.Mulai && 
                            g.Tanggal < periode.Selesai
                        select g.Total).Sum();
                    periode.TotalPembelian = total;

                    total = (
                        from g in Pembelians
                        where
                            g.Tanggal >= periode.Mulai &&
                            g.Tanggal < periode.Selesai &&
                            (g.StatusTransaksi == (int)StatusTransaksi.Dibatalkan ||
                            g.StatusTransaksi == (int)StatusTransaksi.Diputihkan)
                        select g.Total).Sum();
                    periode.PembelianBatal = total;

                    /*
                    total = (
                        from g in ReturPembelians
                        where
                            g.Tanggal >= periode.Mulai &&
                            g.Tanggal < periode.Selesai
                        select g.Total).Sum();
                    periode.PembelianBatal += total;
                    */
                    tempReturPembelians = ReturPembelians.Where(g => g.Tanggal >= periode.Mulai && g.Tanggal < periode.Selesai).ToList();
                    tempReturPembelianDetails = ReturPembelianDetails.Where(g => tempReturPembelians.Where(h => h.ReturPembelianID == g.ReturPembelianID).Count() > 0).ToList();
                    temp_jml = 0;
                    for (int i = 0; i < tempReturPembelianDetails.Count; i++)
                    {
                        temp_jml += (tempReturPembelianDetails[i].Harga * tempReturPembelianDetails[i].Jumlah);
                    }
                    periode.PembelianBatal += temp_jml;

                    total = (
                        from g in Penjualans where 
                            g.Tanggal >= periode.Mulai &&
                            g.Tanggal < periode.Selesai
                        select g.Total).Sum();
                    periode.TotalPenjualan = total;

                    total = (
                        from g in Penjualans
                        where
                            g.Tanggal >= periode.Mulai &&
                            g.Tanggal < periode.Selesai &&
                            (g.StatusTransaksi == (int)StatusTransaksi.Dibatalkan ||
                            g.StatusTransaksi == (int)StatusTransaksi.Diputihkan)
                        select g.Total).Sum();
                    periode.PenjualanBatal = total;
                    
                    /*
                    total = (
                        from g in ReturPenjualans
                        where
                            g.Tanggal >= periode.Mulai &&
                            g.Tanggal < periode.Selesai
                        select g.Total).Sum();
                    periode.PenjualanBatal += total;
                    */
                    tempReturPenjualans = ReturPenjualans.Where(g => g.Tanggal >= periode.Mulai && g.Tanggal < periode.Selesai).ToList();
                    tempReturPenjualanDetails = ReturPenjualanDetails.Where(g => tempReturPenjualans.Where(h => h.ReturPenjualanID == g.ReturPenjualanID).Count() > 0).ToList();
                    temp_jml = 0;
                    for (int i = 0; i < tempReturPenjualanDetails.Count; i++)
                    {
                        temp_jml += (tempReturPenjualanDetails[i].Harga * tempReturPenjualanDetails[i].Jumlah);
                    }
                    periode.PenjualanBatal += temp_jml;

                    LaporanPeriodes.Add(periode);
                    periode.KasAkhirID = kas.KasKecilID;
                }
                else if (kas.Keterangan.StartsWith(penjualan_prefix_string))
                {
                    periode.Penerimaan += kas.Total;
                    periode.KasAkhir += kas.Total;
                }
                else if (kas.Keterangan.StartsWith(pembelian_prefix_string))
                {
                    periode.Pengeluaran += kas.Total;
                    periode.KasAkhir -= kas.Total;
                }
                else if (kas.Keterangan.StartsWith(penjualan_batal_prefix_string) || kas.Keterangan.StartsWith(penjualan_retur_prefix_string))
                {
                    periode.Penerimaan -= kas.Total;
                    periode.KasAkhir -= kas.Total;
                }
                else if (kas.Keterangan.StartsWith(pembelian_batal_prefix_string) || kas.Keterangan.StartsWith(pembelian_retur_prefix_string))
                {
                    periode.Pengeluaran -= kas.Total;
                    periode.KasAkhir += kas.Total;
                }
                else if (kas.JenisKasKecil.Nama == "DEBET")
                {
                    periode.Penerimaan += kas.Total;
                    periode.KasAkhir += kas.Total;
                }
                else if (kas.JenisKasKecil.Nama == "KREDIT")
                {
                    periode.Pengeluaran += kas.Total;
                    periode.KasAkhir -= kas.Total;
                }
            }

            //return View(LaporanPeriodes);

            int start = (page - 1) * PAGE_SIZE;
            //var lokasis = db.Lokasis.OrderBy(p => p.Nama);
            int jml_item = LaporanPeriodes.Count();

            ViewBag.NUM_OF_DATA = jml_item;
            ViewBag.NUM_OF_PAGE = jml_item / PAGE_SIZE + (jml_item % PAGE_SIZE > 0 ? 1 : 0);
            ViewBag.CUR_PAGE = page;
            ViewBag.START = start + 1;
            ViewBag.END = (start + PAGE_SIZE) < jml_item ? (start + PAGE_SIZE) : jml_item;

            return View(LaporanPeriodes.Skip(start).Take(PAGE_SIZE));
        }

        public ActionResult Bulanan()
        {

            List<Pembelian> Pembelians = db.Pembelians.ToList();
            List<Penjualan> Penjualans = db.Penjualans.ToList();
            List<ReturPembelian> ReturPembelians = db.ReturPembelians.ToList();
            List<ReturPembelianDetail> ReturPembelianDetails = db.ReturPembelianDetails.ToList();
            List<ReturPembelian> tempReturPembelians = new List<ReturPembelian>();
            List<ReturPembelianDetail> tempReturPembelianDetails = new List<ReturPembelianDetail>();
            
            List<ReturPenjualan> ReturPenjualans = db.ReturPenjualans.ToList();
            List<ReturPenjualanDetail> ReturPenjualanDetails = db.ReturPenjualanDetails.ToList();
            List<ReturPenjualan> tempReturPenjualans = new List<ReturPenjualan>();
            List<ReturPenjualanDetail> tempReturPenjualanDetails = new List<ReturPenjualanDetail>();

            List<KasKecil> KasKecils = db.KasKecils.ToList();

            List<LaporanBulanan> LaporanBulanans = new List<LaporanBulanan>();
            LaporanBulanan bulanan = null;

            DateTime datestart = DateTime.Now.AddDays(1000);
            datestart = Pembelians.Count > 0 ? Pembelians[0].Tanggal : datestart;
            datestart = Penjualans.Count > 0 && Penjualans[0].Tanggal < datestart ? Penjualans[0].Tanggal : datestart;
            datestart = KasKecils.Count > 0 && KasKecils[0].Tanggal < datestart ? KasKecils[0].Tanggal : datestart;
            DateTime today = DateTime.Now;

            datestart = new DateTime(datestart.Year, datestart.Month, 1);
            int temp_jml;
            while (datestart <= today)
            {
                bulanan = new LaporanBulanan();
                bulanan.Bulan = datestart;

                bulanan.Penjualan =
                    (from g in Penjualans
                     where g.Tanggal.Year == datestart.Year && g.Tanggal.Month == datestart.Month
                     select g.Total).Sum();
                bulanan.PenjualanBatal =
                    (from g in Penjualans
                     where g.Tanggal.Year == datestart.Year && g.Tanggal.Month == datestart.Month &&
                         g.StatusTransaksi == (int)StatusTransaksi.Dibatalkan
                     select g.Total).Sum();
                tempReturPenjualans = ReturPenjualans.Where(g => g.Tanggal.Year == datestart.Year && g.Tanggal.Month == datestart.Month).ToList();
                tempReturPenjualanDetails = ReturPenjualanDetails.Where(g => tempReturPenjualans.Where(h=>h.ReturPenjualanID == g.ReturPenjualanID).Count() > 0).ToList();
                temp_jml = 0;
                for (int i = 0; i < tempReturPenjualanDetails.Count; i++)
                {
                    temp_jml += (tempReturPenjualanDetails[i].Harga * tempReturPenjualanDetails[i].Jumlah);
                }
                bulanan.PenjualanBatal += temp_jml;
                //bulanan.PenjualanBatal +=
                //    (from g in ReturPenjualans
                //     where g.Tanggal.Year == datestart.Year && g.Tanggal.Month == datestart.Month
                //     select g.Total).Sum();

                bulanan.Pembelian =
                    (from g in Pembelians
                     where g.Tanggal.Year == datestart.Year && g.Tanggal.Month == datestart.Month
                     select g.Total).Sum();
                bulanan.PembelianBatal =
                    (from g in Pembelians
                     where g.Tanggal.Year == datestart.Year && g.Tanggal.Month == datestart.Month &&
                         g.StatusTransaksi == (int)StatusTransaksi.Dibatalkan
                     select g.Total).Sum();
                tempReturPembelians = ReturPembelians.Where(g => g.Tanggal.Year == datestart.Year && g.Tanggal.Month == datestart.Month).ToList();
                tempReturPembelianDetails = ReturPembelianDetails.Where(g => tempReturPembelians.Where(h => h.ReturPembelianID == g.ReturPembelianID).Count() > 0).ToList();
                temp_jml = 0;
                for (int i = 0; i < tempReturPembelianDetails.Count; i++)
                {
                    temp_jml += (tempReturPembelianDetails[i].Harga * tempReturPembelianDetails[i].Jumlah);
                }
                bulanan.PembelianBatal += temp_jml;
                //bulanan.PembelianBatal +=
                //    (from g in ReturPembelians
                //     where g.Tanggal.Year == datestart.Year && g.Tanggal.Month == datestart.Month
                //     select g.Total).Sum();

                int temp;
                bulanan.Penerimaan =
                    (from g in KasKecils
                     where g.Tanggal.Year == datestart.Year && 
                        g.Tanggal.Month == datestart.Month && 
                        g.JenisKasKecil.Nama == "DEBET" && 
                        !g.Keterangan.StartsWith(day_opening_string) &&
                        !g.Keterangan.StartsWith(penjualan_prefix_string) &&
                        !g.Keterangan.StartsWith(pembelian_retur_prefix_string)
                     select g.Total).Sum();
                temp =
                    (from g in Penjualans
                     where
                        g.TanggalBayar.HasValue &&
                        g.TanggalBayar.Value.Year == datestart.Year &&
                        g.TanggalBayar.Value.Month == datestart.Month &&
                        g.StatusTransaksi == ((int)StatusTransaksi.Lunas)
                     select g.Total).Sum();
                bulanan.Penerimaan += temp;
                temp = 
                    (from g in ReturPembelians
                     where
                        g.TanggalBayar.HasValue &&
                        g.TanggalBayar.Value.Year == datestart.Year &&
                        g.TanggalBayar.Value.Month == datestart.Month &&
                        g.StatusTransaksi == ((int)StatusTransaksi.Lunas)
                     select g.Total).Sum();
                bulanan.Penerimaan += temp;

                bulanan.Pengeluaran =
                    (from g in KasKecils 
                     where g.Tanggal.Year == datestart.Year && 
                        g.Tanggal.Month == datestart.Month && 
                        g.JenisKasKecil.Nama == "KREDIT" && 
                        !g.Keterangan.StartsWith(day_closing_string) &&
                        !g.Keterangan.StartsWith(pembelian_prefix_string) &&
                        !g.Keterangan.StartsWith(penjualan_retur_prefix_string)
                     select g.Total).Sum();
                temp =
                    (from g in Pembelians
                     where
                        g.TanggalBayar.HasValue &&
                        g.TanggalBayar.Value.Year == datestart.Year &&
                        g.TanggalBayar.Value.Month == datestart.Month &&
                        g.StatusTransaksi == ((int)StatusTransaksi.Lunas)
                     select g.Total).Sum();
                bulanan.Pengeluaran += temp;
                temp =
                    (from g in ReturPenjualans
                     where
                        g.TanggalBayar.HasValue &&
                        g.TanggalBayar.Value.Year == datestart.Year &&
                        g.TanggalBayar.Value.Month == datestart.Month &&
                        g.StatusTransaksi == ((int)StatusTransaksi.Lunas)
                     select g.Total).Sum();
                bulanan.Pengeluaran += temp;


                LaporanBulanans.Add(bulanan);
                datestart = datestart.AddMonths(1);
            }

            return View(LaporanBulanans);
        }
    }
}
