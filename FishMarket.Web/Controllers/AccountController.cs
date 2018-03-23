using FishMarket.Model.Entities;
using FishMarket.Model.ViewModel;
using FishMarket.Repository.Interface;
using FishMarket.Web.Infrastructure.Abstract;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userFromDb = userRepository.GetAllUsers().FirstOrDefault(x => x.Email.Equals(model.Email));
                if (userFromDb != null)
                {
                    ModelState.AddModelError("", "E-mail is already registered!");
                    return View();
                }
                else
                {
                    var user = new User
                    {
                        Email = model.Email,
                        Password = model.Password,
                        ActivationStatus = false,
                        ActivationCode = CreateActivationCode()
                    };
                    userRepository.SaveUser(user);

                    // Send activation e-mail.
                    // The mail should be sent via message queue with async approach
                    // to handle connection failure or system under heavy load!!!
                    SendActivationMail(user.Email, user.ActivationCode);

                    return RedirectToAction("Activation", "Account", new { model.Email });
                }
            }
            else
            {
                return View();
            }
        }

        private void SendActivationMail(string email, string activationCode)
        {
            var subject = "The activation code from Fish Market";
            var body = string.Format(@"Please click to activate your account! http://localhost:50018/Account/Activate?mail={0}&code={1}", email, activationCode);
            var message = new MailMessage("from@example.com", email, subject, body);
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("a31c66cb87cbe7", "61db73a4ca13e7"),
                EnableSsl = true
            };
            client.SendCompleted += (s, e) =>
            {
                client.Dispose();
                message.Dispose();
            };
            client.Send(message);
        }

        private string CreateActivationCode()
        {
            var activationCode = Guid.NewGuid().ToString();
            var activationCodeFound = userRepository.GetAllUsers().Any(key => key.ActivationCode == activationCode);

            if (activationCodeFound)
            {
                activationCode = CreateActivationCode();
            }

            return activationCode;
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Activation(string email)
        {
            object mail = email;
            return View(mail);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResendActivation(string mail)
        {
            var user = userRepository.GetAllUsers().FirstOrDefault(key => key.Email == mail);
            if (user != null)
            {
                SendActivationMail(user.Email, user.ActivationCode);
            }

            return RedirectToAction("Activation", "Account", new { mail });
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Activate(string mail, string code)
        {
            var user = userRepository.GetAllUsers().FirstOrDefault(key => key.Email.Equals(mail) && key.ActivationCode.Equals(code));
            if (user != null)
            {
                user.ActivationStatus = true;
                userRepository.SaveUser(user);
                TempData["activated"] = string.Format("The {0} account is activated! Please login to Fish Market.", mail);
            }
            return RedirectToAction("Activation", "Account", mail);
        }
    }
}