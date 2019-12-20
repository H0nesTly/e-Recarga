using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_Recarga.Models.ViewModels
{
    public class DetalhesPostoViewModel
    {
        public string correnteCarregamento { get; set; }

        public string estacaoDeCarregamento { get; set; }

        public int numeroDeTomadas { get; set; }

        public TomadaPostoViewModel tomadaPostoViewModel { get; set; }

    }

    public class TomadaPostoViewModel
    {
        public int codeDoPosto { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Preço ao Minuto")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "O preço tem de ser possitivo")]
        public double PrecoMinuto { get; set; }

        [Display(Name = "Potência")]
        [Required(ErrorMessage = "Error: Must Choose a Country")]
        public int? PotenciaID { get; set; }
        public virtual Potencia Potencia { get; set; }


        [Display(Name = "Tomada")]
        [Required(ErrorMessage = "Error: Must Choose a Country")]
        public int? TomadaID { get; set; }
        public virtual Tomada Tomada { get; set; }

        public virtual ICollection<Potencia> Potencias { get; set; }
        public virtual ICollection<Tomada> Tomadas { get; set; }
    }
}