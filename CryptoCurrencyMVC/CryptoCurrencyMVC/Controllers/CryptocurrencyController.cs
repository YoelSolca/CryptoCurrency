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
        // GET: CryptocurrencyController
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



        public ActionResult Comprar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Comprar(string value1, string value2, string value3)
        {

            var replaced = value2.Replace(',', '.');
            var replaced2 = value3.Replace(',', '.');

            var valor1 = Convert.ToDouble(value1);


            var value = double.Parse(replaced, CultureInfo.InvariantCulture.NumberFormat);
            var value33 = double.Parse(replaced2, CultureInfo.InvariantCulture.NumberFormat);

            var valor2 = Convert.ToDouble(value);

            //pasar
            var valor3 = Convert.ToDouble(value33);



                ViewBag.email = HttpContext.Session.GetString("email");
                ViewBag.password = HttpContext.Session.GetString("password");

                var resp = userData.ObtenerUsuario(ViewBag.email, ViewBag.password);


                    var a =  resp.AccountCryptocurrencyModel.data;

             var replaced1 = a.Replace(',', '.');

            var convercion = double.Parse(replaced1, CultureInfo.InvariantCulture.NumberFormat);

                var res = convercion + valor3;

            char[] characters = res.ToString().ToCharArray();
            
            //agregar funcion
            userData.Editar(characters, resp.ID);


                ViewBag.userId = HttpContext.Session.GetString("userId");





            var respuesta = userData.Comprar(characters, Convert.ToInt32(ViewBag.userId));


            //CalculatorDollarModel crypto = null;

            //using (var client = new HttpClient())
            //{

            //    var responseTask = client.GetAsync("https://v6.exchangerate-api.com/v6/24c4dd304655d508d5c23614/latest/USD");
            //    responseTask.Wait();

            //    var result = responseTask.Result;

            //    if (result.IsSuccessStatusCode)
            //    {
            //        var read = result.Content.ReadAsAsync<CalculatorDollarModel>();
            //        read.Wait();
            //        crypto = read.Result;
            //    }
            //    else
            //    {
            //        crypto  = null;
            //        ModelState.AddModelError(string.Empty, "Server error occured.");
            //    }

            //}


            return View();
        }


        public ActionResult Venta()
        {
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.password = HttpContext.Session.GetString("password");

            var resp = userData.ObtenerUsuario(ViewBag.email, ViewBag.password);

            return View(resp);
        }


        [HttpPost]
        public ActionResult Venta(string value1, string value2, string value3)
        {

            var replaced = value2.Replace(',', '.');
            var replaced2 = value3.Replace(',', '.');

            var valor1 = Convert.ToDouble(value1);


            var value = double.Parse(replaced, CultureInfo.InvariantCulture.NumberFormat);
            var value33 = double.Parse(replaced2, CultureInfo.InvariantCulture.NumberFormat);

            var valor2 = Convert.ToDouble(value);

            //pasar
            var valor3 = Convert.ToDouble(value33);



            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.password = HttpContext.Session.GetString("password");

            var resp = userData.ObtenerUsuario(ViewBag.email, ViewBag.password);


            var a = resp.AccountCryptocurrencyModel.data;

            var replaced1 = a.Replace(',', '.');

            var convercion = double.Parse(replaced1, CultureInfo.InvariantCulture.NumberFormat);

            var res = convercion + valor3;

            char[] characters = res.ToString().ToCharArray();

            //agregar funcion
            userData.Editar(characters, resp.ID);


            ViewBag.userId = HttpContext.Session.GetString("userId");


            var respuesta = userData.Comprar(characters, Convert.ToInt32(ViewBag.userId));


            //CalculatorDollarModel crypto = null;

            //using (var client = new HttpClient())
            //{

            //    var responseTask = client.GetAsync("https://v6.exchangerate-api.com/v6/24c4dd304655d508d5c23614/latest/USD");
            //    responseTask.Wait();

            //    var result = responseTask.Result;

            //    if (result.IsSuccessStatusCode)
            //    {
            //        var read = result.Content.ReadAsAsync<CalculatorDollarModel>();
            //        read.Wait();
            //        crypto = read.Result;
            //    }
            //    else
            //    {
            //        crypto  = null;
            //        ModelState.AddModelError(string.Empty, "Server error occured.");
            //    }

            //}


            return View();
        }

    }
}
