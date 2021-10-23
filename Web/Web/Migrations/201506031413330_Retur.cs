namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Retur : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReturPembelians",
                c => new
                    {
                        ReturPembelianID = c.Int(nullable: false, identity: true),
                        Tanggal = c.DateTime(nullable: false),
                        Total = c.Int(nullable: false),
                        UserName = c.String(),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.ReturPembelianID);
            
            CreateTable(
                "dbo.ReturPembelianDetails",
                c => new
                    {
                        ReturPembelianID = c.Int(nullable: false),
                        PembelianID = c.Int(nullable: false),
                        BarangID = c.Int(nullable: false),
                        Jumlah = c.Int(nullable: false),
                        Harga = c.Int(nullable: false),
                        isPaid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReturPembelianID, t.PembelianID, t.BarangID })
                .ForeignKey("dbo.ReturPembelians", t => t.ReturPembelianID, cascadeDelete: true)
                .ForeignKey("dbo.Pembelians", t => t.PembelianID, cascadeDelete: true)
                .ForeignKey("dbo.Barangs", t => t.BarangID, cascadeDelete: true)
                .Index(t => t.ReturPembelianID)
                .Index(t => t.PembelianID)
                .Index(t => t.BarangID);
            
            CreateTable(
                "dbo.ReturPenjualans",
                c => new
                    {
                        ReturPenjualanID = c.Int(nullable: false, identity: true),
                        Tanggal = c.DateTime(nullable: false),
                        Total = c.Int(nullable: false),
                        UserName = c.String(),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.ReturPenjualanID);
            
            CreateTable(
                "dbo.ReturPenjualanDetails",
                c => new
                    {
                        ReturPenjualanID = c.Int(nullable: false),
                        PenjualanID = c.Int(nullable: false),
                        BarangID = c.Int(nullable: false),
                        Jumlah = c.Int(nullable: false),
                        Harga = c.Int(nullable: false),
                        isPaid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReturPenjualanID, t.PenjualanID, t.BarangID })
                .ForeignKey("dbo.ReturPenjualans", t => t.ReturPenjualanID, cascadeDelete: true)
                .ForeignKey("dbo.Penjualans", t => t.PenjualanID, cascadeDelete: true)
                .ForeignKey("dbo.Barangs", t => t.BarangID, cascadeDelete: true)
                .Index(t => t.ReturPenjualanID)
                .Index(t => t.PenjualanID)
                .Index(t => t.BarangID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ReturPenjualanDetails", new[] { "BarangID" });
            DropIndex("dbo.ReturPenjualanDetails", new[] { "PenjualanID" });
            DropIndex("dbo.ReturPenjualanDetails", new[] { "ReturPenjualanID" });
            DropIndex("dbo.ReturPembelianDetails", new[] { "BarangID" });
            DropIndex("dbo.ReturPembelianDetails", new[] { "PembelianID" });
            DropIndex("dbo.ReturPembelianDetails", new[] { "ReturPembelianID" });
            DropForeignKey("dbo.ReturPenjualanDetails", "BarangID", "dbo.Barangs");
            DropForeignKey("dbo.ReturPenjualanDetails", "PenjualanID", "dbo.Penjualans");
            DropForeignKey("dbo.ReturPenjualanDetails", "ReturPenjualanID", "dbo.ReturPenjualans");
            DropForeignKey("dbo.ReturPembelianDetails", "BarangID", "dbo.Barangs");
            DropForeignKey("dbo.ReturPembelianDetails", "PembelianID", "dbo.Pembelians");
            DropForeignKey("dbo.ReturPembelianDetails", "ReturPembelianID", "dbo.ReturPembelians");
            DropTable("dbo.ReturPenjualanDetails");
            DropTable("dbo.ReturPenjualans");
            DropTable("dbo.ReturPembelianDetails");
            DropTable("dbo.ReturPembelians");
        }
    }
}
