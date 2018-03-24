using Moq;
using System.Web.Mvc;

namespace FishMarket.Web.Tests.Common
{
    public static class FakeControllerContext
    {
        public static ControllerContext GetContextWithMockedSession()
        {
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.SetupGet(x => x.HttpContext.Session["CurrentUserId"]).Returns(1);
            return mockControllerContext.Object;
        }

        public static ControllerContext GetContextWithMockedNullSession()
        {
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.SetupGet(x => x.HttpContext.Session["CurrentUserId"]).Returns(null);
            return mockControllerContext.Object;
        }
    }
}
