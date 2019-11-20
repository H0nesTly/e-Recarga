using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace e_Recarga.Models
{
    public class e_RecargaContext :
        DbContext
    {
        public e_RecargaContext():
            base("e_RecargaDB")
        {
            //https://www.entityframeworktutorial.net/code-first/database-initialization-strategy-in-code-first.aspx
            Database.SetInitializer<e_RecargaContext>(new CreateDatabaseIfNotExists<e_RecargaContext>());


        }

        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<ContaBancaria> ContaBancarias { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Carregamento> Carregamentos { get; set; }
        public DbSet<Posto> Postos { get; set; }
        public DbSet<EstacaoCarregamento> EstacaoCarregamentos { get; set; }
        public DbSet<Localizacao> Localizacaos { get; set; }
    }
}