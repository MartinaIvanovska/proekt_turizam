using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace proekt_turizam.Models
{
	public class Booking
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        public DateTime BookingDate { get; set; }

        public BookingStatus Status { get; set; }

        public int NumberOfPeople { get; set; }

        // Foreign keys
        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }

        public string TouristId { get; set; }

        [ForeignKey("TouristId")]
        public virtual ApplicationUser Tourist { get; set; }
    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }
}