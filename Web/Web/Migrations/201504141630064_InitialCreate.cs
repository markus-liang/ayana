namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        Nama = c.String(nullable: false),
                        Alamat = c.String(),
                        Telepon = c.String(),
                        ToP = c.Int(nullable: false),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.SupplierID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        Nama = c.String(nullable: false, maxLength: 100),
                        Alamat = c.String(maxLength: 255),
                        Telepon = c.String(maxLength: 50),
                        KategoriCustomerID = c.Int(nullable: false),
                        ToP = c.Int(nullable: false),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.KategoriCustomers", t => t.KategoriCustomerID, cascadeDelete: true)
                .Index(t => t.KategoriCustomerID);
            
            CreateTable(
                "dbo.KategoriCustomers",
                c => new
                    {
                        KategoriCustomerID = c.Int(nullable: false, identity: true),
                        Nama = c.String(nullable: false, maxLength: 100),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.KategoriCustomerID);
            
            CreateTable(
                "dbo.JenisBarangs",
                c => new
                    {
                        JenisBarangID = c.Int(nullable: false, identity: true),
                        Nama = c.String(nullable: false),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.JenisBarangID);
            
            CreateTable(
                "dbo.Barangs",
                c => new
                    {
                        BarangID = c.Int(nullable: false, identity: true),
                        JenisBarangID = c.Int(nullable: false),
                        LokasiID = c.Int(nullable: false),
                        Nama = c.String(nullable: false),
                        Qty = c.Int(nullable: false),
                        HargaBeli = c.Int(nullable: false),
                        HargaGrosir = c.Int(nullable: false),
                        HargaMinimum = c.Int(nullable: false),
                        HargaPriceList = c.Int(nullable: false),
                        HargaRataan = c.Single(nullable: false),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.BarangID)
                .ForeignKey("dbo.JenisBarangs", t => t.JenisBarangID, cascadeDelete: true)
                .ForeignKey("dbo.Lokasis", t => t.LokasiID, cascadeDelete: true)
                .Index(t => t.JenisBarangID)
                .Index(t => t.LokasiID);
            
            CreateTable(
                "dbo.Lokasis",
                c => new
                    {
                        LokasiID = c.Int(nullable: false, identity: true),
                        Nama = c.String(nullable: false),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.LokasiID);
            
            CreateTable(
                "dbo.PembelianDetails",
                c => new
                    {
                        PembelianID = c.Int(nullable: false),
                        BarangID = c.Int(nullable: false),
                        Jumlah = c.Int(nullable: false),
                        Harga = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PembelianID, t.BarangID })
                .ForeignKey("dbo.Pembelians", t => t.PembelianID, cascadeDelete: true)
                .ForeignKey("dbo.Barangs", t => t.BarangID, cascadeDelete: true)
                .Index(t => t.PembelianID)
                .Index(t => t.BarangID);
            
            CreateTable(
                "dbo.Pembelians",
                c => new
                    {
                        PembelianID = c.Int(nullable: false, identity: true),
                        Tanggal = c.DateTime(nullable: false),
                        SupplierID = c.Int(nullable: false),
                        JatuhTempo = c.DateTime(nullable: false),
                        TanggalBayar = c.DateTime(),
                        TipePembayaranID = c.Int(),
                        Total = c.Int(nullable: false),
                        UserName = c.String(),
                        Keterangan = c.String(),
                        StatusTransaksi = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PembelianID)
                .ForeignKey("dbo.Suppliers", t => t.SupplierID, cascadeDelete: true)
                .ForeignKey("dbo.TipePembayarans", t => t.TipePembayaranID)
                .Index(t => t.SupplierID)
                .Index(t => t.TipePembayaranID);
            
            CreateTable(
                "dbo.TipePembayarans",
                c => new
                    {
                        TipePembayaranID = c.Int(nullable: false, identity: true),
                        Nama = c.String(nullable: false),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.TipePembayaranID);
            
            CreateTable(
                "dbo.Penjualans",
                c => new
                    {
                        PenjualanID = c.Int(nullable: false, identity: true),
                        Tanggal = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        JatuhTempo = c.DateTime(nullable: false),
                        TanggalBayar = c.DateTime(),
                        TipePembayaranID = c.Int(),
                        Total = c.Int(nullable: false),
                        UserName = c.String(),
                        Keterangan = c.String(),
                        StatusTransaksi = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PenjualanID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.TipePembayarans", t => t.TipePembayaranID)
                .Index(t => t.CustomerID)
                .Index(t => t.TipePembayaranID);
            
            CreateTable(
                "dbo.PenjualanDetails",
                c => new
                    {
                        PenjualanID = c.Int(nullable: false),
                        BarangID = c.Int(nullable: false),
                        Jumlah = c.Int(nullable: false),
                        Harga = c.Int(nullable: false),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => new { t.PenjualanID, t.BarangID })
                .ForeignKey("dbo.Penjualans", t => t.PenjualanID, cascadeDelete: true)
                .ForeignKey("dbo.Barangs", t => t.BarangID, cascadeDelete: true)
                .Index(t => t.PenjualanID)
                .Index(t => t.BarangID);
            
            CreateTable(
                "dbo.KasKecils",
                c => new
                    {
                        KasKecilID = c.Int(nullable: false, identity: true),
                        JenisKasKecilID = c.Int(nullable: false),
                        Tanggal = c.DateTime(nullable: false),
                        Total = c.Int(nullable: false),
                        Keterangan = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.KasKecilID)
                .ForeignKey("dbo.JenisKasKecils", t => t.JenisKasKecilID, cascadeDelete: true)
                .Index(t => t.JenisKasKecilID);
            
            CreateTable(
                "dbo.JenisKasKecils",
                c => new
                    {
                        JenisKasKecilID = c.Int(nullable: false, identity: true),
                        Nama = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.JenisKasKecilID);
            
            CreateTable(
                "dbo.StokAdjusts",
                c => new
                    {
                        StokAdjustID = c.Int(nullable: false, identity: true),
                        Tanggal = c.DateTime(nullable: false),
                        Username = c.String(),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.StokAdjustID);
            
            CreateTable(
                "dbo.StokAdjustDetails",
                c => new
                    {
                        StokAdjustID = c.Int(nullable: false),
                        BarangID = c.Int(nullable: false),
                        JumlahDalamData = c.Int(nullable: false),
                        JumlahReal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StokAdjustID, t.BarangID })
                .ForeignKey("dbo.StokAdjusts", t => t.StokAdjustID, cascadeDelete: true)
                .ForeignKey("dbo.Barangs", t => t.BarangID, cascadeDelete: true)
                .Index(t => t.StokAdjustID)
                .Index(t => t.BarangID);
            
            CreateTable(
                "dbo.KeyPairs",
                c => new
                    {
                        KeyPairID = c.String(nullable: false, maxLength: 128),
                        Value = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.KeyPairID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.StokAdjustDetails", new[] { "BarangID" });
            DropIndex("dbo.StokAdjustDetails", new[] { "StokAdjustID" });
            DropIndex("dbo.KasKecils", new[] { "JenisKasKecilID" });
            DropIndex("dbo.PenjualanDetails", new[] { "BarangID" });
            DropIndex("dbo.PenjualanDetails", new[] { "PenjualanID" });
            DropIndex("dbo.Penjualans", new[] { "TipePembayaranID" });
            DropIndex("dbo.Penjualans", new[] { "CustomerID" });
            DropIndex("dbo.Pembelians", new[] { "TipePembayaranID" });
            DropIndex("dbo.Pembelians", new[] { "SupplierID" });
            DropIndex("dbo.PembelianDetails", new[] { "BarangID" });
            DropIndex("dbo.PembelianDetails", new[] { "PembelianID" });
            DropIndex("dbo.Barangs", new[] { "LokasiID" });
            DropIndex("dbo.Barangs", new[] { "JenisBarangID" });
            DropIndex("dbo.Customers", new[] { "KategoriCustomerID" });
            DropForeignKey("dbo.StokAdjustDetails", "BarangID", "dbo.Barangs");
            DropForeignKey("dbo.StokAdjustDetails", "StokAdjustID", "dbo.StokAdjusts");
            DropForeignKey("dbo.KasKecils", "JenisKasKecilID", "dbo.JenisKasKecils");
            DropForeignKey("dbo.PenjualanDetails", "BarangID", "dbo.Barangs");
            DropForeignKey("dbo.PenjualanDetails", "PenjualanID", "dbo.Penjualans");
            DropForeignKey("dbo.Penjualans", "TipePembayaranID", "dbo.TipePembayarans");
            DropForeignKey("dbo.Penjualans", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Pembelians", "TipePembayaranID", "dbo.TipePembayarans");
            DropForeignKey("dbo.Pembelians", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.PembelianDetails", "BarangID", "dbo.Barangs");
            DropForeignKey("dbo.PembelianDetails", "PembelianID", "dbo.Pembelians");
            DropForeignKey("dbo.Barangs", "LokasiID", "dbo.Lokasis");
            DropForeignKey("dbo.Barangs", "JenisBarangID", "dbo.JenisBarangs");
            DropForeignKey("dbo.Customers", "KategoriCustomerID", "dbo.KategoriCustomers");
            DropTable("dbo.KeyPairs");
            DropTable("dbo.StokAdjustDetails");
            DropTable("dbo.StokAdjusts");
            DropTable("dbo.JenisKasKecils");
            DropTable("dbo.KasKecils");
            DropTable("dbo.PenjualanDetails");
            DropTable("dbo.Penjualans");
            DropTable("dbo.TipePembayarans");
            DropTable("dbo.Pembelians");
            DropTable("dbo.PembelianDetails");
            DropTable("dbo.Lokasis");
            DropTable("dbo.Barangs");
            DropTable("dbo.JenisBarangs");
            DropTable("dbo.KategoriCustomers");
            DropTable("dbo.Customers");
            DropTable("dbo.Suppliers");
        }
    }
}
