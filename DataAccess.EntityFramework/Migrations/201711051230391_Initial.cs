namespace DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adverts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Title = c.String(),
                        Text = c.String(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        PublishDate = c.DateTime(nullable: false),
                        ViewsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        RoleName = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.CityAdverts",
                c => new
                    {
                        City_Id = c.Int(nullable: false),
                        Advert_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.City_Id, t.Advert_Id })
                .ForeignKey("dbo.Cities", t => t.City_Id, cascadeDelete: true)
                .ForeignKey("dbo.Adverts", t => t.Advert_Id, cascadeDelete: true)
                .Index(t => t.City_Id)
                .Index(t => t.Advert_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Adverts", "UserId", "dbo.Users");
            DropForeignKey("dbo.CityAdverts", "Advert_Id", "dbo.Adverts");
            DropForeignKey("dbo.CityAdverts", "City_Id", "dbo.Cities");
            DropIndex("dbo.CityAdverts", new[] { "Advert_Id" });
            DropIndex("dbo.CityAdverts", new[] { "City_Id" });
            DropIndex("dbo.Roles", new[] { "User_Id" });
            DropIndex("dbo.Adverts", new[] { "UserId" });
            DropTable("dbo.CityAdverts");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Cities");
            DropTable("dbo.Adverts");
        }
    }
}
