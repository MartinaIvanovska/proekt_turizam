using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace proekt_turizam.Models
{
	public class Review
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        public int Rating { get; set; } // 1-5
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        // Foreign keys
        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }

        public string TouristId { get; set; }

        [ForeignKey("TouristId")]
        public virtual ApplicationUser Tourist { get; set; }
    }
}