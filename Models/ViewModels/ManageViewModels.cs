﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace e_Recarga.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class EditDadosPessoaisViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Campo {0} excede os caracters {1}.")]
        [DataType(DataType.Text)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Campo {0} excede os caracters {1}.")]
        [DataType(DataType.Text)]
        [Display(Name = "País")]
        public string Pais { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Campo {0} excede os caracters {1}.")]
        [DataType(DataType.Text)]
        [Display(Name = "Concelho")]
        public string Concelho { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Campo {0} excede os caracters {1}.")]
        [DataType(DataType.Text)]
        [Display(Name = "Freguesia")]
        public string Freguesia { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Campo {0} excede os caracters {1}.")]
        [DataType(DataType.Text)]
        [Display(Name = "Localidade")]
        public string Localidade { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}(-\d{3})?$", ErrorMessage = "Formato código postal incorreto.")]
        [DataType(DataType.Text)]
        [Display(Name = "Código Postal")]
        public string CodigoPostal { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Campo {0} excede os caracters {1}.")]
        [DataType(DataType.Text)]
        [Display(Name = "Morada")]
        public string Morada { get; set; }

        [Required]
        [StringLength(9, ErrorMessage = "Campo {0} excede os caracters {1}.", MinimumLength = 9)]
        [DataType(DataType.Text)]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Required]
        [StringLength(9, ErrorMessage = "Campo {0} excede os caracters {1}.", MinimumLength = 9)]
        [DataType(DataType.Text)]
        [Display(Name = "Telemovel")]
        public string Telemovel { get; set; }

        [Required]
        [StringLength(9, ErrorMessage = "Campo {0} excede os caracters {1}.", MinimumLength = 9)]
        [DataType(DataType.Text)]
        [Display(Name = "NIF")]
        public string NIF { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Nascimento")]
        public System.DateTime? DataNascimento { get; set; }

        public VeiculosViewModel veiculosViewModel { get; set; }

        public IEnumerable<VeiculosListViewModel> EnumVeiculosViewModel { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}