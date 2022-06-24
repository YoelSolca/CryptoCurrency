using CryptoCurrencyMVC.Data;
using CryptoCurrencyMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace CryptoCurrencyMVC.Controllers
{
    public class HomeController : Controller
    {
        UserData userData = new UserData();
        HistoryData historyData = new HistoryData();

        public IActionResult Index()
        {
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.password = HttpContext.Session.GetString("password");


            if(ViewBag.email == null && ViewBag.password == null)
            {
                return RedirectToAction("Login", "Login");
            }

            //Obtener usuario por Id
            ViewBag.Id = HttpContext.Session.GetString("Id");
            var oUser = userData.GetUser(Convert.ToInt32(ViewBag.Id));


            ViewBag.data = oUser.AccountCryptocurrencyModel.Data;

            var oList = historyData.Movements(oUser.ID);

            ViewBag.Operations = oList;

            return View(oUser);
        }
    
    }
}