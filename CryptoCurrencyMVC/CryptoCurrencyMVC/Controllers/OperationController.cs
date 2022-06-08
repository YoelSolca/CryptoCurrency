using CryptoCurrencyMVC.Data;
using CryptoCurrencyMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoCurrencyMVC.Controllers
{
    public class OperationController : Controller
    {

        UserData userData = new UserData();

        // GET: OperationController
        public void Deposit()
        {
            

        }

        public ActionResult Transfer()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Transfer(TransferModel transfer)
        {

            ViewBag.Amount = HttpContext.Session.GetString("Amount");

            ViewBag.Amount = Convert.ToDouble(ViewBag.Amount);

            var res = ViewBag.Amount - transfer.Amount;

            var r = userData.VerificarCBU(transfer.CBU);

            if (r.CBU == null)
            {
                ViewBag.msg = "CBU ingresado es incorrecto";
                return View();
            }

            ViewBag.userId = HttpContext.Session.GetString("userId");



            var respuesta = userData.Transfer(transfer, Convert.ToInt32(ViewBag.userId),r.ID);

            //actulaizar dinero
            userData.Editar(res, 21);


            if (respuesta)
                return RedirectToAction("Transfer");
            else
                return View();
        }


        public ActionResult DepositPeso()
        {

            return View();
        }

    }
}
