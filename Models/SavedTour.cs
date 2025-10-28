using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace proekt_turizam.Models
{
	public class SavedTour
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SavedTourId { get; set; }

        public string TouristId { get; set; }

        [ForeignKey("TouristId")]
        public virtual ApplicationUser Tourist { get; set; }

        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }

        public DateTime SavedOn { get; set; }
    }
}