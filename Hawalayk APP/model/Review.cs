﻿using System.ComponentModel.DataAnnotations;

namespace Hawalayk_APP.model
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Rating { get; set; }
        public string? Headline { get; set; }
        public string? Content { get; set; }
        public int? PositiveReacts { get; set; }
        public int? NegativeReacts { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
