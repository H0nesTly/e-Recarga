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
            TomadaPostos = new HashSet<TomadaPosto>();
        }

        [Key]
        public int ID { get; set; }
                
        [Display(Name = "Corrente de Carregamento")]
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Formato do campo incorreto.")]
        public string CorrenteCarregamento { get; set; }
                
        [Display(Name = "Estação de Carregamento")]
        [ForeignKey("EstacaoCarregamentoPosto")]
        public int EstacaoCarregamentoID { get; set; }
        public virtual EstacaoCarregamento EstacaoCarregamentoPosto { get; set; }

        public virtual ICollection<TomadaPosto> TomadaPostos { get; set; }

    }

    public class TomadaPosto
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Tomada")]
        [ForeignKey("TomadaTomadaPosto")]
        public int TomadaID { get; set; }
        public virtual Tomada TomadaTomadaPosto { get; set; }

        [Display(Name = "Posto")]
        [ForeignKey("PostoTomadaPosto")]
        public int PostoID { get; set; }
        public virtual Posto PostoTomadaPosto { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Preço ao Minuto")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O preço tem de ser possitivo")]
        public double PrecoMinuto { get; set; }

        [Display(Name = "Potência")]
        [ForeignKey("PotenciaTomadaPosto")]
        public int PotenciaID { get; set; }
        public virtual Potencia PotenciaTomadaPosto { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }

    }

    public class Tomada
    {
        public Tomada()
        {
            TomadaPostos = new HashSet<TomadaPosto>();
        }

        [Key]
        public int ID { get; set; }

        [Display(Name = "Tipo de Tomada")]
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public string TipoTomada { get; set; }

        public virtual ICollection<TomadaPosto> TomadaPostos { get; set; }
    }
    

    public class Potencia
    {
        public Potencia()
        {
            TomadaPostos = new HashSet<TomadaPosto>();
        }

        [Key]
        public int ID { get; set; }

        [Display(Name = "Potência")]
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Formato do campo incorreto.")]
        public string PotenciaNominalKw { get; set; }

        [Display(Name = "Tipo de Potência")]
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public string TipoPotencia { get; set; }

        public virtual ICollection<TomadaPosto> TomadaPostos { get; set; }
    }

    public class EstacaoCarregamento
    {
        public EstacaoCarregamento()
        {
            this.Postos = new HashSet<Posto>();
        }

        [Key]
        public int ID { get; set; }
        
        [Display(Name = "Designação")]
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public string Designacao { get; set; }

        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Formato do campo incorreto.")]
        public string Latitude { get; set; }

        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Formato do campo incorreto.")]
        public string Longitude { get; set; }

        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [Display(Name = "Código-Postal")]
        [StringLength(10, ErrorMessage = "Tamanho do campo excedido.")]
        [RegularExpression(@"^\d{4}(-\d{3})?$", ErrorMessage = "Formato código postal incorreto.")]
        public string CodigoPostal { get; set; }

        
        [DataType(DataType.Text, ErrorMessage = "Formato campo incorreto.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        public string Localidade { get; set; }

        
        [ForeignKey("ConcelhoEstacaoCarregamento")]
        [Display(Name = "Concelho")]
        public int ConcelhoID { get; set; }
        public virtual Concelho ConcelhoEstacaoCarregamento { get; set; }

       
        [ForeignKey("UtilizadorEstacao")]
        [Display(Name = "Rede Proprietária")]
        public string UtilizadorID { get; set; }
        public virtual ApplicationUser UtilizadorEstacao { get; set; }
        
        public virtual ICollection<Posto> Postos { get; set; }
    } 

    public class Distrito
    {
        [Key]
        public int ID { get; set; }
        
        [Display(Name = "Distrito")]
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public string Nome { get; set; }

        public virtual ICollection<Concelho> Concelhos { get; set; }
    }

    public class Concelho
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Concelho")]
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public string Nome { get; set; }

        [Display(Name = "Distrito")]
        [ForeignKey("DistritoConcelho")]
        public int DistritoID { get; set; }
        public virtual Distrito DistritoConcelho { get; set; }
    }
    

    public class Veiculo
    {
        public Veiculo()
        {
            this.Carregamentos = new HashSet<Carregamento>();
        }

        [Key]
        public int ID { get; set; }

        [Display(Name = "Matrícula")]
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public string Matricula { get; set; }
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public string Marca { get; set; }
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public string Modelo { get; set; }

        [Display(Name = "Utilizador")]
        [ForeignKey("UtilizadorVeiculo")]
        public string UtilizadorID { get; set; }
        public virtual ApplicationUser UtilizadorVeiculo { get; set; }
        
        public virtual ICollection<Carregamento> Carregamentos { get; set; }
    }

    public class Reserva
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
        [ForeignKey("UtilizadorReserva")]
        public string UtilizadorID { get; set; }
        public virtual ApplicationUser UtilizadorReserva { get; set; }

        [Display(Name = "Carregamento")]
        [ForeignKey("CarregamentoReserva")]
        public int? CarregamentoID { get; set; }
        public virtual Carregamento CarregamentoReserva { get; set; }

        [Display(Name = "Tomada")]
        [ForeignKey("TomadaPostoReserva")]
        public int? TomadaPostoID { get; set; }
        public virtual TomadaPosto TomadaPostoReserva { get; set; }

        [Display(Name = "Cancelada")]
        public bool Cancelada { get; set; }
    }

    public class Carregamento
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Data Início Carregamento")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public DateTime DataInicioCarregamento { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Data Fim Carregamento")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public DateTime DataFimCarregamento { get; set; }      

        [Display(Name = "Utilizador")]
        [ForeignKey("UtilizadorCarregamento")]
        public string UtilizadorID { get; set; }
        public virtual ApplicationUser UtilizadorCarregamento { get; set; }

        [Display(Name = "Veículo")]
        [ForeignKey("VeiculoCarregamento")]
        public int VeiculoID { get; set; }
        public virtual Veiculo VeiculoCarregamento { get; set; }

        [Display(Name = "Tomada Carregamento")]
        [ForeignKey("TomadaCarregamento")]
        public int TomadaID { get; set; }
        public virtual Tomada TomadaCarregamento { get; set; }

        [Display(Name = "Conta Bancária Déb.")]
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

        [DataType(DataType.CreditCard, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Número Cartão")]
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public string NumeroCartao { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Formato campo incorreto.")]
        [Display(Name = "Data Validade")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public DateTime Validade { get; set; }

        [RegularExpression(@"^\d$", ErrorMessage = "Formato código postal incorreto.")]
        [StringLength(10, ErrorMessage = "Tamanho do campo excedido.")]
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        public string CVV { get; set; }
        
        public bool Ativa { get; set; }

        [Display(Name = "Utilizador")]
        [ForeignKey("UtilizadorContaBancaria")]
        public string UtilizadorID { get; set; }
        public virtual ApplicationUser UtilizadorContaBancaria { get; set; }

        public virtual ICollection<Carregamento> Carregamentos { get; set; }

    }














}