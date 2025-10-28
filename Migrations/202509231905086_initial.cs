namespace proekt_turizam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        BookingDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        TourId = c.Int(nullable: false),
                        TouristId = c.Int(nullable: false),
                        Tourist_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Tourist_Id)
                .Index(t => t.TourId)
                .Index(t => t.Tourist_Id);
            
            CreateTable(
                "dbo.Tours",
                c => new
                    {
                        TourId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        TourType = c.Int(nullable: false),
                        City = c.String(),
                        Region = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TourGuideId = c.Int(nullable: false),
                        TourGuide_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TourId)
                .ForeignKey("dbo.AspNetUsers", t => t.TourGuide_Id)
                .Index(t => t.TourGuide_Id);
            
            CreateTable(
                "dbo.TourDates",
                c => new
                    {
                        TourDateId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        MaxParticipants = c.Int(nullable: false),
                        TourId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TourDateId)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.TourImages",
                c => new
                    {
                        TourImageId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        TourId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TourImageId)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Comment = c.String(),
                        ReviewDate = c.DateTime(nullable: false),
                        TourId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.TourId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Role = c.Int(nullable: false),
                        IsApproved = c.Boolean(),
                        Bio = c.String(),
                        Experience = c.String(),
                        CompanyName = c.String(),
                        ContactInfo = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SavedTours",
                c => new
                    {
                        SavedTourId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TourId = c.Int(nullable: false),
                        SavedOn = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SavedTourId)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.TourId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Tours", "TourGuide_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SavedTours", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SavedTours", "TourId", "dbo.Tours");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "Tourist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "TourId", "dbo.Tours");
            DropForeignKey("dbo.TourImages", "TourId", "dbo.Tours");
            DropForeignKey("dbo.Bookings", "TourId", "dbo.Tours");
            DropForeignKey("dbo.TourDates", "TourId", "dbo.Tours");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SavedTours", new[] { "User_Id" });
            DropIndex("dbo.SavedTours", new[] { "TourId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            DropIndex("dbo.Reviews", new[] { "TourId" });
            DropIndex("dbo.TourImages", new[] { "TourId" });
            DropIndex("dbo.TourDates", new[] { "TourId" });
            DropIndex("dbo.Tours", new[] { "TourGuide_Id" });
            DropIndex("dbo.Bookings", new[] { "Tourist_Id" });
            DropIndex("dbo.Bookings", new[] { "TourId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.SavedTours");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Reviews");
            DropTable("dbo.TourImages");
            DropTable("dbo.TourDates");
            DropTable("dbo.Tours");
            DropTable("dbo.Bookings");
        }
    }
}
