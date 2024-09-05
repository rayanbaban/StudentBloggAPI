using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;

namespace StudentBloggAPI.Mappers
{
	public class PostMapper : IMapper<Post, PostDTO>
	{
		public PostDTO MapToDto(Post model)
		{
			return new PostDTO(model.Id, model.UserId, model.Title, model.Content, model.Created);
		}

		public Post MapToModel(PostDTO dto)
		{
			var dtNow = DateTime.Now;
			return new Post() 
			{
				Id = dto.PostId,
				UserId = dto.UserId,
				Title = dto.Title,
				Content = dto.Content,
				Created = dtNow,
				Updated = dtNow,

			};
		}
	}
}
