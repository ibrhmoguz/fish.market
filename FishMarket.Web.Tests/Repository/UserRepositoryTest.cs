using FishMarket.Model.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace FishMarket.Web.Tests.Repository
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void ReturnAllUsers()
        {
            var userRepository = new UserRepositoryMock().GetUserRepoMockedInstance();

            var result = userRepository.GetAllUsers();
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("user1@test.com", result.ElementAt(0).Email);
            Assert.AreEqual("user2@test.com", result.ElementAt(1).Email);
        }

        [TestMethod]
        public void AddUser()
        {
            var mockedUserRepo = new UserRepositoryMock();
            var userRepository = mockedUserRepo.GetUserRepoMockedInstance();

            var user = new User { UserId = 0, Email = "user2@test.com", Password = "123", ActivationStatus = false, ActivationCode = "user2123" };
            userRepository.SaveUser(user);

            mockedUserRepo.mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
            mockedUserRepo.dbContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateUser()
        {
            var mockedUserRepo = new UserRepositoryMock();
            var userRepository = mockedUserRepo.GetUserRepoMockedInstance();

            var user = new User { UserId = 1, Email = "user2@test.com", Password = "123", ActivationStatus = false, ActivationCode = "user2123" };
            userRepository.SaveUser(user);

            mockedUserRepo.dbContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
