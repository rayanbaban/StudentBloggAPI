using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Services.Interfaces;

public interface IUserService
{

	// CRUD: create - read - update - delete

	//Read
	Task <IEnumerable<UserDTO>> GetAllUsersAsync();

	Task<UserDTO?> GetUserByIdAsync( int id);

	//Update
	Task<UserDTO?> UpdateUserAsync(int id, UserDTO userDTO, int loginUserId);
	
	//Delete
	Task<UserDTO?> DeleteUserAsync(int id, int loginUserId);

	Task<int?> GetAuthenticatedIdAsync(string userName, string password);

	Task<UserDTO?> RegisterAsync(UserRegistrationDTO userDTO);

	Task<IEnumerable<UserDTO>>GetPageAsync(int pageNr, int pageSize);

}
