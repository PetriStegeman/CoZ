namespace CoZ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tesdttestsd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        CharacterId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
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
                        CurrentLocation_LocationId = c.Int(),
                    })
                .PrimaryKey(t => t.CharacterId)
                .ForeignKey("dbo.Locations", t => t.CurrentLocation_LocationId)
                .Index(t => t.CurrentLocation_LocationId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        XCoord = c.Int(nullable: false),
                        YCoord = c.Int(nullable: false),
                        Description = c.String(),
                        ShortDescription = c.String(),
                        IsVisited = c.Boolean(nullable: false),
                        Altitude = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Map_MapId = c.Int(),
                    })
                .PrimaryKey(t => t.LocationId)
                .ForeignKey("dbo.Maps", t => t.Map_MapId)
                .Index(t => t.Map_MapId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Value = c.Int(nullable: false),
                        IsSellable = c.Boolean(nullable: false),
                        Character_CharacterId = c.Int(),
                        Location_LocationId = c.Int(),
                        Monster_MonsterId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Characters", t => t.Character_CharacterId)
                .ForeignKey("dbo.Locations", t => t.Location_LocationId)
                .ForeignKey("dbo.Monsters", t => t.Monster_MonsterId)
                .Index(t => t.Character_CharacterId)
                .Index(t => t.Location_LocationId)
                .Index(t => t.Monster_MonsterId);
            
            CreateTable(
                "dbo.Monsters",
                c => new
                    {
                        MonsterId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Level = c.Int(nullable: false),
                        Hp = c.Int(nullable: false),
                        Strength = c.Int(nullable: false),
                        Gold = c.Int(nullable: false),
                        Location_LocationId = c.Int(),
                    })
                .PrimaryKey(t => t.MonsterId)
                .ForeignKey("dbo.Locations", t => t.Location_LocationId)
                .Index(t => t.Location_LocationId);
            
            CreateTable(
                "dbo.Maps",
                c => new
                    {
                        MapId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MapId)
                .ForeignKey("dbo.Characters", t => t.MapId)
                .Index(t => t.MapId);
            
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
            DropForeignKey("dbo.Locations", "Map_MapId", "dbo.Maps");
            DropForeignKey("dbo.Maps", "MapId", "dbo.Characters");
            DropForeignKey("dbo.Characters", "CurrentLocation_LocationId", "dbo.Locations");
            DropForeignKey("dbo.Items", "Monster_MonsterId", "dbo.Monsters");
            DropForeignKey("dbo.Monsters", "Location_LocationId", "dbo.Locations");
            DropForeignKey("dbo.Items", "Location_LocationId", "dbo.Locations");
            DropForeignKey("dbo.Items", "Character_CharacterId", "dbo.Characters");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Maps", new[] { "MapId" });
            DropIndex("dbo.Monsters", new[] { "Location_LocationId" });
            DropIndex("dbo.Items", new[] { "Monster_MonsterId" });
            DropIndex("dbo.Items", new[] { "Location_LocationId" });
            DropIndex("dbo.Items", new[] { "Character_CharacterId" });
            DropIndex("dbo.Locations", new[] { "Map_MapId" });
            DropIndex("dbo.Characters", new[] { "CurrentLocation_LocationId" });
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
