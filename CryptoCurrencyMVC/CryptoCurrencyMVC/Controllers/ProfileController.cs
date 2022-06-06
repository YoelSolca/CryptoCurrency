using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoCurrencyMVC.Controllers
{
    public class ProfileController : Controller
    {
        // GET: ProfileController
        public ActionResult Profile()
        {
            return View();
        }
    }
}
