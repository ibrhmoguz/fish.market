using FishMarket.Model.ViewModel;
using FishMarket.Repository.Interface;
using FishMarket.Web.Infrastructure.Abstract;
using System.Linq;
using System.Web.Mvc;

namespace FishMarket.Web.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        IUser userRepository;
        public AccountController(IAuthProvider auth, IUser user)
        {
            this.authProvider = auth;
            this.userRepository = user;
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.GetAllUsers().FirstOrDefault(x => x.Email.Equals(model.Email)
                && x.Password.Equals(model.Password));
                if (user != null)
                {
                    authProvider.Authenticate(model.Email, model.Password);
                    Session["CurrentUserId"] = user.UserId;
                    return Redirect(returnUrl ?? Url.Action("Index", "Fish"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect e-mail or password!");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            Session.Clear();
            authProvider.SignOut();
            return RedirectToAction("Login", "Account", null);
        }
    }
}