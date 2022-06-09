using CryptoCurrencyMVC.Data;
using CryptoCurrencyMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CryptoCurrencyMVC.Controllers
{
    public class OperationController : Controller
    {

        TransferDepositData transferDepositData = new TransferDepositData();
        UserData userData = new UserData();
        MoneyData moneyData = new MoneyData();

        //Peso
        // GET: OperationController
        public ActionResult DepositPeso()
        {
            ViewBag.Id = HttpContext.Session.GetString("Id");
            var oUser = userData.GetUser(Convert.ToInt32(ViewBag.Id));

            return View(oUser);
        }

        // GET: OperationController
        public ActionResult DepositDollar()
        {
            ViewBag.Id = HttpContext.Session.GetString("Id");
            var oUser = userData.GetUser(Convert.ToInt32(ViewBag.Id));

            return View(oUser);

        }


        public ActionResult DepositCryptocurrency()
        {
            ViewBag.Id = HttpContext.Session.GetString("Id");
            var oUser = userData.GetUser(Convert.ToInt32(ViewBag.Id));


            return View(oUser);
        }
      
        public ActionResult TransferPeso()
        {
            return View();
        }

        //Transferencias y depositos 
        [HttpPost]
        public ActionResult TransferPeso(TransferModel oTransfer)
        {

            ViewBag.Id = HttpContext.Session.GetString("Id");
            var resp = userData.GetUser(Convert.ToInt32(ViewBag.Id));

            if (resp.accountPeso.AccountBalance <= 0)
            {
                ViewBag.msg = "No posse fondos suficientes";
                return View();
            }

            if (oTransfer.Amount < 0)
            {
                ViewBag.msg = "No se puede ingresar numeros negativos";
                return View();
            }

            var oTransferPeso = moneyData.VerifyCBUPeso(oTransfer.CBU);
            if (oTransferPeso.CBU == null)
            {
                ViewBag.msg = "CBU ingresado es incorrecto";
                return View();
            }

            var res = resp.accountPeso.AccountBalance - oTransfer.Amount;



            var rdeposito = moneyData.GetPeso(oTransferPeso.ID);
            var sum = rdeposito.AccountBalance + oTransfer.Amount;

            moneyData.EditPeso(sum, oTransferPeso.ID);


            var respuesta = transferDepositData.TransferDeposit(oTransfer.Amount, Convert.ToInt32(ViewBag.Id), oTransferPeso.ID, "Peso");

            //actulaizar dinero
            moneyData.EditPeso(res, Convert.ToInt32(ViewBag.Id));


            if (respuesta)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }


        public ActionResult TransferDollar()
        {
            return View();
        }


        //Transferencias y depositos 
        [HttpPost]
        public ActionResult TransferDollar(TransferModel oTransfer)
        {

            ViewBag.Id = HttpContext.Session.GetString("Id");
            var resp = userData.GetUser(Convert.ToInt32(ViewBag.Id));

            if (resp.accountDollar.AccountBalance <= 0)
            {
                ViewBag.msg = "No posse fondos suficientes";
                return View();
            }

            if (oTransfer.Amount < 0)
            {
                ViewBag.msg = "No se puede ingresar numeros negativos";
                return View();
            }


            var oTransferDollar = moneyData.VerifyCBUDollar(oTransfer.CBU);

            if (oTransferDollar == null || oTransfer.CBU == null)
            {
                ViewBag.msg = "CBU ingresado es incorrecto";
                return View();
            }

            var res = resp.accountDollar.AccountBalance - oTransfer.Amount;
            //obentgo usuario a enviar
            var rdeposito = moneyData.GetDollar(oTransferDollar.ID);

            var sum = rdeposito.AccountBalance + oTransfer.Amount;


            moneyData.EditDollar(sum, oTransferDollar.ID);


            ViewBag.userId = HttpContext.Session.GetString("userId");



            var respuesta = transferDepositData.TransferDeposit(oTransfer.Amount, Convert.ToInt32(ViewBag.Id), oTransferDollar.ID, "Dolar");

            //actulaizar dinero
            moneyData.EditDollar(res, Convert.ToInt32(ViewBag.Id));


            if (respuesta)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }


        public ActionResult TransferCryptocurrency()
        {
            return View();
        }


        //Transferencias y depositos 
        [HttpPost]
        public ActionResult TransferCryptocurrency(string valor, AccountCryptocurrencyModel oAccountModel)
        {

            ViewBag.Id = HttpContext.Session.GetString("Id");
            var oUser = userData.GetUser(Convert.ToInt32(ViewBag.Id));


            var replaced = valor.Replace(',', '.');
            var value = double.Parse(replaced, CultureInfo.InvariantCulture.NumberFormat);

            var replaced2 = oUser.AccountCryptocurrencyModel.Data.Replace(',', '.');

            var value2 = double.Parse(replaced2, CultureInfo.InvariantCulture.NumberFormat);

            if(value2 <= 0)
            {
                ViewBag.msg = "No posse fondos suficientes";
                return View();
            }

            if (value < 0)
            {
                ViewBag.msg = "No se puede ingresar numeros negativos";
                return View();
            }

            var oVerifyAccountCrypto = moneyData.VerifyUUID(oAccountModel.UUID);

            string guidValue = oVerifyAccountCrypto.UUID.ToString("");

            if(guidValue == "00000000-0000-0000-0000-000000000000")
            {
                ViewBag.msg = "UUID ingresado es incorrecto";
                return View();
            }

            var res = value2 - value;

          
            var oAccountCrypto = moneyData.GetCryptocurrency(oVerifyAccountCrypto.ID);

            var sum = oAccountCrypto.AccountBalance + value;


            char[] characters = res.ToString().ToCharArray();
            char[] characters2 = sum.ToString().ToCharArray();


            transferDepositData.EditCryptocurrency(characters2, oVerifyAccountCrypto.ID);

            transferDepositData.EditCryptocurrency(characters, oUser.ID);




            var respuesta = transferDepositData.TransferDeposit(value, oUser.ID, oVerifyAccountCrypto.ID, "BTC");

            if (respuesta)
              return RedirectToAction("Index", "Home");
             else
            return View();
        }
    }
}

