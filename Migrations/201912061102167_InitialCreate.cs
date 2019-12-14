namespace e_Recarga.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carregamentoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DataInicioCarregamento = c.DateTime(nullable: false),
                        DataFimCarregamento = c.DateTime(nullable: false),
                        UtilizadorID = c.String(maxLength: 128),
                        VeiculoID = c.Int(nullable: false),
                        TomadaID = c.Int(nullable: false),
                        ContaBancariaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaBancarias", t => t.ContaBancariaID, cascadeDelete: true)
                .ForeignKey("dbo.Tomadas", t => t.TomadaID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UtilizadorID)
                .ForeignKey("dbo.Veiculoes", t => t.VeiculoID, cascadeDelete: true)
                .Index(t => t.UtilizadorID)
                .Index(t => t.VeiculoID)
                .Index(t => t.TomadaID)
                .Index(t => t.ContaBancariaID);
            
            CreateTable(
                "dbo.ContaBancarias",
                c => new
                    {
                        ContaBancariaId = c.Int(nullable: false, identity: true),
                        NumeroCartao = c.String(nullable: false, maxLength: 150),
                        Validade = c.DateTime(nullable: false),
                        CVV = c.String(nullable: false, maxLength: 10),
                        Ativa = c.Boolean(nullable: false),
                        UtilizadorID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ContaBancariaId)
                .ForeignKey("dbo.AspNetUsers", t => t.UtilizadorID)
                .Index(t => t.UtilizadorID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nome = c.String(maxLength: 150),
                        Morada = c.String(maxLength: 150),
                        Concelho = c.String(maxLength: 150),
                        Freguesia = c.String(maxLength: 150),
                        CodigoPostal = c.String(maxLength: 10),
                        Localidade = c.String(maxLength: 150),
                        Telemovel = c.String(maxLength: 150),
                        Telefone = c.String(maxLength: 150),
                        NIF = c.String(maxLength: 9),
                        DataNascimento = c.DateTime(),
                        Pais = c.String(maxLength: 150),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EstacaoCarregamentoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Designacao = c.String(nullable: false, maxLength: 150),
                        Latitude = c.String(nullable: false, maxLength: 150),
                        Longitude = c.String(nullable: false, maxLength: 150),
                        CodigoPostal = c.String(nullable: false, maxLength: 10),
                        Localidade = c.String(nullable: false, maxLength: 150),
                        ConcelhoID = c.Int(nullable: false),
                        UtilizadorID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Concelhoes", t => t.ConcelhoID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UtilizadorID)
                .Index(t => t.ConcelhoID)
                .Index(t => t.UtilizadorID);
            
            CreateTable(
                "dbo.Concelhoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150),
                        DistritoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Distritoes", t => t.DistritoID, cascadeDelete: true)
                .Index(t => t.DistritoID);
            
            CreateTable(
                "dbo.Distritoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Postoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CorrenteCarregamento = c.String(nullable: false, maxLength: 150),
                        EstacaoCarregamentoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EstacaoCarregamentoes", t => t.EstacaoCarregamentoID, cascadeDelete: true)
                .Index(t => t.EstacaoCarregamentoID);
            
            CreateTable(
                "dbo.TomadaPostoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TomadaID = c.Int(nullable: false),
                        PostoID = c.Int(nullable: false),
                        PrecoMinuto = c.Double(nullable: false),
                        PotenciaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Postoes", t => t.PostoID, cascadeDelete: true)
                .ForeignKey("dbo.Potencias", t => t.PotenciaID, cascadeDelete: true)
                .ForeignKey("dbo.Tomadas", t => t.TomadaID, cascadeDelete: true)
                .Index(t => t.TomadaID)
                .Index(t => t.PostoID)
                .Index(t => t.PotenciaID);
            
            CreateTable(
                "dbo.Potencias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PotenciaNominalKw = c.String(nullable: false, maxLength: 150),
                        TipoPotencia = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reservas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DataReserva = c.DateTime(nullable: false),
                        DataPrevInicioCarregamento = c.DateTime(nullable: false),
                        DataPrevFimCarregamento = c.DateTime(nullable: false),
                        UtilizadorID = c.String(maxLength: 128),
                        CarregamentoID = c.Int(),
                        TomadaPostoID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Carregamentoes", t => t.CarregamentoID)
                .ForeignKey("dbo.TomadaPostoes", t => t.TomadaPostoID)
                .ForeignKey("dbo.AspNetUsers", t => t.UtilizadorID)
                .Index(t => t.UtilizadorID)
                .Index(t => t.CarregamentoID)
                .Index(t => t.TomadaPostoID);
            
            CreateTable(
                "dbo.Tomadas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TipoTomada = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Veiculoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Matricula = c.String(nullable: false, maxLength: 150),
                        Marca = c.String(nullable: false, maxLength: 150),
                        Modelo = c.String(nullable: false, maxLength: 150),
                        UtilizadorID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UtilizadorID)
                .Index(t => t.UtilizadorID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Carregamentoes", "VeiculoID", "dbo.Veiculoes");
            DropForeignKey("dbo.Carregamentoes", "UtilizadorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Carregamentoes", "TomadaID", "dbo.Tomadas");
            DropForeignKey("dbo.Carregamentoes", "ContaBancariaID", "dbo.ContaBancarias");
            DropForeignKey("dbo.ContaBancarias", "UtilizadorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Veiculoes", "UtilizadorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EstacaoCarregamentoes", "UtilizadorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.TomadaPostoes", "TomadaID", "dbo.Tomadas");
            DropForeignKey("dbo.Reservas", "UtilizadorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservas", "TomadaPostoID", "dbo.TomadaPostoes");
            DropForeignKey("dbo.Reservas", "CarregamentoID", "dbo.Carregamentoes");
            DropForeignKey("dbo.TomadaPostoes", "PotenciaID", "dbo.Potencias");
            DropForeignKey("dbo.TomadaPostoes", "PostoID", "dbo.Postoes");
            DropForeignKey("dbo.Postoes", "EstacaoCarregamentoID", "dbo.EstacaoCarregamentoes");
            DropForeignKey("dbo.EstacaoCarregamentoes", "ConcelhoID", "dbo.Concelhoes");
            DropForeignKey("dbo.Concelhoes", "DistritoID", "dbo.Distritoes");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Veiculoes", new[] { "UtilizadorID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Reservas", new[] { "TomadaPostoID" });
            DropIndex("dbo.Reservas", new[] { "CarregamentoID" });
            DropIndex("dbo.Reservas", new[] { "UtilizadorID" });
            DropIndex("dbo.TomadaPostoes", new[] { "PotenciaID" });
            DropIndex("dbo.TomadaPostoes", new[] { "PostoID" });
            DropIndex("dbo.TomadaPostoes", new[] { "TomadaID" });
            DropIndex("dbo.Postoes", new[] { "EstacaoCarregamentoID" });
            DropIndex("dbo.Concelhoes", new[] { "DistritoID" });
            DropIndex("dbo.EstacaoCarregamentoes", new[] { "UtilizadorID" });
            DropIndex("dbo.EstacaoCarregamentoes", new[] { "ConcelhoID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ContaBancarias", new[] { "UtilizadorID" });
            DropIndex("dbo.Carregamentoes", new[] { "ContaBancariaID" });
            DropIndex("dbo.Carregamentoes", new[] { "TomadaID" });
            DropIndex("dbo.Carregamentoes", new[] { "VeiculoID" });
            DropIndex("dbo.Carregamentoes", new[] { "UtilizadorID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Veiculoes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Tomadas");
            DropTable("dbo.Reservas");
            DropTable("dbo.Potencias");
            DropTable("dbo.TomadaPostoes");
            DropTable("dbo.Postoes");
            DropTable("dbo.Distritoes");
            DropTable("dbo.Concelhoes");
            DropTable("dbo.EstacaoCarregamentoes");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ContaBancarias");
            DropTable("dbo.Carregamentoes");
        }
    }
}
