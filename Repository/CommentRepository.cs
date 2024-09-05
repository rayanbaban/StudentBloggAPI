using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StudentBloggAPI.Data;
using StudentBloggAPI.Data.MigratonsNy;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;

namespace StudentBloggAPI.Repository
{
	public class CommentRepository : ICommentRepository
	{
		private readonly StudentBloggDbContextNy _dbContext;

		public CommentRepository(StudentBloggDbContextNy dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Comment?> AddCommentAsync(Comment comment)
		{
			await _dbContext.Comments.AddAsync(comment);
			await _dbContext.SaveChangesAsync();
			return comment;
		}

		public async Task<Comment?> DeleteCommentAsync(int commentId, int userId)
		{
			var comment = await _dbContext.Comments
	   .Where(c => c.Id == commentId && c.UserId == userId)
	   .FirstOrDefaultAsync();

			if (comment == null)
			{
				return null; 
			}

			_dbContext.Comments.Remove(comment);
			await _dbContext.SaveChangesAsync();

			return comment;
		}

		public async Task<IEnumerable<Comment?>> GetAllCommentsAsync(int page, int pageSize)
		{
			if (page < 1 || pageSize < 1)
			{
				return Enumerable.Empty<Comment>();
			}

			return await _dbContext.Comments
				.OrderByDescending(c => c.DateCommented)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}

		public async Task<Comment?> GetCommentByIdAsync(int id)
		{
			var comment = await _dbContext.Comments.FindAsync(id);
			if (comment == null)
				return null;

			await _dbContext.SaveChangesAsync();
			return comment;
		}

		public async Task<Comment?> UpdateCommentAsync(int commentId, Comment comment)
		{
			var existingComment = await _dbContext.Comments.FindAsync(commentId);
			if (existingComment == null)
			{
				return null;
			}

			existingComment.Content = comment.Content;
			existingComment.Updates = comment.Updates;

			await _dbContext.SaveChangesAsync();

			return existingComment;
		}
	}
}
