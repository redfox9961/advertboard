namespace DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeUserIdNullableForAdvert : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Adverts", "UserId", "dbo.Users");
            DropIndex("dbo.Adverts", new[] { "UserId" });
            AlterColumn("dbo.Adverts", "UserId", c => c.Int());
            CreateIndex("dbo.Adverts", "UserId");
            AddForeignKey("dbo.Adverts", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Adverts", "UserId", "dbo.Users");
            DropIndex("dbo.Adverts", new[] { "UserId" });
            AlterColumn("dbo.Adverts", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Adverts", "UserId");
            AddForeignKey("dbo.Adverts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
