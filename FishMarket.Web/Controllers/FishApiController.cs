using FishMarket.Model.Entities;
using FishMarket.Model.RestAPI;
using FishMarket.Repository.Interface;
using System.Collections.Generic;
using System.Web.Http;

namespace FishMarket.Web.Controllers
{
    public class FishApiController : ApiController
    {
        IFish fishRepository;

        public FishApiController(IFish fishRepository)
        {
            this.fishRepository = fishRepository;
        }

        public IEnumerable<FishRestful> GetAllFishes()
        {
            IEnumerable<Fish> fishes = fishRepository.GetFishes();
            var fishRestfulList = new List<FishRestful>();
            foreach (var fish in fishes)
            {
                fishRestfulList.Add(new FishRestful
                {
                    FishId = fish.FishId,
                    Name = fish.Name,
                    Price = fish.Price
                });
            }

            return fishRestfulList;
        }
    }
}
