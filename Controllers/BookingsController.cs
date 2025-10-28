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
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            // Get all bookings of this user and include the related Tour
            var bookings = db.Bookings
                .Include(b => b.Tour)
                .Where(b => b.TouristId == userId || b.Tour.TourGuideId == userId)
                .ToList();

            

            return View(bookings);
            //var bookings = db.Bookings.Include(b => b.Tour);
            //return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create(int id)
        {
            var tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }

            var booking = new Booking
            {
                TourId = id,
                BookingDate = DateTime.Now,
                TouristId = User.Identity.GetUserId(),
                Status = BookingStatus.Pending
            };

            ViewBag.TourTitle = tour.Title;
            ViewBag.TourCity = tour.City;
            ViewBag.TourRegion = tour.Region;
            ViewBag.TourPrice = tour.Price;

            return View(booking);
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Booking booking)
        {
            var userId = User.Identity.GetUserId();
            booking.TouristId = userId;

            bool alreadyBooked = db.Bookings.Any(b => b.TourId == booking.TourId && b.TouristId == userId);
            
            if (alreadyBooked)
            {
                // Option 1: Add a validation message and redisplay the page
                ModelState.AddModelError("", "You have already booked this tour.");

                // Re-display tour info if needed
                var tour = db.Tours.Find(booking.TourId);
                if (tour != null)
                {
                    ViewBag.TourTitle = tour.Title;
                    ViewBag.TourCity = tour.City;
                    ViewBag.TourRegion = tour.Region;
                    ViewBag.TourPrice = tour.Price;
                }

                return View(booking);
            }

            if (ModelState.IsValid)
            {
                booking.BookingDate = DateTime.Now;

                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TourId = new SelectList(db.Tours, "TourId", "Title", booking.TourId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.TourId = new SelectList(db.Tours, "TourId", "Title", booking.TourId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,BookingDate,Status,TourId,TouristId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TourId = new SelectList(db.Tours, "TourId", "Title", booking.TourId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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

        [HttpGet]
        [Authorize(Roles = "TourGuide")]
        public ActionResult ChangeStatus(int id, BookingStatus status)
        {
            var booking = db.Bookings.Include(b => b.Tour).FirstOrDefault(b => b.BookingId == id);
            if (booking == null)
                return HttpNotFound();

            // Only allow the tour guide who owns the tour to change status
            if (booking.Tour.TourGuideId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);

            // Validate status
            if (status != BookingStatus.Confirmed && status != BookingStatus.Cancelled)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            booking.Status = status;
            db.SaveChanges();

            return RedirectToAction("Index"); // reload the bookings page
        }


    }
}
