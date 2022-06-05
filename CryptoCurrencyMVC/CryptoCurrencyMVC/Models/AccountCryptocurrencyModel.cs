namespace CryptoCurrencyMVC.Models
{
    public class AccountCryptocurrencyModel
    {
        public int ID { get; set; }
        public Guid UUID { get; set; }
        public float AccountBalance { get; set; }
        public int FK_AccountCryptoCurrency { get; set; }
    }
}
