using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowCombatStats.Models.ViewModels
{
    public class UserRegistrationVM
    {
        [Required]
        [Remote(action: "LoginIsExist", controller: "Auth", ErrorMessage = "Login is exist.")]
        public string Login { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Minimum password length 6 characters.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Required]
        public string Email { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
