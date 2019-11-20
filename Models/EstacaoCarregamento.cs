using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Recarga.Models
{
    public class EstacaoCarregamento
    {
        public EstacaoCarregamento()
        {
            this.Postos = new HashSet<Posto>();
        }

        public int EstacaoCarregamentoId { get; set; }

        public virtual int? UtilizadorId { get; set; }
        public virtual Utilizador Utilizador { get; set; }

        public virtual ICollection<Posto> Postos { get; set; }
    }
}