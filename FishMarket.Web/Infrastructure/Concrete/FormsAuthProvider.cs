using FishMarket.Web.Infrastructure.Abstract;
using System.Web.Security;

namespace FishMarket.Web.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            //bool result = FormsAuthentication.Authenticate(username, password);
            //if (result)
            //{
            //    FormsAuthentication.SetAuthCookie(username, false);
            //}

            FormsAuthentication.SetAuthCookie(username, false);
            return true;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}