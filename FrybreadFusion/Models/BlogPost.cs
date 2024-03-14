using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace FrybreadFusion.Models
{
    public class BlogPost
    {
        public BlogPost()
        {

            DatePosted = DateTime.Now;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide a title for the blog post.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The title must be between 3 and 100 characters.")]
        public string? Title { get; set; }
        
        [Required(ErrorMessage = "Please provide text for the blog post.")]
        public string? Text { get; set; }
        
        [Required(ErrorMessage = "Please provide your name.")]
        public string? Name { get; set; }
        public DateTime DatePosted { get; set; }
        public List<Rating> Ratings { get; set; } = new List<Rating>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
    public class Rating
    {
        public int Id { get; set; }
        
        [Range(1, 5, ErrorMessage = "Please rate between 1 and 5.")]
        public int Rate { get; set; } 
        
        public int BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; }
    }
}