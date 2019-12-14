namespace e_Recarga.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitalCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservas", "Cancelada", c => c.Boolean(nullable: false));
            DropColumn("dbo.Reservas", "Eliminada");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservas", "Eliminada", c => c.Boolean(nullable: false));
            DropColumn("dbo.Reservas", "Cancelada");
        }
    }
}
