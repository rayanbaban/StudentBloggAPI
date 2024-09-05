using StudentBloggAPI.Models.Entities;

namespace StudentBloggAPI.Repository.Interfaces;

public interface IPostRepository
{
	//create
	Task<Post?> AddPostAsync(Post user);

	// update 
	Task<Post?> UpdatePostAsync(int id, Post user);
	//delete
	Task<Post?> DeletePostByIdAsync(int id);
	//read
	Task<ICollection<Post>> GetPostsAsync();

	Task<Post?> GetPostByIdAsync(int id);
}
