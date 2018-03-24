using FishMarket.Model.ViewModel;
using FishMarket.Web.Controllers;
using FishMarket.Web.Infrastructure.Concrete;
using FishMarket.Web.Tests.Common;
using FishMarket.Web.Tests.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;

namespace FishMarket.Web.Tests.Controller
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void Login()
        {
            var userRepository = new UserRepositoryMock().GetUserRepoMockedInstance();
            var authProvider = new Mock<FormsAuthProvider>();
            var accountController = new AccountController(authProvider.Object, userRepository);

            var result = accountController.Login();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void LoginIncorrectInfo()
        {
            var userRepository = new UserRepositoryMock().GetUserRepoMockedInstance();
            var authProvider = new Mock<FormsAuthProvider>();
            var accountController = new AccountController(authProvider.Object, userRepository);

            var model = new LoginViewModel { Email = "asdfasdf", Password = "werwerwe" };
            var result = accountController.Login(model, "");

            Assert.AreEqual(true, accountController.ModelState.Keys.Contains("IncorrectInfo"));
        }

        [TestMethod]
        public void LoginNotActivatedAccount()
        {
            var userRepository = new UserRepositoryMock().GetUserRepoMockedInstance();
            var authProvider = new Mock<FormsAuthProvider>();
            var accountController = new AccountController(authProvider.Object, userRepository);

            var model = new LoginViewModel { Email = "user2@test.com", Password = "123" };
            var result = accountController.Login(model, "");

            Assert.AreEqual(true, accountController.ModelState.Keys.Contains("NotActivatedAccount"));
        }

        [TestMethod]
        public void LoginSuccess()
        {
            var model = new LoginViewModel { Email = "user1@test.com", Password = "123" };
            var userRepository = new UserRepositoryMock().GetUserRepoMockedInstance();
            var authProvider = new Mock<FormsAuthProvider>();
            authProvider.Setup(m => m.Authenticate(model.Email, model.Password)).Returns(true);
            var accountController = new AccountController(authProvider.Object, userRepository)
            {
                ControllerContext = FakeControllerContext.GetContextWithMockedSession()
            };

            var result = accountController.Login(model, "/Fish/Index");
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
        }

        [TestMethod]
        public void Logout()
        {
            var userRepository = new UserRepositoryMock().GetUserRepoMockedInstance();
            var authProvider = new Mock<FormsAuthProvider>();
            authProvider.Setup(m => m.SignOut());
            var accountController = new AccountController(authProvider.Object, userRepository)
            {
                ControllerContext = FakeControllerContext.GetContextWithMockedSession()
            };

            var result = accountController.LogOut();
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Register()
        {
            var userRepository = new UserRepositoryMock().GetUserRepoMockedInstance();
            var authProvider = new Mock<FormsAuthProvider>();
            var accountController = new AccountController(authProvider.Object, userRepository);

            var result = accountController.Register();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void RegisterAlreadyRegistered()
        {
            var userRepository = new UserRepositoryMock().GetUserRepoMockedInstance();
            var authProvider = new Mock<FormsAuthProvider>();
            var accountController = new AccountController(authProvider.Object, userRepository);
            var model = new LoginViewModel { Email = "user1@test.com", Password = "123" };

            var result = accountController.Register(model);
            Assert.AreEqual(true, accountController.ModelState.Keys.Contains("AlreadyRegistered"));
        }

        [TestMethod]
        public void RegisterSuccess()
        {
            var model = new LoginViewModel { Email = "user3@test.com", Password = "123" };
            var userRepository = new UserRepositoryMock().GetUserRepoMockedInstance();
            var authProvider = new Mock<FormsAuthProvider>();
            var accountController = new Mock<AccountController>(authProvider.Object, userRepository);
            accountController.Setup(m => m.SendActivationMail(model.Email, ""));            

            var result = accountController.Object.Register(model);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Activation()
        {
            var userRepository = new UserRepositoryMock().GetUserRepoMockedInstance();
            var authProvider = new Mock<FormsAuthProvider>();
            var accountController = new AccountController(authProvider.Object, userRepository);

            var result = accountController.Activation("user1@test.com");
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ResendActivation()
        {
            var userRepository = new UserRepositoryMock().GetUserRepoMockedInstance();
            var authProvider = new Mock<FormsAuthProvider>();
            var accountController = new Mock<AccountController>(authProvider.Object, userRepository);
            accountController.Setup(m => m.SendActivationMail("user1@test.com", ""));

            var result = accountController.Object.ResendActivation("user1@test.com");
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void ActivateAlreadyActivated()
        {
            var userRepository = new UserRepositoryMock().GetUserRepoMockedInstance();
            var authProvider = new Mock<FormsAuthProvider>();
            var tempDataMock = new Mock<TempDataDictionary>();
            var accountController = new AccountController(authProvider.Object, userRepository)
            {
                TempData = tempDataMock.Object
            };

            var result = accountController.Activate("user1@test.com", "user2123");
            Assert.AreEqual(null, accountController.TempData["activated"]);
        }

        [TestMethod]
        public void Activate()
        {
            var userRepository = new UserRepositoryMock().GetUserRepoMockedInstance();
            var authProvider = new Mock<FormsAuthProvider>();
            var tempDataMock = new Mock<TempDataDictionary>();
            var accountController = new AccountController(authProvider.Object, userRepository)
            {
                TempData = tempDataMock.Object
            };

            var result = accountController.Activate("user2@test.com", "user2123");
            Assert.AreNotEqual("", accountController.TempData["activated"]);
        }
    }
}
