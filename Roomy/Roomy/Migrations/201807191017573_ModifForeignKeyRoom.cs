namespace Roomy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifForeignKeyRoom : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rooms", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Rooms", new[] { "CategoryID" });
            AlterColumn("dbo.Rooms", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Rooms", "CategoryID");
            AddForeignKey("dbo.Rooms", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Rooms", new[] { "CategoryID" });
            AlterColumn("dbo.Rooms", "CategoryID", c => c.Int());
            CreateIndex("dbo.Rooms", "CategoryID");
            AddForeignKey("dbo.Rooms", "CategoryID", "dbo.Categories", "ID");
        }
    }
}
