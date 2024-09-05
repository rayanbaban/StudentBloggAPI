using System.ComponentModel.DataAnnotations;

namespace StudentBloggAPI.Models.DTOs
{
	public class CommentDTO
	{
		public CommentDTO(int commentId, int postId, int userId, string content, DateTime dateCommented, DateTime updates)
		{
			CommentId = commentId;
			PostId = postId;
			UserId = userId;
			Content = content;
			DateCommented = dateCommented;
			Updates = updates;
		}
		

		public int CommentId { get; set; }
		public int PostId { get; set; }
		public int UserId { get; set; }

		[Required]
		[MinLength(5, ErrorMessage = "Content må ha minst 5 tegn eller flere")]
		public string Content { get; init; } = string.Empty;

		public DateTime DateCommented { get; init; }
		public DateTime Updates { get; init; }

	}
}
