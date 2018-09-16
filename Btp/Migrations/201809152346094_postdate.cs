namespace Btp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Postulers", "PostTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Postulers", "PostTime");
        }
    }
}
