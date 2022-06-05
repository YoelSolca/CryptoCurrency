namespace CryptoCurrencyMVC.Data
{
    public class Connection
    {
        private string connection = string.Empty;

        public Connection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            connection = builder.GetSection("ConnectionStrings:CryptocurrenciesDB").Value;
        }

        public string getConnection()
        {
            return connection;
        }
    }
}
