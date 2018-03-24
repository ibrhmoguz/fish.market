using FishMarket.Model.Entities;
using FishMarket.Repository.DataContext;
using FishMarket.Repository.Interface;
using FishMarket.Repository.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarket.Web.Tests.Repository
{
    public class UserRepositoryMock
    {
        public Mock<FishDbContext> dbContext;
        public Mock<DbSet<User>> mockSet;
        
        public IUser GetUserRepoMockedInstance()
        {
            mockSet = new Mock<DbSet<User>>();
            var users = GetFakeUsers();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            dbContext = new Mock<FishDbContext>();
            dbContext.Setup(m => m.Users).Returns(mockSet.Object);
            var userRepository = new UserRepository(dbContext.Object);

            return userRepository;
        }

        private IQueryable<User> GetFakeUsers()
        {
            return new List<User>
            {
                new User{UserId=1, Email="user1@test.com", Password="123",ActivationStatus=true,ActivationCode="user1123"},
                new User{UserId=2, Email="user2@test.com", Password="123",ActivationStatus=false,ActivationCode="user2123"}
            }.AsQueryable();
        }
    }
}
