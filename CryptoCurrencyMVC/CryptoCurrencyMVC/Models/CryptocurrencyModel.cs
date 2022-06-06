namespace CryptoCurrencyMVC.Models
{
    public class CryptocurrencyModel
    {
        public string name { get; set; }

        public string image { get; set; }

        public string symbol { get; set; }
        public double current_price { get; set; }

        public double price_change_percentage_24h { get; set; }

        public double total_volume { get; set; }
    }



    public class Images
    {
        public string small { get; set; }
    }
}
