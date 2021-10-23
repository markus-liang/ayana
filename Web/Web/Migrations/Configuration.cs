namespace Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Web.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Web.Models.WebContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Web.Models.WebContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.TipePembayarans.AddOrUpdate(
                p => p.TipePembayaranID,
                new TipePembayaran() { TipePembayaranID = 1, Nama = "CASH" },
                new TipePembayaran() { TipePembayaranID = 2, Nama = "DEBIT CARD" },
                new TipePembayaran() { TipePembayaranID = 3, Nama = "CREDIT CARD" }
            );

            context.KategoriCustomers.AddOrUpdate(
                p => p.KategoriCustomerID,
                new KategoriCustomer() { KategoriCustomerID = 1, Nama = "GROSIR" },
                new KategoriCustomer() { KategoriCustomerID = 2, Nama = "ECERAN" }
            );

            context.JenisKasKecils.AddOrUpdate(
                p => p.JenisKasKecilID,
                new JenisKasKecil() { JenisKasKecilID = 1, Nama = "DEBET" },
                new JenisKasKecil() { JenisKasKecilID = 2, Nama = "KREDIT" }
            );

            context.KeyPairs.AddOrUpdate(
                p => p.KeyPairID,
                new KeyPair() { KeyPairID = "valid_until", Value = "MzAgQXByIDIwMTU=" },
                new KeyPair() { KeyPairID = "last_used", Value = "MTEgQXByIDIwMTU=" }
            );

            /*
            // TEMPORARY
            context.Suppliers.AddOrUpdate(
                p => p.SupplierID,
                new Supplier() { SupplierID = 1, Nama = "Supplier Jok", ToP = 0 },
                new Supplier() { SupplierID = 2, Nama = "Supplier Lampu", ToP = 1 }
            );

            context.Customers.AddOrUpdate(
                p => p.CustomerID,
                new Customer() { CustomerID = 1, Nama = "Umum Grosir", Alamat = "Alamat Umum Grosir", KategoriCustomerID = 1, ToP = 3 },
                new Customer() { CustomerID = 2, Nama = "Umum Eceran", Alamat = "Alamat Umum Eceran", KategoriCustomerID = 2, ToP = 0 }
            );

            context.JenisBarangs.AddOrUpdate(
                p => p.JenisBarangID,
                new JenisBarang() { JenisBarangID = 1, Nama = "Helmet", Keterangan = "All about helmets" },
                new JenisBarang() { JenisBarangID = 2, Nama = "Sound System", Keterangan = "All about sound systems" }
            );

            context.Lokasis.AddOrUpdate(
                p => p.LokasiID,
                new Lokasi() { LokasiID = 1, Nama = "Lokasi 1" },
                new Lokasi() { LokasiID = 2, Nama = "Lokasi 2" }
            );

            context.Barangs.AddOrUpdate(
                p => p.BarangID,
                new Barang() { BarangID = 1, JenisBarangID = 1, LokasiID = 1, Nama = "Helm Kyt", Qty = 0, HargaBeli = 250000, HargaRataan = 0, HargaGrosir = 275000, HargaMinimum = 300000, HargaPriceList = 325000 },
                new Barang() { BarangID = 2, JenisBarangID = 1, LokasiID = 1, Nama = "Helm Abal", Qty = 0, HargaBeli = 50000, HargaRataan = 0, HargaGrosir = 80000, HargaMinimum = 100000, HargaPriceList = 125000 }
            );
        */
        }
    }
}
