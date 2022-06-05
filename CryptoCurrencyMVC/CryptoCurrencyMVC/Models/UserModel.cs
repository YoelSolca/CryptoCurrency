﻿using System.ComponentModel.DataAnnotations;

namespace CryptoCurrencyMVC.Models
{
    public class UserModel: PersonModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Pleas enter the email!")]
        [Display(Name = "Enter email: ")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Pleas enter the password!")]
        [Display(Name = "Enter password: ")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
