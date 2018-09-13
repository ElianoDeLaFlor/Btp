namespace Btp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users : DbMigration
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
            
            CreateTable(
                "dbo.Postulers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecrutementId = c.Int(nullable: false),
                        Nom = c.String(maxLength: 90),
                        Prenom = c.String(maxLength: 120),
                        CheminCv = c.String(),
                        Lettre = c.String(),
                        Attestation = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recrutements", t => t.RecrutementId, cascadeDelete: true)
                .Index(t => t.RecrutementId);
            
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
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 30),
                        Login = c.String(nullable: false, maxLength: 15),
                        UserRole = c.Int(nullable: false),
                        Password = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Postulers", "RecrutementId", "dbo.Recrutements");
            DropIndex("dbo.Postulers", new[] { "RecrutementId" });
            DropTable("dbo.Users");
            DropTable("dbo.Recrutements");
            DropTable("dbo.Postulers");
            DropTable("dbo.ImageManagers");
        }
    }
}
