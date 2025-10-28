using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using proekt_turizam.Models;

namespace proekt_turizam.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager;

        public AdminController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // Show all unapproved Tour Guides
        public ActionResult PendingTourGuides()
        {
            var pendingGuides = userManager.Users
                .Where(u => u.IsApproved == false)
                .ToList();
            return View(pendingGuides);
        }

        // Approve a guide by ID
        public async Task<ActionResult> Approve(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.IsApproved = true;
                await userManager.UpdateAsync(user);
            }

            return RedirectToAction("PendingTourGuides");
        }
    }
}
