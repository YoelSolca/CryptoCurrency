namespace CryptoCurrencyMVC.Models
{
    public class UserOperation
    {
        public int ID { get; set; }

        public int FK_UserID { get; set; }
        public int FK_Extraction { get; set; }
        public int FK_Transfer { get; set; }
        public int FK_Deposit { get; set; }
        public int FK_Investment { get; set; }

        public UserModel User { get; set; }
        public ExtractionModel Extraction { get; set; }
        public TransferModel Transfer { get; set; }
        public DepositModel Deposit { get; set; }
        public InvestmentModel Investment { get; set; }
    }
}
