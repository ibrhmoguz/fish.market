using FishMarket.Model.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FishMarket.Repository.DataContext
{
    public class FishDbContext : DbContext
    {
        public FishDbContext()
            : base("fishMarketDbConnectionString")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Fish> Fishes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
