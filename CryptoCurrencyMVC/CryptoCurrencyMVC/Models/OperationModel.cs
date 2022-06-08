namespace CryptoCurrencyMVC.Models
{
    public class OperationModel
    {
        public int ID { get; set; }
    
        public string Title { get; set; }

        public DateTime Date { get; set; }  
        
        public double Amount { get; set; }

        public string Number { get; set; }

        //Sacar despues
        public string CBU { get; set; }
        
        public string Icon { get; set; }
    
    }
}
