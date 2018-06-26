namespace CoZ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zwollllle : DbMigration
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
                        Speed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CharacterId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Value = c.Int(nullable: false),
                        IsSellable = c.Boolean(nullable: false),
                        IsEquiped = c.Boolean(nullable: false),
                        Hp = c.Int(),
                        CanBeArmor = c.Boolean(),
                        PortionsRemaining = c.Int(),
                        CanBeConsumed = c.Boolean(),
                        Strength = c.Int(),
                        CanBeWeapon = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Character_CharacterId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Characters", t => t.Character_CharacterId)
                .Index(t => t.Character_CharacterId);
            
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
                        Item_ItemId = c.Int(),
                        Character_CharacterId = c.Int(),
                    })
                .PrimaryKey(t => t.LocationId)
                .ForeignKey("dbo.Items", t => t.Item_ItemId)
                .ForeignKey("dbo.Characters", t => t.Character_CharacterId)
                .Index(t => t.Item_ItemId)
                .Index(t => t.Character_CharacterId);
            
            CreateTable(
                "dbo.Monsters",
                c => new
                    {
                        MonsterId = c.Int(nullable: false),
                        Name = c.String(),
                        Level = c.Int(nullable: false),
                        CurrentHp = c.Int(nullable: false),
                        MaxHp = c.Int(nullable: false),
                        Strength = c.Int(nullable: false),
                        Gold = c.Int(nullable: false),
                        Speed = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Loot_ItemId = c.Int(),
                    })
                .PrimaryKey(t => t.MonsterId)
                .ForeignKey("dbo.Locations", t => t.MonsterId)
                .ForeignKey("dbo.Items", t => t.Loot_ItemId)
                .Index(t => t.MonsterId)
                .Index(t => t.Loot_ItemId);
            
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
            DropForeignKey("dbo.Locations", "Character_CharacterId", "dbo.Characters");
            DropForeignKey("dbo.Monsters", "Loot_ItemId", "dbo.Items");
            DropForeignKey("dbo.Monsters", "MonsterId", "dbo.Locations");
            DropForeignKey("dbo.Locations", "Item_ItemId", "dbo.Items");
            DropForeignKey("dbo.Items", "Character_CharacterId", "dbo.Characters");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Monsters", new[] { "Loot_ItemId" });
            DropIndex("dbo.Monsters", new[] { "MonsterId" });
            DropIndex("dbo.Locations", new[] { "Character_CharacterId" });
            DropIndex("dbo.Locations", new[] { "Item_ItemId" });
            DropIndex("dbo.Items", new[] { "Character_CharacterId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Monsters");
            DropTable("dbo.Locations");
            DropTable("dbo.Items");
            DropTable("dbo.Characters");
        }
    }
}
