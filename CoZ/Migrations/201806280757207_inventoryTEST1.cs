namespace CoZ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inventoryTEST1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "ItemType", c => c.Int(nullable: false));
            DropColumn("dbo.Items", "CanBeArmor");
            DropColumn("dbo.Items", "CanBeConsumed");
            DropColumn("dbo.Items", "CanBeWeapon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "CanBeWeapon", c => c.Boolean());
            AddColumn("dbo.Items", "CanBeConsumed", c => c.Boolean());
            AddColumn("dbo.Items", "CanBeArmor", c => c.Boolean());
            DropColumn("dbo.Items", "ItemType");
        }
    }
}
