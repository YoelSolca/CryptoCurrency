﻿namespace CryptoCurrencyMVC.Models
{
    public class AccountCryptocurrencyModel
    {
        public int ID { get; set; }
        public Guid UUID { get; set; }
        public double AccountBalance { get; set; }
        public int FK_AccountCryptoCurrency { get; set; }
    }
}
