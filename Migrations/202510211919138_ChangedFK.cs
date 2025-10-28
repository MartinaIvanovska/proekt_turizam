namespace proekt_turizam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedFK : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Bookings", new[] { "Tourist_Id" });
            DropIndex("dbo.Tours", new[] { "TourGuide_Id" });
            DropColumn("dbo.Bookings", "TouristId");
            DropColumn("dbo.Tours", "TourGuideId");
            RenameColumn(table: "dbo.Bookings", name: "Tourist_Id", newName: "TouristId");
            RenameColumn(table: "dbo.Tours", name: "TourGuide_Id", newName: "TourGuideId");
            RenameColumn(table: "dbo.Reviews", name: "User_Id", newName: "TouristId");
            RenameColumn(table: "dbo.SavedTours", name: "User_Id", newName: "TouristId");
            RenameIndex(table: "dbo.Reviews", name: "IX_User_Id", newName: "IX_TouristId");
            RenameIndex(table: "dbo.SavedTours", name: "IX_User_Id", newName: "IX_TouristId");
            AlterColumn("dbo.Bookings", "TouristId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Tours", "TourGuideId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Bookings", "TouristId");
            CreateIndex("dbo.Tours", "TourGuideId");
            DropColumn("dbo.Reviews", "UserId");
            DropColumn("dbo.SavedTours", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SavedTours", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Reviews", "UserId", c => c.Int(nullable: false));
            DropIndex("dbo.Tours", new[] { "TourGuideId" });
            DropIndex("dbo.Bookings", new[] { "TouristId" });
            AlterColumn("dbo.Tours", "TourGuideId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bookings", "TouristId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.SavedTours", name: "IX_TouristId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.Reviews", name: "IX_TouristId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.SavedTours", name: "TouristId", newName: "User_Id");
            RenameColumn(table: "dbo.Reviews", name: "TouristId", newName: "User_Id");
            RenameColumn(table: "dbo.Tours", name: "TourGuideId", newName: "TourGuide_Id");
            RenameColumn(table: "dbo.Bookings", name: "TouristId", newName: "Tourist_Id");
            AddColumn("dbo.Tours", "TourGuideId", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "TouristId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tours", "TourGuide_Id");
            CreateIndex("dbo.Bookings", "Tourist_Id");
        }
    }
}
