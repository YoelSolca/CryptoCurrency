using CryptoCurrencyMVC.Data;
using CryptoCurrencyMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoCurrencyMVC.Controllers
{
    public class ProfileController : Controller
    {
        UserData userData = new UserData();

        // GET: ProfileController
        public ActionResult Profile()
        {
            //Solo devuelve la vista

            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.password = HttpContext.Session.GetString("password");

            var oUser = userData.ObtenerUsuario(ViewBag.email, ViewBag.password);

            return View(oUser);
        }
     

        [HttpPost]
        public IActionResult Profile(PersonModel oUser)
        {
            ViewBag.Id = HttpContext.Session.GetString("Id");


            if (!ModelState.IsValid)
            {
                return View();
            }

            var respusta = userData.EditUser(oUser, Convert.ToInt32(ViewBag.Id));

            if (respusta)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }

    }
}
