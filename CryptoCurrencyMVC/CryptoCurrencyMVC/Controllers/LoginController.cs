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
                return RedirectToAction("Index", "Home");
            }
            else
            {

                ViewData["message"] = "Login Details Failed";
            }

            return View();
        }

        


        public ActionResult Welcome(UserModel user)
        {
            return View(user);
        }
    }
}
