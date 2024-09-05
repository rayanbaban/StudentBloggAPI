using System.ComponentModel.DataAnnotations;

namespace StudentBloggAPI.Models.DTOs
{
	//public record UserRegistrationDTO(
	//	string UserName,
	//	string FirstName,
	//	string LastName,
	//	string Password,
	//	string Email
	//	);


	public class UserRegistrationDTO
	{
		//[Required(ErrorMessage = "UserName må være med!")]
		//[MinLength(3, ErrorMessage = "UserName må ha minst 3 tegn!")]
		public string UserName { get; init; } = string.Empty;

		//[Required(ErrorMessage = "FirstName må være med!")]
		public string FirstName { get; init; } = string.Empty;

		//[Required(ErrorMessage = "LastName må være med!")]
		public string LastName { get; init; } = string.Empty;

		//[Required(ErrorMessage = "Passord må være med!")]
		public string Password { get; init; } = string.Empty;

		//[EmailAddress(ErrorMessage = "Email må være med!")]
		//public string Email { get; init; } = string.Empty;
	}
}
