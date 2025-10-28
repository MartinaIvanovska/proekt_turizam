namespace proekt_turizam.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using proekt_turizam.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<proekt_turizam.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(proekt_turizam.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Create roles if they don’t exist
            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole("Admin"));

            if (!roleManager.RoleExists("TourGuide"))
                roleManager.Create(new IdentityRole("TourGuide"));

            if (!roleManager.RoleExists("Tourist"))
                roleManager.Create(new IdentityRole("Tourist"));

            // Example: Assign a user to the Admin role
            var user = userManager.FindByEmail("admin@site.com");
            if (user != null && !userManager.IsInRole(user.Id, "Admin"))
                userManager.AddToRole(user.Id, "Admin");


            var user_tour_guide = userManager.FindByEmail("tour_guide@site.com");
            if (user_tour_guide != null && !userManager.IsInRole(user_tour_guide.Id, "TourGuide"))
                userManager.AddToRole(user_tour_guide.Id, "TourGuide");

            var user_tourist = userManager.FindByEmail("tourist@site.com");
            if (user_tourist != null && !userManager.IsInRole(user_tourist.Id, "Tourist"))
                userManager.AddToRole(user_tourist.Id, "Tourist");
        }
    }
}
