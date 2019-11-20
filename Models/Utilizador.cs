using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace e_Recarga.Models
{
    public class Utilizador : IdentityUser
    {
        public Utilizador()
        {
            this.Veiculos = new HashSet<Veiculo>();
            this.Reservas = new HashSet<Reserva>();
            this.EstacaoCarregamentos = new HashSet<EstacaoCarregamento>();
            this.ContaBancarias = new HashSet<ContaBancaria>();
        }

        public int UtilizadorId { get; set; }

        public virtual ICollection<Veiculo> Veiculos { get; set; }
        public virtual ICollection<Reserva> Reservas{ get; set; }
        public virtual ICollection<EstacaoCarregamento> EstacaoCarregamentos{ get; set; }
        public virtual ICollection<ContaBancaria> ContaBancarias { get; set; }  
    }
}