using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mvc5Project.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuário:")]
        public string LoginUsername { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha:")]
        public string LoginPassword { get; set; }

        [Display(Name = "Lembrar?")]
        public bool RememberMe { get; set; }
    }
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "O nome de {0} deve conter entre {2} e {1} caracteres.", MinimumLength = 5)]
        [RegularExpression("^([a-zA-Z0-9]{5,20})$", ErrorMessage = "O {0} deve conter apenas caracteres alfanuméricos")]
        [Display(Name = "Usuário:")]
        public string RegisterUsername { get; set; }

        [Required]
        [Display(Name = "Email:")]
        public string RegisterEmail { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "A {0} deve conter entre {2} e {1} caracteres.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha:")]
        public string RegisterPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha:")]
        [Compare("RegisterPassword", ErrorMessage = "As senhas não batem.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nome:")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Sobrenome:")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "País:")]
        public string Country { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Aniversário:")]
        public DateTime BirthDate { get; set; }
    }
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "O nome de {0} deve conter entre {2} e {1} caracteres.", MinimumLength = 5)]
        [RegularExpression("^([a-zA-Z0-9]{5,20})$", ErrorMessage = "O {0} deve conter apenas caracteres alfanuméricos")]
        [Display(Name = "Usuário:")]
        public string ExtUsername { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nome:")]
        public string ExtFirstName { get; set; }

        [Required]
        [Display(Name = "Sobrenome:")]
        public string ExtLastName { get; set; }

        [Required]
        [Display(Name = "País:")]
        public string ExtCountry { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Aniversário:")]
        public DateTime ExtBirthDate { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Lembrar desse navegador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} seve conter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "As senhas não batem!")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
