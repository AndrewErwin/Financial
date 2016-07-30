using System;
using System.Web.Mvc;
using System.Web.Security;
using Financial.DAO;
using Financial.Models;
using WebMatrix.WebData;

namespace Financial.Controllers
{
    public class UserController : Controller
    {
        private UserDAO userDAO;

        public UserController(UserDAO userDao)
        {
            this.userDAO = userDao;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("New");
        }

        [HttpGet]
        [Route("SignUp", Name = "SignUp")]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SignUp", Name = "SignUpPost")]
        public ActionResult SignUp(UserSignUpModel newUser)
        {
            if (ModelState.IsValid)
            {               
                try
                {
                    WebSecurity.CreateUserAndAccount(newUser.Login, newUser.Password, new { Name = newUser.Name });
                    WebSecurity.Login(newUser.Login, newUser.Password);
                    return RedirectToRoute("Start");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("create_user_error", e.Message);
                }
            }
            return View(newUser);
        }

        [HttpGet]
        [Route("SignIn", Name = "SignIn")]
        public ActionResult SignIn(String returnUrl)
        {
            return View(new UserLoginModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SignIn", Name = "SignInPost")]
        public ActionResult SignIn(UserLoginModel user)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(user.Login, user.Password))
                {
                    if (!string.IsNullOrEmpty(user.ReturnUrl) && Url.IsLocalUrl(user.ReturnUrl))
                    {
                        return Redirect(user.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToRoute("Start");
                    }
                }
                else
                {
                    ModelState.AddModelError("invalid_login", "Usuário e/ou Senha incorretos.");
                }
            }
            return View(user);
        }

        [HttpGet]
        [Route("SignOut", Name = "SignOut")]
        public ActionResult SignOut()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}