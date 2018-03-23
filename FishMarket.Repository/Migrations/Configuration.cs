namespace FishMarket.Repository.Migrations
{
    using FishMarket.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FishMarket.Repository.DataContext.FishDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FishMarket.Repository.DataContext.FishDbContext context)
        {
            
        }
    }
}
