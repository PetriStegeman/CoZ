namespace CoZ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "XCoord", c => c.Int(nullable: false));
            AddColumn("dbo.Locations", "YCoord", c => c.Int(nullable: false));
            AddColumn("dbo.Locations", "Map_Id", c => c.Int());
            CreateIndex("dbo.Locations", "Map_Id");
            AddForeignKey("dbo.Locations", "Map_Id", "dbo.Maps", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "Map_Id", "dbo.Maps");
            DropIndex("dbo.Locations", new[] { "Map_Id" });
            DropColumn("dbo.Locations", "Map_Id");
            DropColumn("dbo.Locations", "YCoord");
            DropColumn("dbo.Locations", "XCoord");
        }
    }
}
