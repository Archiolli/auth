using System.ComponentModel.DataAnnotations;

namespace BancoApi.ViewModels
{
    public class AccountLoginViewModel
    {             
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty; 
        [Required]
        public double Saldo { get; set; }
              
    }

    public class AccountSignupViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;

    }


    public class AccountSaldoViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public double Saldo { get; set; }

    }

        public class AccountSaldoUpdateViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public double Saldo { get; set; }
    }
}
