using FishMarket.Model.Entities;
using FishMarket.Repository.DataContext;
using FishMarket.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace FishMarket.Repository.Repository
{
    public class FishRepository : IFish
    {
        public IEnumerable<Fish> GetFishesByUserId(int userId)
        {
            using (var context = new FishDbContext())
            {
                return context.Fishes.Where(x => x.UserId.Equals(userId)).ToList();
            }
        }

        public Fish GetFishById(int fishId)
        {
            using (var context = new FishDbContext())
            {
                return context.Fishes.FirstOrDefault(f => f.FishId.Equals(fishId));
            }
        }

        public void SaveFish(Fish fish)
        {
            using (var context = new FishDbContext())
            {
                if (fish.FishId == 0)
                {
                    context.Fishes.Add(fish);
                }
                else
                {
                    Fish fishFromDb = context.Fishes.Find(fish.FishId);
                    if (fishFromDb != null)
                    {
                        fishFromDb.Name = fish.Name;
                        fishFromDb.Price = fish.Price;
                        fishFromDb.ImageData = fish.ImageData;
                        fishFromDb.ImageMimeType = fish.ImageMimeType;
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
