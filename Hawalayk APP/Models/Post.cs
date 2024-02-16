﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Hawalayk_APP.Models
{
    public class Post
    {
        public int Id { get; set; }
       
        [ForeignKey("Image")]
        public int ImageId { get; set; }
        public Image Image { get; set; }

        
        [ForeignKey("Craftsman")]
        public int CraftsmanId { get; set; }
        public Craftsman Craftsman { get; set; }
        public string? Content { get; set; }
    }
}
