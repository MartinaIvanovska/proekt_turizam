using Microsoft.AspNet.Identity;
using proekt_turizam.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace proekt_turizam.Controllers
{
    public class ToursController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tours
        public ActionResult Index(string id)
        {
            var userId = User.Identity.GetUserId();
            var tours = db.Tours
              .Include(t => t.TourGuide)
              .Where(t => t.TourGuideId == userId) // string comparison
              .OrderByDescending(t => t.TourId)        // orders by Tour Id
              .ToList();

            return View(tours);
        }


        // GET: Tours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: Tours/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
    [Bind(Include = "Title,ShortDescription,Description,TourType,City,Region,Price,StartDate,EndDate,TourGuideId")] Tour tour,
    HttpPostedFileBase MainImage)  
        {
            if (ModelState.IsValid)
            {
                if (MainImage != null && MainImage.ContentLength > 0)
                {
                    
                    var uploadDir = Server.MapPath("~/Uploads");
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                    var path = Path.Combine(uploadDir, fileName);

                    
                    MainImage.SaveAs(path);

                    tour.MainImageUrl = "/Uploads/" + fileName;
                }
                tour.TourGuideId = User.Identity.GetUserId();
                tour.TourGuide = db.Users.Find(tour.TourGuideId);

                db.Tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("../Home/Index");
            }

            return View(tour);
        }

        // GET: Tours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }

            return View(tour);
        }

        // POST: Tours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TourId,Title,ShortDescription,Description,TourType,City,Region,Price,StartDate,EndDate,TourGuideId")] Tour tour,
    HttpPostedFileBase MainImage)
        {
            if (ModelState.IsValid)
            {
                // Get the original tour from DB
                var existingTour = db.Tours.AsNoTracking().FirstOrDefault(t => t.TourId == tour.TourId);
                
                    // ✅ Keep old image if no new one uploaded
                tour.MainImageUrl = existingTour.MainImageUrl;
                tour.TourGuideId = existingTour.TourGuideId;

                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tour);
        }

        // GET: Tours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tour tour = db.Tours.Find(id);
            db.Tours.Remove(tour);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
