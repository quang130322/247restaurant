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
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 32),
                        Password = c.String(nullable: false, maxLength: 32),
                        Phone = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 255),
                        Name = c.String(nullable: false, maxLength: 255),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.Customers", t => t.Id)
                .ForeignKey("common.Shippers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "common.CovidInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vaccination = c.Int(nullable: false),
                        HealthStatus = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        AccountId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("security.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "common.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
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
                "common.ShipperOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        OrderTimeReceived = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                        ShipperId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("common.Shippers", t => t.ShipperId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ShipperId);
            
            CreateTable(
                "common.Shippers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("common.ShipperOrders", "ShipperId", "common.Shippers");
            DropForeignKey("security.Accounts", "Id", "common.Shippers");
            DropForeignKey("common.ShipperOrders", "OrderId", "common.Orders");
            DropForeignKey("common.OrderDetails", "OrderId", "common.Orders");
            DropForeignKey("common.OrderDetails", "FoodId", "common.Foods");
            DropForeignKey("common.Foods", "CategoryId", "common.Categories");
            DropForeignKey("common.Orders", "CustomerId", "common.Customers");
            DropForeignKey("security.Accounts", "Id", "common.Customers");
            DropForeignKey("common.CovidInfo", "AccountId", "security.Accounts");
            DropIndex("common.ShipperOrders", new[] { "ShipperId" });
            DropIndex("common.ShipperOrders", new[] { "OrderId" });
            DropIndex("common.Foods", new[] { "CategoryId" });
            DropIndex("common.OrderDetails", new[] { "OrderId" });
            DropIndex("common.OrderDetails", new[] { "FoodId" });
            DropIndex("common.Orders", new[] { "CustomerId" });
            DropIndex("common.CovidInfo", new[] { "AccountId" });
            DropIndex("security.Accounts", new[] { "Id" });
            DropTable("common.Shippers");
            DropTable("common.ShipperOrders");
            DropTable("common.Categories");
            DropTable("common.Foods");
            DropTable("common.OrderDetails");
            DropTable("common.Orders");
            DropTable("common.Customers");
            DropTable("common.CovidInfo");
            DropTable("security.Accounts");
        }
    }
}
