namespace FishMarket.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Activation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "ActivationCode", c => c.String());
            AddColumn("dbo.User", "ActivationStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "ActivationStatus");
            DropColumn("dbo.User", "ActivationCode");
        }
    }
}
