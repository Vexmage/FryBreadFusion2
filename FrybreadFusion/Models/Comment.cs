namespace FrybreadFusion.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserComment { get; set; } // If this is supposed to be the main content of the comment
        public DateTime DatePosted { get; set; }

        // Add these properties if they are required
        public int BlogPostId { get; set; } // Foreign key to the BlogPost
        public BlogPost BlogPost { get; set; } // Navigation property
    }
}
