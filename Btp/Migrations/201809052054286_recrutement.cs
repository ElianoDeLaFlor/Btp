namespace Btp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recrutement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recrutements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Post = c.String(maxLength: 90),
                        DatePublication = c.DateTime(nullable: false),
                        DateExpiration = c.DateTime(nullable: false),
                        Niveau = c.String(maxLength: 90),
                        LieuDepot = c.String(maxLength: 90),
                        Type = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Recrutements");
        }
    }
}
