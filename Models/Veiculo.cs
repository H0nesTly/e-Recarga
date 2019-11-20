using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Recarga.Models
{
    public class Veiculo
    {
        public Veiculo()
        {
            this.Reservas = new HashSet<Reserva>();
            this.Carregamentos = new HashSet<Carregamento>();
        }

        public int VeiculoId { get; set; }

        public int? UtilizadorId { get; set; }
        public virtual Utilizador Utilizador { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }
        public virtual ICollection<Carregamento> Carregamentos { get; set; }
    }
}