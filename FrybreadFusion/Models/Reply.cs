using FrybreadFusion.Models;

public class Reply
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime DatePosted { get; set; }
    public string UserName { get; set; }  

  
    public int CommentId { get; set; }
    public Comment Comment { get; set; } 
}
