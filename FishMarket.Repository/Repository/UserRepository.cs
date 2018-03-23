using FishMarket.Model.Entities;
using FishMarket.Repository.DataContext;
using FishMarket.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace FishMarket.Repository.Repository
{
    public class UserRepository : IUser
    {
        public IEnumerable<User> GetAllUsers()
        {
            using (FishDbContext context = new FishDbContext())
            {
                return context.Users.ToList();
            }
        }

        public void SaveUser(User user)
        {
            using (var context = new FishDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
