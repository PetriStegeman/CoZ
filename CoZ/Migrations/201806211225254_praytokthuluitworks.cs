namespace CoZ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class praytokthuluitworks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Monsters", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Monsters", "Discriminator");
        }
    }
}
