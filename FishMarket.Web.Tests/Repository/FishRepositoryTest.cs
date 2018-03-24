using FishMarket.Model.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace FishMarket.Web.Tests.Repository
{
    [TestClass]
    public class FishRepositoryTest
    {
        [TestMethod]
        public void ReturnAllFishes()
        {
            var fishRepository = new FishRepositoryMock().GetUserRepoMockedInstance();

            var result = fishRepository.GetFishes();
            Assert.AreEqual(5, result.Count());
            Assert.AreEqual("Lüfer", result.ElementAt(0).Name);
            Assert.AreEqual(25.90, result.ElementAt(1).Price);
        }

        
        [TestMethod]
        public void GetFishesByUser()
        {
            var fishRepository = new FishRepositoryMock().GetUserRepoMockedInstance();

            var result = fishRepository.GetFishesByUserId(1);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Lüfer", result.ElementAt(0).Name);
            Assert.AreEqual("Palamut", result.ElementAt(2).Name);
        }

        [TestMethod]
        public void AddFish()
        {
            var mockedUserRepo = new FishRepositoryMock();
            var fishRepository = mockedUserRepo.GetUserRepoMockedInstance();

            var fish = new Fish() { FishId = 0, Name = "Kalkan", Price = 15.90 };
            fishRepository.SaveFish(fish);

            mockedUserRepo.mockSet.Verify(m => m.Add(It.IsAny<Fish>()), Times.Once());
            mockedUserRepo.dbContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateFish()
        {
            var mockedUserRepo = new FishRepositoryMock();
            var fishRepository = mockedUserRepo.GetUserRepoMockedInstance();

            var fish = new Fish() { FishId = 1, Name = "Kalkan", Price = 15.90 };
            fishRepository.SaveFish(fish);

            mockedUserRepo.dbContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
