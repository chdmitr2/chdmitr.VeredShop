namespace VeredShopBL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VeredShop : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        SellerId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Sellers", t => t.SellerId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.SellerId);
            
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        SellerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Salary = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SellerId);
            
            CreateTable(
                "dbo.Sells",
                c => new
                    {
                        SellId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SellId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Barcode = c.Long(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CountOnShelf = c.Int(nullable: false),
                        CountInStorage = c.Int(nullable: false),
                        StorekeeperId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Storekeepers", t => t.StorekeeperId, cascadeDelete: true)
                .Index(t => t.StorekeeperId);
            
            CreateTable(
                "dbo.Storekeepers",
                c => new
                    {
                        StorekeeperId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Salary = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StorekeeperId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "StorekeeperId", "dbo.Storekeepers");
            DropForeignKey("dbo.Sells", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Sells", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "SellerId", "dbo.Sellers");
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropIndex("dbo.Products", new[] { "StorekeeperId" });
            DropIndex("dbo.Sells", new[] { "ProductId" });
            DropIndex("dbo.Sells", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "SellerId" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropTable("dbo.Storekeepers");
            DropTable("dbo.Products");
            DropTable("dbo.Sells");
            DropTable("dbo.Sellers");
            DropTable("dbo.Orders");
            DropTable("dbo.Clients");
        }
    }
}
