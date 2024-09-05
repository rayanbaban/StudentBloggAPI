using Microsoft.EntityFrameworkCore;
using StudentBloggAPI.Data;
using StudentBloggAPI.Data.MigratonsNy;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;

namespace StudentBloggAPI.Repository;

public class UserRepositoryDbContext : IUserRepository
{
	private readonly StudentBloggDbContextNy _dbContext;
	private readonly ILogger<UserRepositoryDbContext> _logger;

	public UserRepositoryDbContext(StudentBloggDbContextNy dbContext, ILogger<UserRepositoryDbContext> logger)
    {
        _dbContext = dbContext;
		_logger = logger;
    }
    public async Task<User?> AddUserAsync(User user)
	{
		_logger?.LogDebug("Legger til en ny bruker med navn: {@user} ", user);
		_dbContext.Users.Add(user);
		await _dbContext.SaveChangesAsync();

		return user;
	}

	public async Task<User?> DeleteUserByIdAsync(int id)
	{
		_logger?.LogDebug("Sletter bruker med id: {@id}", id);
		var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

		if (user == null)
			return null;

		await _dbContext.Users.Where(x => x.Id == id)
			.ExecuteDeleteAsync();

		_dbContext.SaveChanges();

		return user;
	}

	public async Task<ICollection<User>> GetPageAsync(int pageNr, int pageSize)
	{
		var totCount = _dbContext.Users.Count();
		var totPages = (int)Math.Ceiling((double)totCount / pageSize);

		return await _dbContext.Users
			.OrderBy(x => x.Id)
			.Skip((pageNr - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();
	}

	public async Task<User?> GetUserByIdAsync(int id)
	{
		var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

		return user;

	}
	public async Task<User?> GetUserByNameAsync(string name)
	{
		var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName.Equals(name));

		return user;
	}

	public async Task<ICollection<User>> GetUsersAsync()
	{
		return await _dbContext.Users.ToListAsync();
	}

	public async Task<User?> UpdateUserAsync(int id, User user)
	{
		_logger?.LogDebug("Oppdaterer bruker {@user}", user);
		var updateUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
		if (updateUser == null) return null;

		updateUser.UserName = string.IsNullOrEmpty(user.UserName) ? updateUser.UserName : user.UserName;
		updateUser.FirstName = string.IsNullOrEmpty(user.FirstName) ? updateUser.UserName : user.FirstName;
		updateUser.LastName = string.IsNullOrEmpty(user.LastName) ? updateUser.LastName : user.LastName;
		updateUser.Email = string.IsNullOrEmpty(user.Email) ? updateUser.Email : user.Email;
		updateUser.Updated = DateTime.Now;

		await _dbContext.SaveChangesAsync();
		return updateUser;
	}

}
