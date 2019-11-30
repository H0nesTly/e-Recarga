using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace e_Recarga.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        public string Nome { get; set; }
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        public string Morada { get; set; }
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        public string Concelho { get; set; }
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        public string Freguesia { get; set; }
        [StringLength(10, ErrorMessage = "Tamanho do campo excedido.")]
        [RegularExpression(@"^\d{4}(-\d{3})?$", ErrorMessage = "Formato código postal incorreto.")]
        public string CodigoPostal { get; set; }
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        public string Localidade { get; set; }
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        public string Telemovel { get; set; }
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        public string Telefone { get; set; }
        [StringLength(9, ErrorMessage = "Tamanho do campo excedido.")]
        public string NIF { get; set; }
        public DateTime? DataNascimento { get; set; }
        [StringLength(150, ErrorMessage = "Tamanho do campo excedido.")]
        public string Pais { get; set; }


        public virtual ICollection<Reserva> Reservas { get; set; }
        public virtual ICollection<Carregamento> Carregamentos { get; set; }
        public virtual ICollection<Veiculo> Veiculos { get; set; }
        public virtual ICollection<EstacaoCarregamento> EstacaoCarregamentos { get; set; }
        public virtual ICollection<ContaBancaria> ContaBancarias { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("e-RecargaDB", throwIfV1Schema: false)
        {
            Database.SetInitializer(new ApplicationDbInitializer());
            //Other template initializers
            //Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Localizacao> Localizacaos { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<ContaBancaria> ContaBancarias { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Carregamento> Carregamentos { get; set; }
        public DbSet<Tomada> Tomadas { get; set; }
        public DbSet<Potencia> Potencias { get; set; }
        public DbSet<TomadaPosto> TomadaPostos { get; set; }
        public DbSet<Posto> Postos { get; set; }
        public DbSet<EstacaoCarregamento> EstacaoCarregamentoes { get; set; }
        public IEnumerable ApplicationUsers { get; internal set; }
    }

    public class ApplicationDbInitializer
    : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //Create User=SuperAdmin@Admin.com with password=SuperAdmin@123456 in the SuperAdmin role
        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new ApplicationUserManager(userStore);
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new ApplicationRoleManager(roleStore);

            const string saname = "superadmin@example.com";
            const string sapassword = "Sadmin@12345";

            const string aname = "admin@example.com";
            const string apassword = "Admin@12345";

            const string rpname = "rp@example.com";
            const string rppassword = "Rp@12345";

            const string unname = "un@example.com";
            const string unpassword = "Un@12345";


            //Create Role SuperAdmin, see the function bellow
            var superadminrole = CreateRole(roleManager, "SuperAdmin");

            //Create Role Admin, see the function bellow
            var adminrole = CreateRole(roleManager, "Admin");

            //Create Role Rede Proprietária, see the function bellow
            var redeproprietariarole = CreateRole(roleManager, "RedeProprietaria");

            //Create Role Rede Proprietária, see the function bellow
            var utilizadornomalrole = CreateRole(roleManager, "UtilizadorNormal");

            //Create the SuperAdmin user
           
            CreateUser(userManager, saname, sapassword, "", "", "", "", "", "", "", "", null, "", "", superadminrole);

            //Create Admin user 
            CreateUser(userManager, aname, apassword, "", "", "", "", "", "", "", "", null, "", "", adminrole);

            CreateUser(userManager, rpname, rppassword, "", "", "", "", "", "", "", "", null, "", "", redeproprietariarole);

            CreateUser(userManager, unname, unpassword, "", "", "", "", "", "", "", "", null, "", "", utilizadornomalrole);

            CreateData();
        }

        private static void CreateData()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            Tomada tomada1 = new Tomada()
            {
                TipoTomada = "Mennekes"
            };
            Tomada tomada2 = new Tomada()
            {
                TipoTomada = "Industrial"
            };
            Tomada tomada3 = new Tomada()
            {
                TipoTomada = "CCS"
            };
            Tomada tomada4 = new Tomada()
            {
                TipoTomada = "CHAdeMO"
            };

            dbContext.Tomadas.Add(tomada1);
            dbContext.Tomadas.Add(tomada2);
            dbContext.Tomadas.Add(tomada3);
            dbContext.Tomadas.Add(tomada4);



            Potencia potencia1 = new Potencia()
            {
                PotenciaNominal = "3.68 Kw",
                TipoPotencia = "Normal"
            };
            Potencia potencia2 = new Potencia()
            {
                PotenciaNominal = "7.4 Kw",
                TipoPotencia = "Normal"
            };
            Potencia potencia3 = new Potencia()
            {
                PotenciaNominal = "22 Kw",
                TipoPotencia = "Semi Rápido"
            };
            Potencia potencia4 = new Potencia()
            {
                PotenciaNominal = "50 Kw",
                TipoPotencia = "Rápido"
            };
            dbContext.Potencias.Add(potencia1);
            dbContext.Potencias.Add(potencia2);
            dbContext.Potencias.Add(potencia3);
            dbContext.Potencias.Add(potencia4);


            Localizacao localizacao1 = new Localizacao()
            {
                Regiao = "Coimbra",
                Concelho = "Santo Antonio Dos Olivais",
                Freguesia = "Santo Antonio Dos Olivais",
                Localidade = "Solum",
                Longitude = "00.00",
                Latitude = "00.00"
            };
            dbContext.Localizacaos.Add(localizacao1);

            dbContext.SaveChanges();
        }



        private static IdentityRole CreateRole(ApplicationRoleManager roleManager, string roleName)
        {
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }
            return role;
        }

        public string Nome { get; set; }
        public string Morada { get; set; }
        public string Concelho { get; set; }
        public string Freguesia { get; set; }
        public string CodigoPostal { get; set; }
        public string Localidade { get; set; }
        public string Telemovel { get; set; }
        public string Telefone { get; set; }
        public string NIF { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Pais { get; set; }

        private static ApplicationUser CreateUser(ApplicationUserManager userManager, string name, string password, string morada, string concelho, string freguesia, string codigopostal, string localidade, string telemovel, string telefone, string nif, DateTime? datanascimento, string pais, string nome, IdentityRole role)
        {
            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, Morada = morada, Concelho = concelho, Freguesia = freguesia, CodigoPostal = codigopostal, Localidade = localidade, Telemovel = telemovel, Telefone = telefone, NIF = nif, DataNascimento = datanascimento, Pais = pais, Nome = nome };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user to role if not already added
            if (role != null)
            {
                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(role.Name))
                {
                    var result = userManager.AddToRole(user.Id, role.Name);
                }
            }


            return user;
        }
    }
}