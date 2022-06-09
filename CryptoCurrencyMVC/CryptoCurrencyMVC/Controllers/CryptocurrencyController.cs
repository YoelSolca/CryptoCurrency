using CryptoCurrencyMVC.Data;
using CryptoCurrencyMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Web.Http.ModelBinding;

namespace CryptoCurrencyMVC.Controllers
{
    public class CryptocurrencyController : Controller
    {

        UserData userData = new UserData();
        TransferDepositData transferDepositData = new TransferDepositData();
        MoneyData moneyData = new MoneyData();

        public ActionResult List()
        {
           IEnumerable<CryptocurrencyModel> crypto = null;

            using (var client = new HttpClient())
            {

                var responseTask = client.GetAsync("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=100&page=1&sparkline=false");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<IList<CryptocurrencyModel>>();
                    read.Wait();
                    crypto = read.Result;
                }
                else
                {
                    crypto = Enumerable.Empty<CryptocurrencyModel>();
                    ModelState.AddModelError(string.Empty, "Server error occured.");
                }

            }


            return View(crypto);
        }



        public ActionResult Buy()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Buy(string value1, string value2)
        {
            var replaced = value1.Replace(',', '.');
            var replaced2 = value2.Replace(',', '.');

            var valueDouble2 = double.Parse(replaced2, CultureInfo.InvariantCulture.NumberFormat);
            var valueDouble3 = double.Parse(replaced, CultureInfo.InvariantCulture.NumberFormat);

            var valor3 = Convert.ToDouble(valueDouble2);

            ViewBag.Id = HttpContext.Session.GetString("Id");
            var resp = userData.GetUser(Convert.ToInt32(ViewBag.Id));

            if (resp.accountPeso.AccountBalance <= 0)
            {
                ViewBag.msg = "No posse fondos suficientes";
                return View();
            }

            if (valueDouble3 < 0)
            {
                ViewBag.msg = "No se puede ingresar numeros negativos";
                return View();
            }
            var data =  resp.AccountCryptocurrencyModel.Data;

            var replaced1 = data.Replace(',', '.');

            var convercion = double.Parse(replaced1, CultureInfo.InvariantCulture.NumberFormat);

            var res = convercion + valor3;

            char[] characters = res.ToString().ToCharArray();


            transferDepositData.EditCryptocurrency(characters, resp.ID);

            var respBuy = transferDepositData.Buy(characters, Convert.ToInt32(ViewBag.Id), "Compra", "BTC");


            return View();
        }


        public ActionResult SellPeso()
        {

            ViewBag.Id = HttpContext.Session.GetString("Id");
            var resp = userData.GetUser(Convert.ToInt32(ViewBag.Id));

            return View(resp);
        }


        [HttpPost]
        public ActionResult SellPeso(string value1, string value2)
        {

            var replaced = value1.Replace(',', '.');
            var valueDouble1 = double.Parse(replaced, CultureInfo.InvariantCulture.NumberFormat);

            var replaced2 = value2.Replace(',', '.');
            var valueDouble2 = double.Parse(replaced2, CultureInfo.InvariantCulture.NumberFormat);


            ViewBag.Id = HttpContext.Session.GetString("Id");
            var resp = userData.GetUser(Convert.ToInt32(ViewBag.Id));

            var data = resp.AccountCryptocurrencyModel.Data;

            var replaced3 = data.Replace(',', '.');
            var convercion = double.Parse(replaced3, CultureInfo.InvariantCulture.NumberFormat);

            if (Convert.ToDouble(resp.AccountCryptocurrencyModel.Data) <= 0)
            {
                ViewBag.msg = "No posse fondos suficientes";
                return View();
            }


            if (valueDouble1 < 0)
            {
                ViewBag.msg = "No se puede ingresar numeros negativos";
                return View();
            }

            var sum = convercion - valueDouble1;

            char[] characters = sum.ToString().ToCharArray();

            //agregar funcion
            transferDepositData.EditCryptocurrency(characters, resp.ID);


            var res = resp.accountPeso.AccountBalance + valueDouble2;
            char[] characters2 = valueDouble2.ToString().ToCharArray();

            var respuesta = transferDepositData.Buy(characters2, Convert.ToInt32(ViewBag.Id), "Venta", "Peso");

            moneyData.EditPeso(res, Convert.ToInt32(ViewBag.Id));


            return RedirectToAction("Index", "Home");
        }


        public ActionResult SellDollar()
        {
            ViewBag.Id = HttpContext.Session.GetString("Id");
            var resp = userData.GetUser(Convert.ToInt32(ViewBag.Id));

            return View(resp);
        }


        [HttpPost]
        public ActionResult SellDollar(string value1, string value2)
        {

            var replaced = value2.Replace(',', '.');
            var value = double.Parse(replaced, CultureInfo.InvariantCulture.NumberFormat);


            var replaced2 = value1.Replace(',', '.');
            var value3 = double.Parse(replaced2, CultureInfo.InvariantCulture.NumberFormat);

            ViewBag.Id = HttpContext.Session.GetString("Id");

            var resp = userData.GetUser(Convert.ToInt32(ViewBag.Id));

            if (Convert.ToDouble(resp.AccountCryptocurrencyModel.Data) <= 0)
            {
                ViewBag.msg = "No posse fondos suficientes";
                return View();
            }

            var data = resp.AccountCryptocurrencyModel.Data;
            var replaced1 = data.Replace(',', '.');
            var convercion = double.Parse(replaced1, CultureInfo.InvariantCulture.NumberFormat);


            var sum = convercion - value3;

            char[] characters = sum.ToString().ToCharArray();

            transferDepositData.EditCryptocurrency(characters, resp.ID);


            var res = resp.accountDollar.AccountBalance + value;
            char[] characters2 = value.ToString().ToCharArray();

            var respuesta = transferDepositData.Buy(characters2, Convert.ToInt32(ViewBag.Id), "Venta", "Dolar");

            moneyData.EditDollar(res, Convert.ToInt32(ViewBag.Id));


            return RedirectToAction("Index", "Home");
        }
    }
}
