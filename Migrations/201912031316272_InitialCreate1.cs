namespace e_Recarga.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Reservas", name: "PostoID", newName: "Posto_ID");
            RenameIndex(table: "dbo.Reservas", name: "IX_PostoID", newName: "IX_Posto_ID");
            AddColumn("dbo.Reservas", "TomadaPostoID", c => c.Int());
            CreateIndex("dbo.Reservas", "TomadaPostoID");
            AddForeignKey("dbo.Reservas", "TomadaPostoID", "dbo.TomadaPostoes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "TomadaPostoID", "dbo.TomadaPostoes");
            DropIndex("dbo.Reservas", new[] { "TomadaPostoID" });
            DropColumn("dbo.Reservas", "TomadaPostoID");
            RenameIndex(table: "dbo.Reservas", name: "IX_Posto_ID", newName: "IX_PostoID");
            RenameColumn(table: "dbo.Reservas", name: "Posto_ID", newName: "PostoID");
        }
    }
}
