using Microsoft.AspNet.Identity;
using proekt_turizam.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace proekt_turizam.Controllers
{
    public class SavedToursController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SavedTours
        public ActionResult Index()
        {
            var savedTours = db.SavedTours.Include(s => s.Tour).OrderByDescending(t => t.TourId);
            return View(savedTours.ToList());
        }

        // GET: SavedTours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedTour savedTour = db.SavedTours.Find(id);
            if (savedTour == null)
            {
                return HttpNotFound();
            }
            return View(savedTour);
        }

        // GET: SavedTours/Create
        public ActionResult Create(int id)
        {
            var tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }

            var saved = new SavedTour
            {
                TourId = id,
                TouristId = User.Identity.GetUserId()
                
            };

            ViewBag.TourTitle = tour.Title;
            ViewBag.TourCity = tour.City;
            ViewBag.TourRegion = tour.Region;
            ViewBag.TourPrice = tour.Price;


            return View(saved);
        }

        // POST: SavedTours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SavedTour savedTour)
        {
            var userId = User.Identity.GetUserId();
            savedTour.TouristId = userId;

            bool alreadySaved = db.SavedTours.Any(b => b.TourId == savedTour.TourId && b.TouristId == savedTour.TouristId);

            if (alreadySaved)
            {
               
                ModelState.AddModelError("", "You have already saved this tour.");

                
                var tour = db.Tours.Find(savedTour.TourId);
                if (tour != null)
                {
                    ViewBag.TourTitle = tour.Title;
                    ViewBag.TourCity = tour.City;
                    ViewBag.TourRegion = tour.Region;
                    ViewBag.TourPrice = tour.Price;
                }

                return View(savedTour);
            }
            if (ModelState.IsValid)
            {
                savedTour.SavedOn = DateTime.Now;
                db.SavedTours.Add(savedTour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TourId = new SelectList(db.Tours, "TourId", "Title", savedTour.TourId);
            return View(savedTour);
        }

        // GET: SavedTours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedTour savedTour = db.SavedTours.Find(id);
            if (savedTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.TourId = new SelectList(db.Tours, "TourId", "Title", savedTour.TourId);
            return View(savedTour);
        }

        // POST: SavedTours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SavedTourId,UserId,TourId,SavedOn")] SavedTour savedTour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(savedTour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TourId = new SelectList(db.Tours, "TourId", "Title", savedTour.TourId);
            return View(savedTour);
        }

        // GET: SavedTours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedTour savedTour = db.SavedTours.Find(id);
            if (savedTour == null)
            {
                return HttpNotFound();
            }
            return View(savedTour);
        }

        // POST: SavedTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SavedTour savedTour = db.SavedTours.Find(id);
            db.SavedTours.Remove(savedTour);
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
