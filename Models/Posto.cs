using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Recarga.Models
{
    public class Posto
    {
        public Posto()
        {
            Reservas = new HashSet<Reserva>();
        }

        public int PostoId { get; set; }

        public virtual int? EstacaoCarregamentoId { get; set; }
        public virtual EstacaoCarregamento EstacaoCarregamento { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}