namespace Btp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recrutement_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recrutements", "DateExpiration", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recrutements", "DateExpiration");
        }
    }
}
