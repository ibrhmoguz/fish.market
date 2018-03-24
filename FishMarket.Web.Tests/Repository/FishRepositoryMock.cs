using FishMarket.Model.Entities;
using FishMarket.Repository.DataContext;
using FishMarket.Repository.Interface;
using FishMarket.Repository.Repository;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace FishMarket.Web.Tests.Repository
{
    public class FishRepositoryMock
    {
        public Mock<FishDbContext> dbContext;
        public Mock<DbSet<Fish>> mockSet;

        public IFish GetUserRepoMockedInstance()
        {
            mockSet = new Mock<DbSet<Fish>>();
            var fishes = GetFakeFishes();
            mockSet.As<IQueryable<Fish>>().Setup(m => m.Provider).Returns(fishes.Provider);
            mockSet.As<IQueryable<Fish>>().Setup(m => m.Expression).Returns(fishes.Expression);
            mockSet.As<IQueryable<Fish>>().Setup(m => m.ElementType).Returns(fishes.ElementType);
            mockSet.As<IQueryable<Fish>>().Setup(m => m.GetEnumerator()).Returns(fishes.GetEnumerator());

            dbContext = new Mock<FishDbContext>();
            dbContext.Setup(m => m.Fishes).Returns(mockSet.Object);
            var fishRepository = new FishRepository(dbContext.Object);

            return fishRepository;
        }

        private IQueryable<Fish> GetFakeFishes()
        {
            return new List<Fish>
            {
                new Fish(){FishId=1, UserId=1, Name="Lüfer", Price=15.90, ImageData=Encoding.UTF8.GetBytes("LüferFoto"), ImageMimeType="image/png"},
                new Fish(){FishId=2,UserId=1, Name="Somon", Price=25.90},
                new Fish(){FishId=3,UserId=1, Name="Palamut", Price=15.90},
                new Fish(){FishId=4,UserId=2, Name="Çinekop", Price=15.90},
                new Fish(){FishId=5,UserId=2, Name="Hamsi", Price=15.90}
            }.AsQueryable();
        }
    }
}
