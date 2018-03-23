using FishMarket.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace FishMarket.Repository.DataContext
{
    public class FishDbInitializer : DropCreateDatabaseIfModelChanges<FishDbContext>
    {
        protected override void Seed(FishDbContext context)
        {
            var users = new List<User>
            {
                new User{UserId=1, UserName="ibrahim", Password="123", Email="ibrahim@test.com"},
                new User{UserId=2, UserName="test", Password="123", Email="test@test.com"}
            };

            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var fishes = new List<Fish>
            {
                new Fish(){FishId=1, UserId=1, Name="Lüfer", Price=15.90},
                new Fish(){FishId=2, UserId=1, Name="Somon", Price=25.90},
                new Fish(){FishId=3, UserId=1, Name="Palamut", Price=15.90},
                new Fish(){FishId=4, UserId=2, Name="Çinekop", Price=15.90},
                new Fish(){FishId=5, UserId=2, Name="Hamsi", Price=15.90}
            };

            fishes.ForEach(f => context.Fishes.Add(f));
            context.SaveChanges();
        }
    }
}
