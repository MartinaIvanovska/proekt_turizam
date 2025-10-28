using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace proekt_turizam.Models
{
	public class TourImage
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TourImageId { get; set; }
        public string ImageUrl { get; set; }

        // Foreign key
        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }
    }
}