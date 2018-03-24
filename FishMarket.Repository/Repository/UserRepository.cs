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
                if (user.UserId == 0)
                {
                    context.Users.Add(user);
                }
                else
                {
                    User userFromDb = context.Users.Find(user.UserId);
                    if (userFromDb != null)
                    {
                        userFromDb.ActivationCode = user.ActivationCode;
                        userFromDb.ActivationStatus = user.ActivationStatus;
                        userFromDb.Email = user.Email;
                        userFromDb.Password = user.Password;
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
