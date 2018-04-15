namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialisedatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "QUANLY-BANBANH.Bakery",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ngaypost = c.DateTime(nullable: false),
                        VAT = c.Double(nullable: false),
                        count = c.Decimal(nullable: false, precision: 10, scale: 0),
                        category_Id = c.String(maxLength: 128),
                        images_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("QUANLY-BANBANH.Category", t => t.category_Id)
                .ForeignKey("QUANLY-BANBANH.Image", t => t.images_Id)
                .Index(t => t.category_Id)
                .Index(t => t.images_Id);
            
            CreateTable(
                "QUANLY-BANBANH.Category",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "QUANLY-BANBANH.Image",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        nameImage = c.String(),
                        url = c.String(),
                        width = c.Decimal(nullable: false, precision: 10, scale: 0),
                        height = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "QUANLY-BANBANH.Bill",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nameuser = c.String(),
                        Addressuser = c.String(),
                        SDT = c.String(),
                        Email = c.String(),
                        confirmEmail = c.String(),
                        Totalprice = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "QUANLY-BANBANH.Billdetails",
                c => new
                    {
                        iddetails = c.String(nullable: false, maxLength: 128),
                        quality = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Bakery_ID = c.String(maxLength: 128),
                        Bill_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.iddetails)
                .ForeignKey("QUANLY-BANBANH.Bakery", t => t.Bakery_ID)
                .ForeignKey("QUANLY-BANBANH.Bill", t => t.Bill_Id)
                .Index(t => t.Bakery_ID)
                .Index(t => t.Bill_Id);
            
            CreateTable(
                "QUANLY-BANBANH.Introduction",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        details = c.String(),
                        title = c.String(),
                        type = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Author_Id = c.String(maxLength: 128),
                        image_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("QUANLY-BANBANH.AspNetUsers", t => t.Author_Id)
                .ForeignKey("QUANLY-BANBANH.Image", t => t.image_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.image_Id);
            
            CreateTable(
                "QUANLY-BANBANH.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Decimal(nullable: false, precision: 1, scale: 0),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Decimal(nullable: false, precision: 1, scale: 0),
                        TwoFactorEnabled = c.Decimal(nullable: false, precision: 1, scale: 0),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Decimal(nullable: false, precision: 1, scale: 0),
                        AccessFailedCount = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserName = c.String(nullable: false, maxLength: 256),
                        representImage_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("QUANLY-BANBANH.Image", t => t.representImage_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.representImage_Id);
            
            CreateTable(
                "QUANLY-BANBANH.AspNetUserClaims",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("QUANLY-BANBANH.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "QUANLY-BANBANH.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("QUANLY-BANBANH.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "QUANLY-BANBANH.Log",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        logstring = c.String(),
                        datetime = c.DateTime(nullable: false),
                        bakeryuser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("QUANLY-BANBANH.AspNetUsers", t => t.bakeryuser_Id)
                .Index(t => t.bakeryuser_Id);
            
            CreateTable(
                "QUANLY-BANBANH.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("QUANLY-BANBANH.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("QUANLY-BANBANH.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "QUANLY-BANBANH.News",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Body = c.String(),
                        DatePost = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "QUANLY-BANBANH.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "QUANLY-BANBANH.Sale",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        time1 = c.DateTime(nullable: false),
                        time2 = c.DateTime(nullable: false),
                        sale = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "QUANLY-BANBANH.Shop",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        address = c.String(),
                        gmail = c.String(),
                        Googlemapembded = c.String(),
                        SDT = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "QUANLY-BANBANH.Slider",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        position = c.Decimal(nullable: false, precision: 10, scale: 0),
                        image_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("QUANLY-BANBANH.Image", t => t.image_Id)
                .Index(t => t.image_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("QUANLY-BANBANH.Slider", "image_Id", "QUANLY-BANBANH.Image");
            DropForeignKey("QUANLY-BANBANH.AspNetUserRoles", "RoleId", "QUANLY-BANBANH.AspNetRoles");
            DropForeignKey("QUANLY-BANBANH.Introduction", "image_Id", "QUANLY-BANBANH.Image");
            DropForeignKey("QUANLY-BANBANH.Introduction", "Author_Id", "QUANLY-BANBANH.AspNetUsers");
            DropForeignKey("QUANLY-BANBANH.AspNetUserRoles", "UserId", "QUANLY-BANBANH.AspNetUsers");
            DropForeignKey("QUANLY-BANBANH.AspNetUsers", "representImage_Id", "QUANLY-BANBANH.Image");
            DropForeignKey("QUANLY-BANBANH.Log", "bakeryuser_Id", "QUANLY-BANBANH.AspNetUsers");
            DropForeignKey("QUANLY-BANBANH.AspNetUserLogins", "UserId", "QUANLY-BANBANH.AspNetUsers");
            DropForeignKey("QUANLY-BANBANH.AspNetUserClaims", "UserId", "QUANLY-BANBANH.AspNetUsers");
            DropForeignKey("QUANLY-BANBANH.Billdetails", "Bill_Id", "QUANLY-BANBANH.Bill");
            DropForeignKey("QUANLY-BANBANH.Billdetails", "Bakery_ID", "QUANLY-BANBANH.Bakery");
            DropForeignKey("QUANLY-BANBANH.Bakery", "images_Id", "QUANLY-BANBANH.Image");
            DropForeignKey("QUANLY-BANBANH.Bakery", "category_Id", "QUANLY-BANBANH.Category");
            DropIndex("QUANLY-BANBANH.Slider", new[] { "image_Id" });
            DropIndex("QUANLY-BANBANH.AspNetRoles", "RoleNameIndex");
            DropIndex("QUANLY-BANBANH.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("QUANLY-BANBANH.AspNetUserRoles", new[] { "UserId" });
            DropIndex("QUANLY-BANBANH.Log", new[] { "bakeryuser_Id" });
            DropIndex("QUANLY-BANBANH.AspNetUserLogins", new[] { "UserId" });
            DropIndex("QUANLY-BANBANH.AspNetUserClaims", new[] { "UserId" });
            DropIndex("QUANLY-BANBANH.AspNetUsers", new[] { "representImage_Id" });
            DropIndex("QUANLY-BANBANH.AspNetUsers", "UserNameIndex");
            DropIndex("QUANLY-BANBANH.Introduction", new[] { "image_Id" });
            DropIndex("QUANLY-BANBANH.Introduction", new[] { "Author_Id" });
            DropIndex("QUANLY-BANBANH.Billdetails", new[] { "Bill_Id" });
            DropIndex("QUANLY-BANBANH.Billdetails", new[] { "Bakery_ID" });
            DropIndex("QUANLY-BANBANH.Bakery", new[] { "images_Id" });
            DropIndex("QUANLY-BANBANH.Bakery", new[] { "category_Id" });
            DropTable("QUANLY-BANBANH.Slider");
            DropTable("QUANLY-BANBANH.Shop");
            DropTable("QUANLY-BANBANH.Sale");
            DropTable("QUANLY-BANBANH.AspNetRoles");
            DropTable("QUANLY-BANBANH.News");
            DropTable("QUANLY-BANBANH.AspNetUserRoles");
            DropTable("QUANLY-BANBANH.Log");
            DropTable("QUANLY-BANBANH.AspNetUserLogins");
            DropTable("QUANLY-BANBANH.AspNetUserClaims");
            DropTable("QUANLY-BANBANH.AspNetUsers");
            DropTable("QUANLY-BANBANH.Introduction");
            DropTable("QUANLY-BANBANH.Billdetails");
            DropTable("QUANLY-BANBANH.Bill");
            DropTable("QUANLY-BANBANH.Image");
            DropTable("QUANLY-BANBANH.Category");
            DropTable("QUANLY-BANBANH.Bakery");
        }
    }
}
