using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Recarga.Models
{
    public class Carregamento
    {
        public int CarregamentoId { get; set; }

        public int? VeiculoId { get; set; }
        public virtual Veiculo Veiculo { get; set; }

        public virtual Reserva Reserva { get; set; }

        public virtual int? ContaBancariaId { get; set; }
        public virtual ContaBancaria ContaBancaria { get; set; }
    }
}