namespace FrybreadFusion.Models
{
    public class BlogViewModel
    {
        public BlogPost NewPost { get; set; } = new BlogPost();
        public List<BlogPost> Posts { get; set; } = new List<BlogPost>();
    }
}
