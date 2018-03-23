using FishMarket.Model;
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
            using (FishDbContext context = new FishDbContext())
            {
                return context.Fishes.Where(x => x.UserId.Equals(userId)).ToList();
            }
        }
    }
}
