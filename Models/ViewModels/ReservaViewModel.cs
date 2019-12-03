using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_Recarga.Models.ViewModels
{
    public class ReservaViewModel
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Data Reserva")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public DateTime DataReserva { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Data Prev. Início Carregamento")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public DateTime DataPrevInicioCarregamento { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Data Prev. Fim Carregamento")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public DateTime DataPrevFimCarregamento { get; set; }   //atributo que é o resultado da somada de 12h ao atributo DataPrevInicioCarregamento

        [Display(Name = "Utilizador")]
        public string UtilizadorID { get; set; }
        public virtual ApplicationUser UtilizadorReserva { get; set; }

        [Display(Name = "Carregamento")]
        public int? CarregamentoID { get; set; }
        public virtual Carregamento CarregamentoReserva { get; set; }

        [Display(Name = "Tomada")]
        public int? TomadaPostoID { get; set; }
        public virtual TomadaPosto TomadaPostoReserva { get; set; }

        public ProcurarPostos ProcurarPostos { get; set; }
    }


    public class ProcurarPostos
    {
        [DataType(DataType.DateTime, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Data Início Carregamento")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public DateTime DataInicioCarregamento { get; set; }
        
        [Display(Name = "Concelho")]
        public int ConcelhoID { get; set; }
        public virtual Concelho Concelho { get; set; }

        [Display(Name = "Distrito")]
        public int DistritoID { get; set; }
        public virtual Distrito Distrito { get; set; }
        
    }


    



}