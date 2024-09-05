using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentBloggAPI.Models.Entities;

public class Comment
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("PostId")]
    public int PostId { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    public DateTime DateCommented { get; set; }

    [Required]
    public DateTime Updates { get; set; }
	[Required]
	public bool IsAdminUser { get; set; }


	//Navigation properties
	public virtual Post? Post { get; set; }
    public virtual User? User { get; set; }
}
