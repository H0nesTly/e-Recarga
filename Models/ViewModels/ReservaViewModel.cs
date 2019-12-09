using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_Recarga.Models.ViewModels
{
    public class ReservaViewModel
    {

        public NovaReservaViewModel novaReservaViewModel { get; set; }
        public ProcurarPostosViewModel procurarPostosViewModel { get; set; }

        
    }


    public class NovaReservaViewModel
    {
      
        [DataType(DataType.DateTime, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Data Prev. Início Carregamento")]
   
        public DateTime DataPrevInicioCarregamento { get; set; }

        
        [DataType(DataType.DateTime, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Data Prev. Fim Carregamento")]
      
        public DateTime DataPrevFimCarregamento { get; set; }

        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [Display(Name = "Tomada")]
        public int TomadaPostoID { get; set; }
        public virtual TomadaPosto TomadaPostoReserva { get; set; }

       


    }

    public class ProcurarPostosViewModel
    {
        [DataType(DataType.DateTime, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Data Início Carregamento")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public DateTime DataInicioCarregamento { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Data Fim Carregamento")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public DateTime DataFimCarregamento { get; set; }

        [Display(Name = "Concelho")]
        public int? ConcelhoID { get; set; }
        public virtual Concelho Concelho { get; set; }

        [Display(Name = "Distrito")]
        public int? DistritoID { get; set; }
        public virtual Distrito Distrito { get; set; }

        [Display(Name = "Estação Carregamento")]
        public int? EstacaoCarregamentoID { get; set; }
        public virtual EstacaoCarregamento EstacaoCarregamento { get; set; }

        [Display(Name = "Potência")]
        public int? PotenciaID { get; set; }
        public virtual Potencia Potencia { get; set; }

        [Display(Name = "Tomada")]
        public int? TomadaID { get; set; }
        public virtual Tomada Tomada { get; set; }


        public virtual ICollection<TomadaPosto> TomadaPostos { get; set; }
        public virtual ICollection<Concelho> Concelhos { get; set; }
        public virtual ICollection<Distrito> Distritos { get; set; }
        public virtual ICollection<EstacaoCarregamento> EstacaoCarregamentos { get; set; }
        public virtual ICollection<Potencia> Potencias { get; set; }
        public virtual ICollection<Tomada> Tomadas { get; set; }

    }


    public class IndexReservaViewModel
    {
        [Display(Name = "Reserva")]
        public int? ReservaID { get; set; }
        public virtual Reserva Reserva { get; set; }

        [Display(Name = "Estação Carregamento")]
        public int? EstacaoCarregamentoID { get; set; }
        public virtual EstacaoCarregamento EstacaoCarregamento { get; set; }

        [Display(Name = "Posto")]
        public int? PostoID { get; set; }
        public virtual Posto PostoCarregamento { get; set; }

        [Display(Name = "Tomada")]
        public int? TomadaPostoID { get; set; }
        public virtual TomadaPosto TomadaPosto { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Data Início Reserva")]
        public DateTime DataInicioReserva { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Data Fim Reserva")]
        public DateTime DataFimReserva { get; set; }

        [Display(Name = "Total Estimado")]
        public double? Total { get; set; }


        


    }





}