using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
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

        public DbSet<Concelho> Concelhoes { get; set; }
        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<ContaBancaria> ContaBancarias { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Carregamento> Carregamentoes { get; set; }
        public DbSet<Tomada> Tomadas { get; set; }
        public DbSet<Potencia> Potencias { get; set; }
        public DbSet<TomadaPosto> TomadaPostoes { get; set; }
        public DbSet<Posto> Postoes { get; set; }
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
                PotenciaNominalKw = "3.68",
                TipoPotencia = "Normal"
            };
            Potencia potencia2 = new Potencia()
            {
                PotenciaNominalKw = "7.4",
                TipoPotencia = "Normal"
            };
            Potencia potencia3 = new Potencia()
            {
                PotenciaNominalKw = "22",
                TipoPotencia = "Semi Rápido"
            };
            Potencia potencia4 = new Potencia()
            {
                PotenciaNominalKw = "50",
                TipoPotencia = "Rápido"
            };
            dbContext.Potencias.Add(potencia1);
            dbContext.Potencias.Add(potencia2);
            dbContext.Potencias.Add(potencia3);
            dbContext.Potencias.Add(potencia4);


            Distrito distrito1 = new Distrito() { Nome = "Aveiro" };
            Distrito distrito2 = new Distrito() { Nome = "Beja" };
            Distrito distrito3 = new Distrito() { Nome = "Braga" };
            Distrito distrito4 = new Distrito() { Nome = "Bragança" };
            Distrito distrito5 = new Distrito() { Nome = "Castelo Branco" };
            Distrito distrito6 = new Distrito() { Nome = "Coimbra" };
            Distrito distrito7 = new Distrito() { Nome = "Evora" };
            Distrito distrito8 = new Distrito() { Nome = "Faro" };
            Distrito distrito9 = new Distrito() { Nome = "Guarda" };
            Distrito distrito10 = new Distrito() { Nome = "Leiria" };
            Distrito distrito11 = new Distrito() { Nome = "Lisboa" };
            Distrito distrito12 = new Distrito() { Nome = "Portalegre" };
            Distrito distrito13 = new Distrito() { Nome = "Porto" };
            Distrito distrito14 = new Distrito() { Nome = "Santarem" };
            Distrito distrito15 = new Distrito() { Nome = "Setubal" };
            Distrito distrito16 = new Distrito() { Nome = "Viana do Castelo" };
            Distrito distrito17 = new Distrito() { Nome = "Vila Real" };
            Distrito distrito18 = new Distrito() { Nome = "Viseu" };
            Distrito distrito19 = new Distrito() { Nome = "Ilha da Madeira" };
            Distrito distrito20 = new Distrito() { Nome = "Ilha do Porto Santo" };

            dbContext.Distritos.Add(distrito1);
            dbContext.Distritos.Add(distrito2);
            dbContext.Distritos.Add(distrito3);
            dbContext.Distritos.Add(distrito4);
            dbContext.Distritos.Add(distrito5);
            dbContext.Distritos.Add(distrito6);
            dbContext.Distritos.Add(distrito7);
            dbContext.Distritos.Add(distrito8);
            dbContext.Distritos.Add(distrito9);
            dbContext.Distritos.Add(distrito10);
            dbContext.Distritos.Add(distrito11);
            dbContext.Distritos.Add(distrito12);
            dbContext.Distritos.Add(distrito13);
            dbContext.Distritos.Add(distrito14);
            dbContext.Distritos.Add(distrito15);
            dbContext.Distritos.Add(distrito16);
            dbContext.Distritos.Add(distrito17);
            dbContext.Distritos.Add(distrito18);
            dbContext.Distritos.Add(distrito19);
            dbContext.Distritos.Add(distrito20);

            dbContext.SaveChanges();

         
            Distrito distrito = dbContext.Distritos.SingleOrDefault(d => d.Nome == distrito6.Nome);
            if (distrito == null)
                return;

            Concelho concelho1 = new Concelho() { Nome = "Arganil", DistritoID = distrito.ID };
            Concelho concelho2 = new Concelho() { Nome = "Cantanhede", DistritoID = distrito.ID };
            Concelho concelho3 = new Concelho() { Nome = "Coimbra", DistritoID = distrito.ID };
            Concelho concelho4 = new Concelho() { Nome = "Condexa-a-Nova", DistritoID = distrito.ID };
            Concelho concelho5 = new Concelho() { Nome = "Figueira da Foz", DistritoID = distrito.ID };
            Concelho concelho6 = new Concelho() { Nome = "Góis", DistritoID = distrito.ID };
            Concelho concelho7 = new Concelho() { Nome = "Lousã", DistritoID = distrito.ID };
            Concelho concelho8 = new Concelho() { Nome = "Mira", DistritoID = distrito.ID };
            Concelho concelho9 = new Concelho() { Nome = "Miranda do Corvo", DistritoID = distrito.ID };
            Concelho concelho10 = new Concelho() { Nome = "Montemor-o-Velho", DistritoID = distrito.ID };
            Concelho concelho11 = new Concelho() { Nome = "Oliveira do Hospital", DistritoID = distrito.ID };
            Concelho concelho12 = new Concelho() { Nome = "Pampilhosa da Serra", DistritoID = distrito.ID };
            Concelho concelho13 = new Concelho() { Nome = "Penacova", DistritoID = distrito.ID };
            Concelho concelho14 = new Concelho() { Nome = "Penela", DistritoID = distrito.ID };
            Concelho concelho15 = new Concelho() { Nome = "Soure", DistritoID = distrito.ID };
            Concelho concelho16 = new Concelho() { Nome = "Tábua", DistritoID = distrito.ID };
            Concelho concelho17 = new Concelho() { Nome = "Vila Nova de Poiares", DistritoID = distrito.ID };

            dbContext.Concelhoes.Add(concelho1);
            dbContext.Concelhoes.Add(concelho2);
            dbContext.Concelhoes.Add(concelho3);
            dbContext.Concelhoes.Add(concelho4);
            dbContext.Concelhoes.Add(concelho5);
            dbContext.Concelhoes.Add(concelho6);
            dbContext.Concelhoes.Add(concelho7);
            dbContext.Concelhoes.Add(concelho8);
            dbContext.Concelhoes.Add(concelho9);
            dbContext.Concelhoes.Add(concelho10);
            dbContext.Concelhoes.Add(concelho11);
            dbContext.Concelhoes.Add(concelho12);
            dbContext.Concelhoes.Add(concelho13);
            dbContext.Concelhoes.Add(concelho14);
            dbContext.Concelhoes.Add(concelho15);
            dbContext.Concelhoes.Add(concelho16);
            dbContext.Concelhoes.Add(concelho17);

            
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