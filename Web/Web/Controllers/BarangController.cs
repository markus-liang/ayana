using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ExcelLibrary.SpreadSheet;
using Web.Models;

namespace Web.Controllers
{
    public class BarangController : BaseController
    {
        private WebContext db = new WebContext();

        public ViewResult Index(int page, string filter)
        {
            IQueryable<Barang> barangs;
            int start = (page-1)*PAGE_SIZE;
            if (filter == null || filter.Trim() == "")
            {
                barangs = db.Barangs.Include(b => b.JenisBarang).Include(b => b.Lokasi).OrderBy(p => p.Nama);
            }
            else
            {
                filter = filter.Trim();
                barangs = db.Barangs
                    .Where(p => p.Nama.Contains(filter) || p.Lokasi.Nama.Contains(filter) || p.JenisBarang.Nama.Contains(filter))
                    .Include(b => b.JenisBarang)
                    .Include(b => b.Lokasi)
                    .OrderBy(p => p.Nama);
            }
            int jml_item = barangs.Count();

            ViewBag.ISPAGING = true;
            ViewBag.NUM_OF_DATA = jml_item;
            ViewBag.NUM_OF_PAGE = jml_item / PAGE_SIZE + (jml_item % PAGE_SIZE > 0 ? 1 : 0);
            ViewBag.CUR_PAGE = page;
            ViewBag.START = start + 1;
            ViewBag.END = (start + PAGE_SIZE) < jml_item ? (start + PAGE_SIZE) : jml_item;
            ViewBag.FILTER = filter;

            return View(barangs.OrderBy(p => p.Nama).Skip(start).Take(PAGE_SIZE).ToList());
        }

        public ViewResult Details(int id)
        {
            Barang barang = db.Barangs.Find(id);
            return View(barang);
        }

        //
        // GET: /Barang/Create

        public ActionResult Create()
        {
            ViewBag.JenisBarangID = new SelectList(db.JenisBarangs, "JenisBarangID", "Nama");
            ViewBag.LokasiID = new SelectList(db.Lokasis, "LokasiID", "Nama");
            return View();
        } 

        //
        // POST: /Barang/Create

        [HttpPost]
        public ActionResult Create(Barang barang)
        {
            barang.Qty = 0;
            if (ModelState.IsValid)
            {
                db.Barangs.Add(barang);
                db.SaveChanges();
                return RedirectToAction("Index", new { @page=1 });  
            }

            ViewBag.JenisBarangID = new SelectList(db.JenisBarangs, "JenisBarangID", "Nama", barang.JenisBarangID);
            ViewBag.LokasiID = new SelectList(db.Lokasis, "LokasiID", "Nama", barang.LokasiID);
            return View(barang);
        }
        
        //
        // GET: /Barang/Edit/5
 
        public ActionResult Edit(int id, int return_page)
        {
            Barang barang = db.Barangs.Find(id);
            ViewBag.JenisBarangID = new SelectList(db.JenisBarangs, "JenisBarangID", "Nama", barang.JenisBarangID);
            ViewBag.LokasiID = new SelectList(db.Lokasis, "LokasiID", "Nama", barang.LokasiID);
            ViewBag.ReturnPage = return_page;
            return View(barang);
        }

        //
        // POST: /Barang/Edit/5

        [HttpPost]
        public ActionResult Edit(Barang barang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(barang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { @page = Request["return_page"] });
            }
            ViewBag.JenisBarangID = new SelectList(db.JenisBarangs, "JenisBarangID", "Nama", barang.JenisBarangID);
            ViewBag.LokasiID = new SelectList(db.Lokasis, "LokasiID", "Nama", barang.LokasiID);
            return View(barang);
        }

        //
        // GET: /Barang/Delete/5
 
        public ActionResult Delete(int id)
        {
            Barang barang = db.Barangs.Find(id);
            return View(barang);
        }

        //
        // POST: /Barang/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Barang barang = db.Barangs.Find(id);
            db.Barangs.Remove(barang);
            db.SaveChanges();
            return RedirectToAction("Index", new { @page = 1 });
        }

        [HttpPost]
        public string toExcel(string filename)
        {
            try
            {
                List<Barang> barangs = db.Barangs.OrderBy(p => p.Nama).ToList();

                //create new xls file
                Workbook workbook = new Workbook();
                Worksheet worksheet = new Worksheet("Sheet Barang");
                worksheet.Cells[0, 0] = new Cell("JENIS");
                worksheet.Cells[0, 1] = new Cell("LOKASI");
                worksheet.Cells[0, 2] = new Cell("NAMA");
                worksheet.Cells[0, 3] = new Cell("QTY");
                worksheet.Cells[0, 4] = new Cell("HARGA BELI");
                worksheet.Cells[0, 5] = new Cell("HARGA RATAAN");
                worksheet.Cells[0, 6] = new Cell("HARGA GROSIR");
                worksheet.Cells[0, 7] = new Cell("HARGA ECERAN MINIMUM");
                worksheet.Cells[0, 8] = new Cell("HARGA ECERAN TERTULIS");
                worksheet.Cells[0, 9] = new Cell("KETERANGAN");

                int ctr = 1;
                foreach (Barang b in barangs)
                {
                    worksheet.Cells[ctr, 0] = new Cell(b.JenisBarang.Nama);
                    worksheet.Cells[ctr, 1] = new Cell(b.Lokasi.Nama);
                    worksheet.Cells[ctr, 2] = new Cell(b.Nama);
                    worksheet.Cells[ctr, 3] = new Cell(b.Qty.ToString());
                    worksheet.Cells[ctr, 4] = new Cell(b.HargaBeli.ToString());
                    worksheet.Cells[ctr, 5] = new Cell(b.HargaRataan.ToString());
                    worksheet.Cells[ctr, 6] = new Cell(b.HargaGrosir.ToString());
                    worksheet.Cells[ctr, 7] = new Cell(b.HargaMinimum.ToString());
                    worksheet.Cells[ctr, 8] = new Cell(b.HargaPriceList.ToString());
                    worksheet.Cells[ctr, 9] = new Cell(b.Keterangan);

                    ctr++;
                }

                workbook.Worksheets.Add(worksheet);
                workbook.Save(filename);
                return "OK";
            }
            catch (Exception ex)
            {
                return "ERROR : " + ex.Message;
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Test()
        {
            List<Barang> barangs = db.Barangs.ToList();
            return View();
        }
    }
}