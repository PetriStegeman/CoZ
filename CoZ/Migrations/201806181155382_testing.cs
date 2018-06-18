namespace CoZ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "userId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "userId");
        }
    }
}
