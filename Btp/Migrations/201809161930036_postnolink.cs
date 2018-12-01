namespace Btp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postnolink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Postulers", "RecrutementId", "dbo.Recrutements");
            DropIndex("dbo.Postulers", new[] { "RecrutementId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Postulers", "RecrutementId");
            AddForeignKey("dbo.Postulers", "RecrutementId", "dbo.Recrutements", "Id", cascadeDelete: true);
        }
    }
}
