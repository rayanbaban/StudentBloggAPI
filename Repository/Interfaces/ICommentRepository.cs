using StudentBloggAPI.Models.Entities;

namespace StudentBloggAPI.Repository.Interfaces
{
	public interface ICommentRepository
	{
		Task<IEnumerable<Comment?>> GetAllCommentsAsync(int page, int pageSize);
		Task<Comment?> AddCommentAsync(Comment comment);
		Task<Comment?> UpdateCommentAsync(int commentId, Comment updatedComment);
		Task<Comment?> DeleteCommentAsync(int commentId, int userId);
		Task<Comment?> GetCommentByIdAsync(int id);

	}
}
