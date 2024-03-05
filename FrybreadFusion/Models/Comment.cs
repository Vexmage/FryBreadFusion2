namespace FrybreadFusion.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserComment { get; set; } 
        public DateTime DatePosted { get; set; }

      
        public int BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; } 
        public List<Reply> Replies { get; set; } = new List<Reply>();
    }
}
