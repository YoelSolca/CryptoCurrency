using CryptoCurrencyMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.ModelBinding;

namespace CryptoCurrencyMVC.Controllers
{
    public class CryptocurrencyController : Controller
    {
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
            CalculatorDollarModel crypto = null;

            using (var client = new HttpClient())
            {

                var responseTask = client.GetAsync("https://v6.exchangerate-api.com/v6/24c4dd304655d508d5c23614/latest/USD");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<CalculatorDollarModel>();
                    read.Wait();
                    crypto = read.Result;
                }
                else
                {
                    crypto  = null;
                    ModelState.AddModelError(string.Empty, "Server error occured.");
                }

            }


            return View(crypto);
        }

    }
}
