using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Models.ViewModels
{
    public class SetOrResetPasswordVM
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public bool IsReset { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
