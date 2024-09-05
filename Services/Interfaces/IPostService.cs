using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Services.Interfaces;

public interface IPostService
{
	// CRUD: create - read - update - delete

	//Read
	Task<IEnumerable<PostDTO>> GetAllPostsAsync();

	Task<PostDTO?> GetPostByIdAsync(int postId);

	//Create
	Task<PostDTO?> AddPostAsync(PostDTO PostDTO, int inloggedUser);

	//Update
	Task<PostDTO?> UpdatePostAsync(int postId, PostDTO post, int inloggedUser);

	//Delete
	Task<PostDTO?> DeletePostAsync(int id, int loginUserId);
}
