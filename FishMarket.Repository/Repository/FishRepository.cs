using FishMarket.Model.Entities;
using FishMarket.Repository.DataContext;
using FishMarket.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace FishMarket.Repository.Repository
{
    public class FishRepository : IFish
    {
        FishDbContext dbContext;
        public FishRepository(FishDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Fish> GetFishesByUserId(int userId)
        {
            return dbContext.Fishes.Where(x => x.UserId.Equals(userId)).ToList();
        }

        public Fish GetFishById(int fishId)
        {
            return dbContext.Fishes.FirstOrDefault(f => f.FishId.Equals(fishId));
        }

        public void SaveFish(Fish fish)
        {
            if (fish.FishId == 0)
            {
                dbContext.Fishes.Add(fish);
            }
            else
            {
                Fish fishFromDb = dbContext.Fishes.Find(fish.FishId);
                if (fishFromDb != null)
                {
                    fishFromDb.Name = fish.Name;
                    fishFromDb.Price = fish.Price;
                    fishFromDb.ImageData = fish.ImageData;
                    fishFromDb.ImageMimeType = fish.ImageMimeType;
                }
            }
            dbContext.SaveChanges();
        }

        public IEnumerable<Fish> GetFishes()
        {
            return dbContext.Fishes.ToList();
        }
    }
}
