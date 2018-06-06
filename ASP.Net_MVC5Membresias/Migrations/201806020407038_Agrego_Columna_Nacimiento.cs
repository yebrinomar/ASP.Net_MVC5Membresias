namespace ASP.Net_MVC5Membresias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agrego_Columna_Nacimiento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LugarDeNacimiento", c => c.String(maxLength: 120));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LugarDeNacimiento");
        }
    }
}
