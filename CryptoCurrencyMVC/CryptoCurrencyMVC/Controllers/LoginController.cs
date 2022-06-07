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
                return View();
            }

            var respuesta = userData.RegisterUser(oUser);


            if (respuesta)
                return RedirectToAction("Login");
            else
                return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            var oUser = userData.ObtenerUsuario(user.Email, user.Password);


            if(!String.IsNullOrEmpty(oUser.Email) && !String.IsNullOrEmpty(oUser.Password))
            {
                HttpContext.Session.SetString("email", user.Email);
                HttpContext.Session.SetString("password", user.Password);
                HttpContext.Session.SetString("name", oUser.FirstName + " " + oUser.LastName);


                ViewBag.AccountPeso = oUser.accountPeso;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.msg = "Email o contraseña ingresada es incorrecta";
                return View();
            }

            return View();
        }

        


        public ActionResult Welcome()
        {
            ViewBag.email = HttpContext.Session.GetString("email");

            return View();
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
