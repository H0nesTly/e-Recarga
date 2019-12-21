using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace e_Recarga.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
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
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class TipoDeUtilizador 
    {
        public int id { get; set; }
        public string TipoNome { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Selecione o tipo de cliente")]
        [Display(Name = "Tipo de Cliente")]
        public int idTipoSelecionado { get; set; } = 2;
        public List<TipoDeUtilizador> tipoDeUtilizadores
        {
            get
            {
                List<TipoDeUtilizador> ListUtilizadores = new List<TipoDeUtilizador>()
                {
                    new TipoDeUtilizador() {id = 1, TipoNome="Rede Proprietaria" },
                    new TipoDeUtilizador() {id = 2, TipoNome="Utilizador Normal" }
                };
                return ListUtilizadores;
            }
        }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

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
        [CustomDataAnnotations.DataDeNascimento(ErrorMessage = "Tem que ser maior que 18 anos")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Nascimento")]
        public System.DateTime DataNascimento { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
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
