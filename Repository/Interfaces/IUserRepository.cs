using StudentBloggAPI.Models.Entities;

namespace StudentBloggAPI.Repository.Interfaces
{
	// CRUD - create - read - update - delete
	public interface IUserRepository 
	{
		//create
		Task<User?> AddUserAsync(User user);

		// update 
		Task<User?> UpdateUserAsync(int id,User user);
		//delete
		Task<User?> DeleteUserByIdAsync(int id);
		//read
		Task<ICollection<User>> GetUsersAsync();

		Task<User?> GetUserByIdAsync(int id);

		Task<User?> GetUserByNameAsync(string name);

		Task <ICollection<User>> GetPageAsync(int pageNr, int pageSize);


	}
}
