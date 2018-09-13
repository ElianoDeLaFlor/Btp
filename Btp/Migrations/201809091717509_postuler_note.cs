namespace Btp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postuler_note : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Postulers", "Note");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Postulers", "Note", c => c.String());
        }
    }
}
