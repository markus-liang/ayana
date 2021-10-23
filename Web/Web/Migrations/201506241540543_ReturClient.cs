namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReturClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReturPembelians", "SupplierID", c => c.Int());
            AddColumn("dbo.ReturPenjualans", "CustomerID", c => c.Int());
            AddForeignKey("dbo.ReturPembelians", "SupplierID", "dbo.Suppliers", "SupplierID");
            AddForeignKey("dbo.ReturPenjualans", "CustomerID", "dbo.Customers", "CustomerID");
            CreateIndex("dbo.ReturPembelians", "SupplierID");
            CreateIndex("dbo.ReturPenjualans", "CustomerID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ReturPenjualans", new[] { "CustomerID" });
            DropIndex("dbo.ReturPembelians", new[] { "SupplierID" });
            DropForeignKey("dbo.ReturPenjualans", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.ReturPembelians", "SupplierID", "dbo.Suppliers");
            DropColumn("dbo.ReturPenjualans", "CustomerID");
            DropColumn("dbo.ReturPembelians", "SupplierID");
        }
    }
}
