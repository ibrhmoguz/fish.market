namespace FishMarket.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fish", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Fish", "ImageMimeType", c => c.String(maxLength: 10));
            AlterColumn("dbo.User", "UserName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.User", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.User", "ActivationCode", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "ActivationCode", c => c.String());
            AlterColumn("dbo.User", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.User", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.Fish", "ImageMimeType", c => c.String());
            AlterColumn("dbo.Fish", "Name", c => c.String(nullable: false));
        }
    }
}
