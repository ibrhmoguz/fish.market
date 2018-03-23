using FishMarket.Model.Entities;
using System.Collections.Generic;

namespace FishMarket.Repository.Interface
{
    public interface IFish
    {
        IEnumerable<Fish> GetFishesByUserId(int userId);
    }
}
