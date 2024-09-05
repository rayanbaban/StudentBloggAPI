using Microsoft.EntityFrameworkCore;
using StudentBloggAPI.Data;
using StudentBloggAPI.Data.MigratonsNy;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;
using StudentBloggAPI.Services;

namespace StudentBloggAPI.Repository
{
    public class PostRepository : IPostRepository
    {

        private readonly StudentBloggDbContextNy _dbContext; 
		private readonly ILogger<PostRepository> _logger;


		public PostRepository(StudentBloggDbContextNy dbContext, ILogger<PostRepository> logger)
        {
			_logger = logger;
            _dbContext = dbContext;
        }
       

        public async Task<Post?> DeletePostByIdAsync(int id)
        {
			var user = await _dbContext.Post.FirstOrDefaultAsync(x => x.Id == id);

			if (user == null)
				return null;

			var entities = _dbContext.Post.Remove(user);

			await _dbContext.SaveChangesAsync();
			if (entities != null)
				return entities.Entity;
			return null;

		}

        public async Task<Post?> GetPostByIdAsync(int id)
        {
			var user = await _dbContext.Post.FirstOrDefaultAsync(x => x.Id == id);

			return user;
		}

        public async Task<ICollection<Post>> GetPostsAsync()
        {
			return await _dbContext.Post.ToListAsync();
		}

		public async Task<Post?> AddPostAsync(Post post)
		{
			_logger.LogDebug("la til en ny bruker: {@user}", post);
			var entry = await _dbContext.Post.AddAsync(post);
			await _dbContext.SaveChangesAsync();
			if (entry != null) { return entry.Entity; }

			return null;

		}

		public async Task<Post?> UpdatePostAsync(int id, Post user)
        {
			var existingUser = await _dbContext.Post.FirstOrDefaultAsync(x => x.Id == id);

			if (existingUser == null)
			{
				return null; // Brukeren med den angitte ID-en ble ikke funnet.
			}

			// Gjør oppdateringer på den eksisterende brukeren med de nye opplysningene.
			existingUser.Title = user.Title;
			existingUser.Content = user.Content;
			existingUser.Updated = user.Updated;

			// Lagre endringene i databasen.
			await _dbContext.SaveChangesAsync();

			return existingUser;
		}
	}
}
