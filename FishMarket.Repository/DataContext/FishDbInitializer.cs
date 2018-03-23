using FishMarket.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace FishMarket.Repository.DataContext
{
    public class FishDbInitializer : CreateDatabaseIfNotExists<FishDbContext>
    {
        protected override void Seed(FishDbContext context)
        {
            var users = new List<User>
            {
                new User{Email="ibrahim@test.com", Password="123"},
                new User{Email="test@test.com", Password="123"}
            };

            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var fishes = new List<Fish>
            {
                new Fish(){UserId=1, Name="Lüfer", Price=15.90},
                new Fish(){UserId=1, Name="Somon", Price=25.90},
                new Fish(){UserId=1, Name="Palamut", Price=15.90},
                new Fish(){UserId=2, Name="Çinekop", Price=15.90},
                new Fish(){UserId=2, Name="Hamsi", Price=15.90}
            };

            fishes.ForEach(f => context.Fishes.Add(f));
            context.SaveChanges();
        }
    }
}
