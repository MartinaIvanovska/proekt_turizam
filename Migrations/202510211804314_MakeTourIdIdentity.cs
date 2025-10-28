namespace proekt_turizam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeTourIdIdentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "uselsess", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "uselsess");
        }
    }
}
