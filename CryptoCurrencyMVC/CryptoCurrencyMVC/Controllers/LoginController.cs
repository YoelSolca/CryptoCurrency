using CryptoCurrencyMVC.Data;
using CryptoCurrencyMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace CryptoCurrencyMVC.Controllers
{
    public class LoginController : Controller
    {
        UserData userData = new UserData();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel oUser)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }

            //var verifyEmail = userData.VerifyEmail(oUser.Email);

            //if(verifyEmail != null){
            //    return RedirectToAction("Login");
            //}

            var respuesta = userData.RegisterUser(oUser);


            if (respuesta)
                return RedirectToAction("Login");
            else
                return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel oUserModel)
        {
            var oUser = userData.Login(oUserModel.Email, oUserModel.Password);

            if(!String.IsNullOrEmpty(oUser.Email) && !String.IsNullOrEmpty(oUser.Password))
            {
                HttpContext.Session.SetString("email", oUserModel.Email);
                HttpContext.Session.SetString("password", oUserModel.Password);
                HttpContext.Session.SetString("name", oUser.FirstName);
                HttpContext.Session.SetString("Id", oUser.ID.ToString());

                var oUserId = userData.GetUser((oUser.ID));

                ViewBag.AccountPeso = oUserId.accountPeso;
                HttpContext.Session.SetString("Amount", oUserId.accountPeso.AccountBalance.ToString());

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.msg = "Email o contraseña ingresada es incorrecta";
                return View();
            }
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("email");
            HttpContext.Session.Remove("password");
            return RedirectToAction("Login");
        }
    }
}
