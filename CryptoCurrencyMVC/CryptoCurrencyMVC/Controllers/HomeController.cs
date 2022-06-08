using CryptoCurrencyMVC.Data;
using CryptoCurrencyMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace CryptoCurrencyMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        UserData userData = new UserData();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.password = HttpContext.Session.GetString("password");

            if(ViewBag.email == null && ViewBag.password == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var oUser = userData.ObtenerUsuario(ViewBag.email, ViewBag.password);

            var oList = userData.Movements(oUser.ID);

            ViewBag.Operations = oList;

            return View(oUser);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}