namespace CoZ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Gold = c.Int(nullable: false),
                        XCoord = c.Int(nullable: false),
                        YCoord = c.Int(nullable: false),
                        Experience = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        MaxHp = c.Int(nullable: false),
                        CurrentHp = c.Int(nullable: false),
                        MaxMp = c.Int(nullable: false),
                        CurrentMp = c.Int(nullable: false),
                        Strength = c.Int(nullable: false),
                        Magic = c.Int(nullable: false),
                        Insanity = c.Int(nullable: false),
                        CurrentLocation_Id = c.Int(),
                        Map_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.CurrentLocation_Id)
                .ForeignKey("dbo.Maps", t => t.Map_Id)
                .Index(t => t.CurrentLocation_Id)
                .Index(t => t.Map_Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ShortDescription = c.String(),
                        IsVisited = c.Boolean(nullable: false),
                        Altitude = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Value = c.Int(nullable: false),
                        IsSellable = c.Boolean(nullable: false),
                        Location_Id = c.Int(),
                        Monster_Id = c.Int(),
                        Character_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .ForeignKey("dbo.Monsters", t => t.Monster_Id)
                .ForeignKey("dbo.Characters", t => t.Character_Id)
                .Index(t => t.Location_Id)
                .Index(t => t.Monster_Id)
                .Index(t => t.Character_Id);
            
            CreateTable(
                "dbo.Monsters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Level = c.Int(nullable: false),
                        Hp = c.Int(nullable: false),
                        Strength = c.Int(nullable: false),
                        Gold = c.Int(nullable: false),
                        Location_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .Index(t => t.Location_Id);
            
            CreateTable(
                "dbo.Maps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Characters", "Map_Id", "dbo.Maps");
            DropForeignKey("dbo.Items", "Character_Id", "dbo.Characters");
            DropForeignKey("dbo.Characters", "CurrentLocation_Id", "dbo.Locations");
            DropForeignKey("dbo.Monsters", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.Items", "Monster_Id", "dbo.Monsters");
            DropForeignKey("dbo.Items", "Location_Id", "dbo.Locations");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Monsters", new[] { "Location_Id" });
            DropIndex("dbo.Items", new[] { "Character_Id" });
            DropIndex("dbo.Items", new[] { "Monster_Id" });
            DropIndex("dbo.Items", new[] { "Location_Id" });
            DropIndex("dbo.Characters", new[] { "Map_Id" });
            DropIndex("dbo.Characters", new[] { "CurrentLocation_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Maps");
            DropTable("dbo.Monsters");
            DropTable("dbo.Items");
            DropTable("dbo.Locations");
            DropTable("dbo.Characters");
        }
    }
}
