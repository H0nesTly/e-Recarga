
using System.ComponentModel.DataAnnotations;

namespace e_Recarga.Models
{
    public class VeiculosViewModel
    {
        public string CodeDoUser { get; set; }

        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public string Matricula { get; set; }

        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public string Marca { get; set; }

        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public string Modelo { get; set; }
    }
}