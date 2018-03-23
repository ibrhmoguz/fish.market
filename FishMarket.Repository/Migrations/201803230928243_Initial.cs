namespace FishMarket.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fish",
                c => new
                    {
                        FishId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        ImageData = c.String(),
                    })
                .PrimaryKey(t => t.FishId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fish", "UserId", "dbo.User");
            DropIndex("dbo.Fish", new[] { "UserId" });
            DropTable("dbo.User");
            DropTable("dbo.Fish");
        }
    }
}
