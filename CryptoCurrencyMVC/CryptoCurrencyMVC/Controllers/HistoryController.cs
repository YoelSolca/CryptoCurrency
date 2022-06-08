using CryptoCurrencyMVC.Data;
using Microsoft.AspNetCore.Mvc;
using PageList;

namespace CryptoCurrencyMVC.Controllers
{
    public class HistoryController : Controller
    {
        UserData userData = new UserData();

        public ActionResult History(int? i)
        {

            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.password = HttpContext.Session.GetString("password");

            var oUser = userData.ObtenerUsuario(ViewBag.email, ViewBag.password);

            var oList = userData.History(oUser.ID);

            ViewBag.Operations = oList;

            return View();
        }

    }
}