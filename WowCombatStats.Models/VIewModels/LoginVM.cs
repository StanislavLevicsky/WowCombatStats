using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowCombatStats.Models.VIewModels
{
    public class LoginVM
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public bool isPersistent { get; set; } = false;
        public string? ReturnUrl { get; set; }
    }
}
