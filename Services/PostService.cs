using Microsoft.AspNetCore.Server.IIS.Core;
using StudentBloggAPI.Mappers;
using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;
using StudentBloggAPI.Services.Interfaces;

namespace StudentBloggAPI.Services
{
	public class PostService : IPostService
	{
		private readonly IPostRepository _postRepository;
		private readonly IUserRepository _userRepository;
		private readonly IMapper<Post, PostDTO> _postMapper;
		private readonly ILogger<PostService> _logger;


		public PostService(IPostRepository postRepository, IUserRepository userRepository,
			IMapper<Post, PostDTO> postMapper, ILogger<PostService> logger)
		{
			_postRepository = postRepository;
			this._userRepository = userRepository;
			_postMapper = postMapper;
			_logger = logger;
		}

		public async Task<PostDTO?> AddPostAsync(PostDTO postDTO, int inloggedUser)
		{
			var loggedInUser = await _userRepository.GetUserByIdAsync(inloggedUser);
	
			var postToAdd = _postMapper.MapToModel(postDTO);
			postToAdd.UserId = inloggedUser;

			var addedPost = await _postRepository.AddPostAsync(postToAdd);

			return addedPost != null ? _postMapper.MapToDto(addedPost) : null;
		}
		public async Task<IEnumerable<PostDTO>> GetAllPostsAsync()
		{
	
			var posts = await _postRepository.GetPostsAsync();
			return posts.Select(post => _postMapper.MapToDto(post)).ToList();
		}

		public async Task<PostDTO?> GetPostByIdAsync(int postId)
		{
			var post = await _postRepository.GetPostByIdAsync(postId);
			return post != null ? _postMapper.MapToDto(post) : null;
		}
		public async Task<PostDTO?> DeletePostAsync(int deletePostId, int loginUserId)
		{
			var postToDelete = await _postRepository.GetPostByIdAsync(deletePostId);
			var loginUser = await _userRepository.GetUserByIdAsync(loginUserId);

			if (postToDelete == null || loginUser == null)
			{
				_logger?.LogError("Innlegget med ID {PostId} ble ikke funnet for sletting", deletePostId);
				throw new UnauthorizedAccessException($"Bruker {loginUserId} har ikke tilgang til å slette innlegget");
			}

			// Sjekk om brukeren har tilgang til å slette innlegget
			if (loginUserId != postToDelete.UserId && !loginUser.IsAdminUser)
			{
				_logger?.LogError("Bruker {LoginUserId} har ikke tilgang til å slette dette innlegget", loginUserId);
				throw new UnauthorizedAccessException($"Bruker {loginUserId} har ikke tilgang til å slette innlegget");
			}

			var deletedPost = await _postRepository.DeletePostByIdAsync(deletePostId);

			if (deletedPost != null)
			{
				_logger?.LogInformation("Innlegget med ID {PostId} ble slettet vellykket", deletePostId);
				var deletedPostDTO = _postMapper.MapToDto(deletedPost);
				return deletedPostDTO;
			}

			return null;
		}


		public async Task<PostDTO?> UpdatePostAsync(int postId, PostDTO postDTO, int loginUserId)
		{
			var postToUpdate = await _postRepository.GetPostByIdAsync(postId);

			// Sjekk om innlegget ble funnet
			if (postToUpdate == null)
			{
				_logger?.LogError("Innlegget med ID {PostId} ble ikke funnet for oppdatering", postId);
				return null;
			}

			// Sjekk om brukeren er logget inn
			if (postToUpdate.User == null)
			{
				_logger?.LogError("Innlegget med ID {PostId} har ingen tilknyttet bruker. Kan ikke oppdatere", postId);
				return null;
			}

			// Sjekk om brukeren har tilgang til å oppdatere innlegget
			if (loginUserId != postToUpdate.UserId && !postToUpdate.User.IsAdminUser)
			{
				_logger?.LogError("Bruker {LoginUserId} har ikke tilgang til å oppdatere dette innlegget", loginUserId);

				// Logg mer informasjon for debugging
				_logger?.LogError($"Detaljer: LoginUserId: {loginUserId}, PostUserId: {postToUpdate.UserId}, IsAdminUser: {postToUpdate.User.IsAdminUser}");

				throw new UnauthorizedAccessException($"Bruker {loginUserId} har ikke tilgang til å oppdatere innlegget");
			}

			// Oppdater innlegget med verdiene fra postDTO
			var post = _postMapper.MapToModel(postDTO);
			post.Id = postId;

			var result = await _postRepository.UpdatePostAsync(postId, post);

			if (result != null)
			{
				_logger?.LogInformation("Innlegget med ID {PostId} ble oppdatert vellykket", postId);
				var updatedPostDTO = _postMapper.MapToDto(result);
				return updatedPostDTO;
			}

			return null;
		}
	}
}
