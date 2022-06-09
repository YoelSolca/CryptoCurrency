using CryptoCurrencyMVC.Data;
using Microsoft.AspNetCore.Mvc;
using PageList;

namespace CryptoCurrencyMVC.Controllers
{
    public class HistoryController : Controller
    {
        UserData userData = new UserData();
        HistoryData historyData = new HistoryData();

        public ActionResult History()
        {
            ViewBag.Id = HttpContext.Session.GetString("Id");
            var oUser = userData.GetUser(Convert.ToInt32(ViewBag.Id));

            var oList = historyData.History(oUser.ID);

            ViewBag.Operations = oList;

            return View();
        }

    }
}