using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentBloggAPI.Models.Entities
{
	public class Post
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("UserId")]
		public int UserId { get; set; }

		[Required]
		[MaxLength(30)]
		public string Title { get; set; } = string.Empty;

		[Required]
		public string Content { get; set; } = string.Empty;

		[Required]
		public DateTime Created { get; set; }

		[Required]
		public DateTime Updated { get; set; }
		[Required]


		//Navigation properties
		public virtual User? User { get; set; }
		public virtual ICollection<Comment> Comments { get; set;} = new HashSet<Comment>();
	}
}
