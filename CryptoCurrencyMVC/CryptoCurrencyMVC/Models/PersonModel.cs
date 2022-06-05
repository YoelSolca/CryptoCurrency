using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoCurrencyMVC.Models
{
    public class PersonModel
    {
        [Column("ID")]
        public int PersonID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string? LocationName { get; set; }


    }
}
