namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleMenu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        MenuID = c.Int(nullable: false, identity: true),
                        ParentID = c.Int(),
                        Text = c.String(nullable: false),
                        Controller = c.String(),
                        Action = c.String(),
                        Params = c.String(),
                        Keterangan = c.String(),
                    })
                .PrimaryKey(t => t.MenuID);
            
            CreateTable(
                "dbo.RoleMenus",
                c => new
                    {
                        RoleName = c.String(nullable: false, maxLength: 128),
                        MenuID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleName, t.MenuID })
                .ForeignKey("dbo.Menus", t => t.MenuID, cascadeDelete: true)
                .Index(t => t.MenuID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.RoleMenus", new[] { "MenuID" });
            DropForeignKey("dbo.RoleMenus", "MenuID", "dbo.Menus");
            DropTable("dbo.RoleMenus");
            DropTable("dbo.Menus");
        }
    }
}
