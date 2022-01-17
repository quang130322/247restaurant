namespace Res247.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "security.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Username = c.String(nullable: false, maxLength: 32),
                        Password = c.String(nullable: false, maxLength: 32),
                        Phone = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 255),
                        Name = c.String(nullable: false, maxLength: 255),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.Customers", t => t.Id)
                .ForeignKey("common.Shippers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "common.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.CovidInfos", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "common.CovidInfos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vaccination = c.Int(nullable: false),
                        HealthStatus = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.Shippers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.CovidInfos", t => t.Id)
                .Index(t => t.Id);
            
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
                        CustomerId = c.Int(nullable: false),
                        ShipperId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("common.Shippers", t => t.ShipperId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ShipperId);
            
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
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.Foods", t => t.FoodId, cascadeDelete: true)
                .ForeignKey("common.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.FoodId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "common.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 500),
                        Image = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 500),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.FoodCategories",
                c => new
                    {
                        FoodId = c.Int(nullable: false),
                        CateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FoodId, t.CateId })
                .ForeignKey("common.Foods", t => t.FoodId, cascadeDelete: true)
                .ForeignKey("common.Categories", t => t.CateId, cascadeDelete: true)
                .Index(t => t.FoodId)
                .Index(t => t.CateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("common.Shippers", "Id", "common.CovidInfos");
            DropForeignKey("common.Orders", "ShipperId", "common.Shippers");
            DropForeignKey("common.OrderDetails", "OrderId", "common.Orders");
            DropForeignKey("common.OrderDetails", "FoodId", "common.Foods");
            DropForeignKey("common.FoodCategories", "CateId", "common.Categories");
            DropForeignKey("common.FoodCategories", "FoodId", "common.Foods");
            DropForeignKey("common.Orders", "CustomerId", "common.Customers");
            DropForeignKey("security.Accounts", "Id", "common.Shippers");
            DropForeignKey("common.Customers", "Id", "common.CovidInfos");
            DropForeignKey("security.Accounts", "Id", "common.Customers");
            DropIndex("common.FoodCategories", new[] { "CateId" });
            DropIndex("common.FoodCategories", new[] { "FoodId" });
            DropIndex("common.OrderDetails", new[] { "OrderId" });
            DropIndex("common.OrderDetails", new[] { "FoodId" });
            DropIndex("common.Orders", new[] { "ShipperId" });
            DropIndex("common.Orders", new[] { "CustomerId" });
            DropIndex("common.Shippers", new[] { "Id" });
            DropIndex("common.Customers", new[] { "Id" });
            DropIndex("security.Accounts", new[] { "Id" });
            DropTable("common.FoodCategories");
            DropTable("common.Categories");
            DropTable("common.Foods");
            DropTable("common.OrderDetails");
            DropTable("common.Orders");
            DropTable("common.Shippers");
            DropTable("common.CovidInfos");
            DropTable("common.Customers");
            DropTable("security.Accounts");
        }
    }
}
