using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Recarga.Models
{
    public class Reserva
    {
        public int ReservaId { get; set; }



        public int? UtilizadorId { get; set; }
        public virtual Utilizador Utilizador { get; set; }

        public int? VeiculoId { get; set; }
        public virtual Veiculo Veiculo { get; set; }

        public virtual Carregamento Carregamento { get; set; }

        public int? PostoId { get; set; }
        public virtual Posto Posto { get; set; }
    }
}