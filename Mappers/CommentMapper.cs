using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;

namespace StudentBloggAPI.Mappers
{
    public class CommentMapper : IMapper<Comment, CommentDTO>
	{
		public CommentDTO MapToDto(Comment model)
		{
			return new CommentDTO(
			model.Id, model.PostId, model.UserId, model.Content, model.DateCommented, model.Updates);
		}

		public Comment MapToModel(CommentDTO dto)
		{
			return new Comment
			{
				Id = dto.CommentId,
				PostId = dto.PostId,
				UserId = dto.UserId,
				Content = dto.Content,
				DateCommented = dto.DateCommented,
				Updates = dto.Updates
			};
		}
	}
}
