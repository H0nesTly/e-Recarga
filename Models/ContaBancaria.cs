using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Recarga.Models
{
    public class ContaBancaria
    {
        public ContaBancaria()
        {
            this.Carregamentos = new HashSet<Carregamento>();
        }

        public int ContaBancariaId { get; set; }

        public int? UtilizadorId { get; set; }
        public virtual Utilizador Utilizador { get; set; }

        public virtual ICollection<Carregamento> Carregamentos { get; set; }

    }
}