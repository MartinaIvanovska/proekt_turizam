namespace proekt_turizam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoOfBookings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "NumberOfPeople", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "NumberOfPeople");
        }
    }
}
