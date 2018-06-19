namespace CoZ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignmap : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Characters", "CurrentLocation_LocationId", "dbo.Locations");
            DropIndex("dbo.Characters", new[] { "CurrentLocation_LocationId" });
            DropColumn("dbo.Characters", "CurrentLocation_LocationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Characters", "CurrentLocation_LocationId", c => c.Int());
            CreateIndex("dbo.Characters", "CurrentLocation_LocationId");
            AddForeignKey("dbo.Characters", "CurrentLocation_LocationId", "dbo.Locations", "LocationId");
        }
    }
}
