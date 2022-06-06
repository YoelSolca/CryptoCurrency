using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoCurrencyMVC.Controllers
{
    public class OperationController : Controller
    {
        // GET: OperationController
        public ActionResult Deposit()
        {
            return View();
        }

        public ActionResult Transfer()
        {
            return View();
        }
   
    }
}
