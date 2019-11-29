using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Key]
        public int ID { get; set; }

        public string CorrenteCarregamento { get; set; }
        public string NumeroTomadas { get; set; }



        [ForeignKey("PotenciaPosto")]
        public int PotenciaID { get; set; }
        public virtual Potencia PotenciaPosto { get; set; }

        [ForeignKey("EstacaoCarregamentoPosto")]
        public int EstacaoCarregamentoID { get; set; }
        public virtual EstacaoCarregamento EstacaoCarregamentoPosto { get; set; }

        public virtual ICollection<TomadaPosto> TomadaPostos { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }
    }

    public class TomadaPosto
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("TomadaTomadaPosto")]
        public int TomadaID { get; set; }
        public virtual Tomada TomadaTomadaPosto { get; set; }

        [ForeignKey("PostoTomadaPosto")]
        public int PostoID { get; set; }
        public virtual Posto PostoTomadaPosto { get; set; }

        public double PrecoMinuto { get; set; }

    }

    public class Tomada
    {
        public Tomada()
        {
            TomadaPostos = new HashSet<TomadaPosto>();
        }

        [Key]
        public int ID { get; set; }
        public string TipoTomada { get; set; }

        public virtual ICollection<TomadaPosto> TomadaPostos { get; set; }
    }
    

    public class Potencia
    {
        public Potencia()
        {
            Postos = new HashSet<Posto>();
        }

        [Key]
        public int ID { get; set; }
        public string PotenciaNominal { get; set; }
        public string TipoPotencia { get; set; }

        public virtual ICollection<Posto> Postos { get; set; }
    }

    public class EstacaoCarregamento
    {
        public EstacaoCarregamento()
        {
            this.Postos = new HashSet<Posto>();
        }

        [Key]
        public int ID { get; set; }

        public string Designacao { get; set; }

        [ForeignKey("UtilizadorEstacao")]
        public string UtilizadorID { get; set; }
        public virtual ApplicationUser UtilizadorEstacao { get; set; }

        [ForeignKey("LocalizacaoEstacao")]
        public int LocalizacaoId { get; set; }
        public virtual Localizacao LocalizacaoEstacao { get; set; }

        public virtual ICollection<Posto> Postos { get; set; }
    }

    public class Localizacao
    {
        [Key]
        public int LocalizacaoId { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Regiao { get; set; }
        public string Concelho { get; set; }
        public string Freguesia { get; set; }
        public string Localidade { get; set; }
    }

    public class Veiculo
    {
        public Veiculo()
        {
            this.Carregamentos = new HashSet<Carregamento>();
        }

        [Key]
        public int ID { get; set; }

        public string Matricula { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }


        [ForeignKey("UtilizadorVeiculo")]
        public string UtilizadorID { get; set; }
        public virtual ApplicationUser UtilizadorVeiculo { get; set; }
        
        public virtual ICollection<Carregamento> Carregamentos { get; set; }
    }

    public class Reserva
    {

        [Key]
        public int ID { get; set; }

        public DateTime DataReserva { get; set; }
        public DateTime DataPrevInicioCarregamento { get; set; }
        public DateTime DataPrevFimCarregamento { get; set; }   //atributo que é o resultado da somada de 12h ao atributo DataPrevInicioCarregamento

        [ForeignKey("UtilizadorReserva")]
        public string UtilizadorID { get; set; }
        public virtual ApplicationUser UtilizadorReserva { get; set; }


        [ForeignKey("CarregamentoReserva")]
        public int? CarregamentoID { get; set; }
        public virtual Carregamento CarregamentoReserva { get; set; }

        [ForeignKey("PostoReserva")]
        public int? PostoID { get; set; }
        public virtual Posto PostoReserva { get; set; }
    }

    public class Carregamento
    {
        [Key]
        public int ID { get; set; }

        public DateTime DataInicioCarregamento { get; set; }
        public DateTime DataFimCarregamento { get; set; }

        
        [ForeignKey("UtilizadorCarregamento")]
        public string UtilizadorID { get; set; }
        public virtual ApplicationUser UtilizadorCarregamento { get; set; }

        [ForeignKey("VeiculoCarregamento")]
        public int VeiculoID { get; set; }
        public virtual Veiculo VeiculoCarregamento { get; set; }

        [ForeignKey("PostoCarregamento")]
        public int PostoID { get; set; }
        public virtual Posto PostoCarregamento { get; set; }

        [ForeignKey("ContaBancariaCarregamento")]
        public int ContaBancariaID { get; set; }
        public virtual ContaBancaria ContaBancariaCarregamento { get; set; }

    }

    public class ContaBancaria
    {
        public ContaBancaria()
        {
            this.Carregamentos = new HashSet<Carregamento>();
        }

        [Key]
        public int ContaBancariaId { get; set; }
        public string NumeroCartao { get; set; }
        public DateTime Validade { get; set; }
        public string CVV { get; set; }
        public bool Ativa { get; set; }
        

        [ForeignKey("UtilizadorContaBancaria")]
        public string UtilizadorID { get; set; }
        public virtual ApplicationUser UtilizadorContaBancaria { get; set; }

        public virtual ICollection<Carregamento> Carregamentos { get; set; }

    }














}