namespace Res247.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "common.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 500),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 500),
                        Image = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "common.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FoodId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.Foods", t => t.FoodId, cascadeDelete: true)
                .ForeignKey("common.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.FoodId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "common.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comment = c.String(maxLength: 500),
                        Status = c.Int(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        CancelReason = c.String(),
                        OrderAddress = c.String(),
                        OrderArrivedAt = c.DateTime(),
                        AccountId = c.String(maxLength: 128),
                        ShipperId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .ForeignKey("common.Shippers", t => t.ShipperId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.ShipperId);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Address = c.String(nullable: false, maxLength: 255),
                        Name = c.String(nullable: false, maxLength: 255),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        Account_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "common.CovidInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vaccination = c.Int(nullable: false),
                        HealthStatus = c.Boolean(nullable: false),
                        TravelToOtherPlace = c.Boolean(nullable: false),
                        HaveSymptoms = c.Boolean(nullable: false),
                        MeetCovidPatients = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        AccountId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        Account_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Account_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "common.Shippers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CovidShippers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vaccination = c.Int(nullable: false),
                        HealthStatus = c.Boolean(nullable: false),
                        TravelToOtherPlace = c.Boolean(nullable: false),
                        HaveSymptoms = c.Boolean(nullable: false),
                        MeetCovidPatients = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        ShipperId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.Shippers", t => t.ShipperId, cascadeDelete: true)
                .Index(t => t.ShipperId);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("common.OrderDetails", "OrderId", "common.Orders");
            DropForeignKey("common.Orders", "ShipperId", "common.Shippers");
            DropForeignKey("dbo.CovidShippers", "ShipperId", "common.Shippers");
            DropForeignKey("common.Orders", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.IdentityUserRoles", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.IdentityUserLogins", "Account_Id", "dbo.Accounts");
            DropForeignKey("common.CovidInfo", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.IdentityUserClaims", "Account_Id", "dbo.Accounts");
            DropForeignKey("common.OrderDetails", "FoodId", "common.Foods");
            DropForeignKey("common.Foods", "CategoryId", "common.Categories");
            DropIndex("dbo.CovidShippers", new[] { "ShipperId" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "Account_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "Account_Id" });
            DropIndex("common.CovidInfo", new[] { "AccountId" });
            DropIndex("dbo.IdentityUserClaims", new[] { "Account_Id" });
            DropIndex("common.Orders", new[] { "ShipperId" });
            DropIndex("common.Orders", new[] { "AccountId" });
            DropIndex("common.OrderDetails", new[] { "OrderId" });
            DropIndex("common.OrderDetails", new[] { "FoodId" });
            DropIndex("common.Foods", new[] { "CategoryId" });
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.CovidShippers");
            DropTable("common.Shippers");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("common.CovidInfo");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.Accounts");
            DropTable("common.Orders");
            DropTable("common.OrderDetails");
            DropTable("common.Foods");
            DropTable("common.Categories");
        }
    }
}
