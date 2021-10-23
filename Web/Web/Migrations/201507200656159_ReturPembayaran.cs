namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReturPembayaran : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReturPembelians", "TanggalBayar", c => c.DateTime());
            AddColumn("dbo.ReturPembelians", "TipePembayaranID", c => c.Int());
            AddColumn("dbo.ReturPembelians", "StatusTransaksi", c => c.Int(nullable: false));
            AddColumn("dbo.ReturPenjualans", "TanggalBayar", c => c.DateTime());
            AddColumn("dbo.ReturPenjualans", "TipePembayaranID", c => c.Int());
            AddColumn("dbo.ReturPenjualans", "StatusTransaksi", c => c.Int(nullable: false));
            AddForeignKey("dbo.ReturPembelians", "TipePembayaranID", "dbo.TipePembayarans", "TipePembayaranID");
            AddForeignKey("dbo.ReturPenjualans", "TipePembayaranID", "dbo.TipePembayarans", "TipePembayaranID");
            CreateIndex("dbo.ReturPembelians", "TipePembayaranID");
            CreateIndex("dbo.ReturPenjualans", "TipePembayaranID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ReturPenjualans", new[] { "TipePembayaranID" });
            DropIndex("dbo.ReturPembelians", new[] { "TipePembayaranID" });
            DropForeignKey("dbo.ReturPenjualans", "TipePembayaranID", "dbo.TipePembayarans");
            DropForeignKey("dbo.ReturPembelians", "TipePembayaranID", "dbo.TipePembayarans");
            DropColumn("dbo.ReturPenjualans", "StatusTransaksi");
            DropColumn("dbo.ReturPenjualans", "TipePembayaranID");
            DropColumn("dbo.ReturPenjualans", "TanggalBayar");
            DropColumn("dbo.ReturPembelians", "StatusTransaksi");
            DropColumn("dbo.ReturPembelians", "TipePembayaranID");
            DropColumn("dbo.ReturPembelians", "TanggalBayar");
        }
    }
}
