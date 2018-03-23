using FishMarket.Model.Entities;
using System.Collections.Generic;

namespace FishMarket.Repository.Interface
{
    public interface IUser
    {
        IEnumerable<User> GetAllUsers();
    }
}
