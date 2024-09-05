using System.ComponentModel.DataAnnotations;

namespace StudentBloggAPI.Models.DTOs
{
	public class PostDTO
	{
		public PostDTO(int postId, int userId, string title, string content, DateTime datePosted)
		{
			PostId = postId;
			UserId = userId;
			Title = title;
			Content = content;
			DatePosted = datePosted;
		}

		public int PostId { get; set; }
		public int UserId { get; set; }
			
		[Required]
		[MinLength(1, ErrorMessage = "Tittel må ha minst 1 tegn")]
		public string Title { get; init; } = string.Empty;

		[Required]
		[MinLength(1, ErrorMessage = "Content må ha minst lengde på 1 tegn")]
		public string Content { get; init; } = string.Empty;

		public DateTime DatePosted { get; init; }
	}
}
