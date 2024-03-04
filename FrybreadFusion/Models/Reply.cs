using FrybreadFusion.Models;

public class Reply
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime DatePosted { get; set; }
    public string UserName { get; set; }  // The user who posted the reply

    // Foreign key for the associated comment
    public int CommentId { get; set; }
    public Comment Comment { get; set; } // Navigation property
}
