namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusBarang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Barangs", "IsNonAktif", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Barangs", "IsNonAktif");
        }
    }
}
