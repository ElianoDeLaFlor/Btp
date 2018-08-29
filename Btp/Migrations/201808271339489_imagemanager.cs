namespace Btp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imagemanager : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Chemin = c.String(maxLength: 120),
                        Description = c.String(maxLength: 256),
                        Title = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImageManagers");
        }
    }
}
