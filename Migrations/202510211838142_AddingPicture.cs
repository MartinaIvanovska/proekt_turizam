namespace proekt_turizam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "MainImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "MainImageUrl");
        }
    }
}
