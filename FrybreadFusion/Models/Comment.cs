using System.ComponentModel.DataAnnotations;

namespace FrybreadFusion.Models


    
{
    public class Comment
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please enter your name.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? UserName { get; set; }
        
        [Required(ErrorMessage = "Please enter a comment.")]
        [StringLength(500, ErrorMessage = "Comment cannot be longer than 500 characters.")]
        public string? UserComment { get; set; } 
        
        public DateTime DatePosted { get; set; }

      
        public int BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; } 
        public List<Reply> Replies { get; set; } = new List<Reply>();
    }
}
