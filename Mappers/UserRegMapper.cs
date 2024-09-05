using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;

namespace StudentBloggAPI.Mappers;

public class UserRegMapper : IMapper<User, UserRegistrationDTO>
{
	public UserRegistrationDTO MapToDto(User model)
	{
		throw new Exception();
	}

	public User MapToModel(UserRegistrationDTO dto)
	{
		var dtNow = DateTime.Now;
		return new User()
		{
			//Email = dto.Email,
			UserName = dto.UserName,
			FirstName = dto.FirstName,
			LastName = dto.LastName,
			IsAdminUser = true,
			Created = dtNow,
			Updated = dtNow
		};
	}
}
