using System.ComponentModel.DataAnnotations;

using FrybreadFusion.Models;

public class Reply
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Please enter a reply.")]
    [StringLength(500, ErrorMessage = "Reply cannot be longer than 500 characters.")]
    public string Text { get; set; }
    public DateTime DatePosted { get; set; }
    [Required(ErrorMessage = "Please enter your name.")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string UserName { get; set; }  

  
    public int CommentId { get; set; }
    public Comment Comment { get; set; } 
}
