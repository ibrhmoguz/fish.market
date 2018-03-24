using FishMarket.Model.Entities;
using FishMarket.Repository.DataContext;
using FishMarket.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace FishMarket.Repository.Repository
{
    public class UserRepository : IUser
    {
        FishDbContext dbContext;
        public UserRepository(FishDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return dbContext.Users.ToList();
        }

        public void SaveUser(User user)
        {
            if (user.UserId == 0)
            {
                dbContext.Users.Add(user);
            }
            else
            {
                User userFromDb = dbContext.Users.Find(user.UserId);
                if (userFromDb != null)
                {
                    userFromDb.ActivationCode = user.ActivationCode;
                    userFromDb.ActivationStatus = user.ActivationStatus;
                    userFromDb.Email = user.Email;
                    userFromDb.Password = user.Password;
                }
            }
            dbContext.SaveChanges();
        }
    }
}
