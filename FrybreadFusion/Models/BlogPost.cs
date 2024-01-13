namespace FrybreadFusion.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Name { get; set; }
        public DateTime DatePosted { get; set; }
        public List<Rating> Ratings { get; set; } = new List<Rating>();
    }
    public class Rating
    {
        public int Id { get; set; }
        public int Rate { get; set; } 
        public int BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; }
    }
}