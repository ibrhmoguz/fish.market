using FishMarket.Web.Controllers;
using FishMarket.Web.Tests.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;
using System.Linq;
using FishMarket.Model.ViewModel;
using FishMarket.Model.Entities;
using System.Text;

namespace FishMarket.Web.Tests.Controller
{
    [TestClass]
    public class FishControllerTest
    {
        [TestMethod]
        public void IndexListAllFishes()
        {
            var fishRepository = new FishRepositoryMock().GetUserRepoMockedInstance();
            var fishController = new FishController(fishRepository)
            {
                ControllerContext = FakeControllerContext()
            };

            var result = fishController.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var model = (FishListViewModel)((ViewResult)result).Model;

            Assert.AreEqual(3, model.Fishes.Count());
            Assert.AreEqual("Lüfer", model.Fishes.ElementAt(0).Name);
            Assert.AreEqual("Palamut", model.Fishes.ElementAt(2).Name);
        }

        [TestMethod]
        public void IndexListNothing()
        {
            var fishRepository = new FishRepositoryMock().GetUserRepoMockedInstance();
            var fishController = new FishController(fishRepository)
            {
                ControllerContext = FakeControllerContextNullSession()
            };

            var result = fishController.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var model = (FishListViewModel)((ViewResult)result).Model;

            Assert.AreEqual(null, model.Fishes);
        }

        [TestMethod]
        public void GetImage()
        {
            var fishRepository = new FishRepositoryMock().GetUserRepoMockedInstance();
            var fishController = new FishController(fishRepository);
            var result = fishController.GetImage(1);

            Assert.AreNotEqual(null, (FileContentResult)result);
        }

        [TestMethod]
        public void GetNoImage()
        {
            var fishRepository = new FishRepositoryMock().GetUserRepoMockedInstance();
            var fishController = new FishController(fishRepository);
            var result = fishController.GetImage(1231231);
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void Edit()
        {
            var fishRepository = new FishRepositoryMock().GetUserRepoMockedInstance();
            var fishController = new FishController(fishRepository);
            var result = fishController.Edit(1);
            var model = (Fish)result.Model;

            Assert.AreEqual(1, model.FishId);
            Assert.AreEqual("Lüfer", model.Name);
            Assert.AreEqual(15.90, model.Price);
        }

        [TestMethod]
        public void EditWithNoImage()
        {
            var fishRepository = new FishRepositoryMock().GetUserRepoMockedInstance();
            var fishController = new FishController(fishRepository)
            {
                ControllerContext = FakeControllerContext()
            };

            var fish = new Fish { FishId = 1, UserId = 1, Name = "Lüfer", Price = 15.90, ImageData = Encoding.UTF8.GetBytes("LüferFoto"), ImageMimeType = "image/png" };
            var result = fishController.Edit(fish, null);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RemoveImage()
        {
            var fishRepository = new FishRepositoryMock().GetUserRepoMockedInstance();
            var fishController = new FishController(fishRepository);
            var result = fishController.RemoveImage(1);
            var model = (Fish)((ViewResult)result).Model;

            Assert.AreEqual(null, model.ImageData);
        }

        private static ControllerContext FakeControllerContext()
        {
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.SetupGet(x => x.HttpContext.Session["CurrentUserId"]).Returns(1);
            return mockControllerContext.Object;
        }

        private static ControllerContext FakeControllerContextNullSession()
        {
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.SetupGet(x => x.HttpContext.Session["CurrentUserId"]).Returns(null);
            return mockControllerContext.Object;
        }
    }
}
