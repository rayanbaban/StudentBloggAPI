using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;

namespace StudentBloggAPI.Mappers
{
	public class UserMapper : IMapper<User, UserDTO>
	{
		public UserDTO MapToDto(User model)
		{
			return new UserDTO(model.Id, model.UserName, model.FirstName,
				model.LastName, model.Email, model.Created);
		}

		public User MapToModel(UserDTO dto)
		{
            return new User()
			{
				Id = dto.id,
				UserName = dto.userName,
				FirstName = dto.firstName,
				LastName = dto.lastName,
				Email = dto.email,
				Created = dto.created
			};
		}
	}
}
