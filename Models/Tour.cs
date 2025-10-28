using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace proekt_turizam.Models
{
	public class Tour
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TourId { get; set; }

        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int uselsess { get; set; }     
        public TourType TourType { get; set; }   
        public string City { get; set; }
        public string Region { get; set; }
        public string MainImageUrl { get; set; }

        public decimal Price { get; set; }

        // Dates
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Foreign key
        public string TourGuideId { get; set; }

        [ForeignKey("TourGuideId")]
        public virtual ApplicationUser TourGuide { get; set; }

        // Navigation
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<TourImage> Images { get; set; }
        public virtual ICollection<TourDate> AvailableDates { get; set; }
    }
    public enum TourType
    {
        Hiking,
        Cycling,
        Cruising,
        Cultural,
        Adventure,
        Other
    }
}