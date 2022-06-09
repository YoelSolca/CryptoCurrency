using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoCurrencyMVC.Models
{
    public class PersonModel
    {
        [Column("ID")]
        public int PersonID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio!")]
        [Display(Name = "Enter nombre: ")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio!")]
        [Display(Name = "Ingresar apellido: ")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "El telefóno es obligatorio!")]
        [Display(Name = "Ingresar teléfono: ")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "El domicilio es obligatorio!")]
        [Display(Name = "Ingresar domicilio: ")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "El dni es obligatorio!")]
        [Display(Name = "Ingresar dni: ")]
        public string? Dni { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria!")]
        [Display(Name = "Ingresar fecha de nacimiento: ")]
        public DateTime Birthdate { get; set; }
    }
}
