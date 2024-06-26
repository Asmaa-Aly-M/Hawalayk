﻿using Hawalayk_APP.Enums;
using System.ComponentModel.DataAnnotations.Schema;


namespace Hawalayk_APP.Models
{

    public class Craftsman : ApplicationUser
    {
        public string PersonalImage { get; set; }
        public string NationalIDImage { get; set; }
        public DateTime ProfilePicLastUpdated { get; set; } = DateTime.UtcNow;

        [ForeignKey("Craft")]
        public int? CraftId { get; set; }
        public Craft? Craft { get; set; }

        public double Rating { get; set; }
        public ICollection<Post> Portfolio { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<JobApplication> JobApplications { get; set; }
        public CraftsmanRegistrationStatus RegistrationStatus { get; set; }

        public double GetRating()
        {
            if(Reviews == null)
            {
                return 0;
            }
            else
            {
                var overallRating = Reviews.Average(r => (double)(r.Rating));
                return overallRating;
            }
        }
    }
}
