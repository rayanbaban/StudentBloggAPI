using System.ComponentModel.DataAnnotations;

namespace StudentBloggAPI.Models.Entities;

public class User
{
	[Key]
	public int Id { get; set; }
	
	[Required]
	[MinLength(3), MaxLength(30)]
	public string UserName { get; set; } = string.Empty;

	[Required]
	public string FirstName { get; set; } = string.Empty;

	[Required]
	public string LastName { get; set; } = string.Empty;
	[EmailAddress]
	public string Email { get; set; } = string.Empty;

	public string HashedPassword { get; set; } = string.Empty;
	public string Salt { get; set; } = string.Empty;

	[Required]
	public DateTime Created { get; set; }

	[Required]
	public DateTime Updated { get; set; }

	[Required]
	public bool IsAdminUser { get; set; }

	//Navigation Properties
	public virtual ICollection<Post> Posts { get; set;} = new HashSet<Post>();
	public virtual ICollection<Comment> Comments { get; set;} = new HashSet<Comment>();
	
}
