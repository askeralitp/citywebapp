namespace CitiesApp.Asp.NetWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Druga : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false, maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false, maxLength: 40));
        }
    }
}
