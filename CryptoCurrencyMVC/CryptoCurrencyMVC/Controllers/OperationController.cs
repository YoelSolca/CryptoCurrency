using CryptoCurrencyMVC.Data;
using CryptoCurrencyMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CryptoCurrencyMVC.Controllers
{
    public class OperationController : Controller
    {

        UserData userData = new UserData();

        //Peso
        // GET: OperationController
        public ActionResult DepositPeso()
        {
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.password = HttpContext.Session.GetString("password");

            var oUser = userData.ObtenerUsuario(ViewBag.email, ViewBag.password);


            return View(oUser);
        }

        // GET: OperationController
        public ActionResult DepositDolar()
        {
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.password = HttpContext.Session.GetString("password");

            var oUser = userData.ObtenerUsuario(ViewBag.email, ViewBag.password);


            return View(oUser);

        }


        public ActionResult DepositCrypto()
        {
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.password = HttpContext.Session.GetString("password");

            var oUser = userData.ObtenerUsuario(ViewBag.email, ViewBag.password);


            return View(oUser);
        }
      
        public ActionResult Transfer()
        {
            return View();
        }

        //Transferencias y depositos 
        [HttpPost]
        public ActionResult Transfer(TransferModel transfer)
        {
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.password = HttpContext.Session.GetString("password");

            var resp = userData.ObtenerUsuario(ViewBag.email, ViewBag.password);

            var res = resp.accountPeso.AccountBalance - transfer.Amount;



            var r = userData.VerificarCBU(transfer.CBU);

            //obentgo usuario a enviar
            var rdeposito = userData.Deposito(r.ID);

            var sum = rdeposito.AccountBalance + transfer.Amount;


            userData.Editar(sum, r.ID);


            if (r.CBU == null)
            {
                ViewBag.msg = "CBU ingresado es incorrecto";
                return View();
            }

            ViewBag.userId = HttpContext.Session.GetString("userId");



            var respuesta = userData.Transfer(transfer.Amount, Convert.ToInt32(ViewBag.userId), r.ID);

            //actulaizar dinero
            userData.Editar(res, Convert.ToInt32(ViewBag.userId));


            if (respuesta)
                return RedirectToAction("Transfer");
            else
                return View();
        }


        public ActionResult TransferDollar()
        {
            return View();
        }


        //Transferencias y depositos 
        [HttpPost]
        public ActionResult TransferDollar(TransferModel transfer)
        {

            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.password = HttpContext.Session.GetString("password");

            var resp = userData.ObtenerUsuario(ViewBag.email, ViewBag.password);

            var res = resp.accountDollar.AccountBalance - transfer.Amount;


            var r = userData.VerificarCBUDollar(transfer.CBU);

            //obentgo usuario a enviar
            var rdeposito = userData.DepositoDolar(r.ID);

            var sum = rdeposito.AccountBalance + transfer.Amount;


            userData.EditarDolar(sum, r.ID);


            if (r.CBU == null)
            {
                ViewBag.msg = "CBU ingresado es incorrecto";
                return View();
            }

            ViewBag.userId = HttpContext.Session.GetString("userId");



            var respuesta = userData.Transfer(transfer.Amount, Convert.ToInt32(ViewBag.userId), r.ID);

            //actulaizar dinero
            userData.EditarDolar(res, Convert.ToInt32(ViewBag.userId));


            if (respuesta)
                return RedirectToAction("Transfer");
            else
                return View();
        }


        public ActionResult TransferCrypto()
        {
            return View();
        }


        //Transferencias y depositos 
        [HttpPost]
        public ActionResult TransferCrypto(string valor, AccountCryptocurrencyModel accountModel)
        {

            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.password = HttpContext.Session.GetString("password");

            var resp = userData.ObtenerUsuario(ViewBag.email, ViewBag.password);

            var replaced = valor.Replace(',', '.');
            var value = double.Parse(replaced, CultureInfo.InvariantCulture.NumberFormat);

            var replaced2 = resp.AccountCryptocurrencyModel.data.Replace(',', '.');

            var value2 = double.Parse(replaced2, CultureInfo.InvariantCulture.NumberFormat);

            var res = value2- value;


            var r = userData.VerificarUUID(accountModel.UUID);


            //obentgo usuario a enviar
            var rdeposito = userData.DepositoCrypto(r.ID);

            //
            var sum = rdeposito.AccountBalance + value;


            //antes

            char[] characters = res.ToString().ToCharArray();
            char[] characters2 = sum.ToString().ToCharArray();

            //agregar funcion

            userData.Editar(characters2, r.ID);
            //userData.Editar(res, resp.ID);
            userData.Editar(characters, resp.ID);

            string guidValue = r.UUID.ToString("");

            if(guidValue == "00000000-0000-0000-0000-000000000000")
            {
                ViewBag.msg = "UUID ingresado es incorrecto";
                return View();
            }



            var respuesta = userData.Transfer(value, resp.ID, r.ID);

            //actulaizar dinero
            //userData.EditarDolar(res, Convert.ToInt32(ViewBag.userId));


            if (respuesta)
              return RedirectToAction("Index", "Home");
             else
            return View();
        }






    }
}

