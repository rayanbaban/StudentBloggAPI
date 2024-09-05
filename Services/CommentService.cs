using Microsoft.AspNetCore.Http.HttpResults;
using StudentBloggAPI.Mappers;
using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;
using StudentBloggAPI.Services.Interfaces;

namespace StudentBloggAPI.Services
{
    public class CommentService : ICommentService
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IUserRepository _userRepository;
		private readonly IMapper<Comment, CommentDTO> _commentMapper;
		private readonly ILogger<CommentService> _logger;

		public CommentService(ICommentRepository commentService, IUserRepository userRepository, IMapper<Comment, CommentDTO> mapperService, ILogger<CommentService> logger)
		{
			_commentRepository = commentService;
			_userRepository = userRepository; 
			_commentMapper = mapperService;
			_logger = logger;
		}

		public async Task<CommentDTO?> AddCommentAsync(int commentId, CommentDTO commentDTO, int inloggedUser)
		{
			var loginUser = await _userRepository.GetUserByIdAsync(inloggedUser);
			var comment = _commentMapper.MapToModel(commentDTO);

			comment.PostId = commentId;
			comment.UserId = inloggedUser;

			var addedComent = await _commentRepository.AddCommentAsync(comment);

			_logger.LogInformation($"Ny kommentar lagt til {addedComent}");

			if (addedComent != null)
			{
				return _commentMapper.MapToDto(addedComent);
			}
			else
			{
				_logger?.LogError("Feil ved forsøk å legge til en kommentar - added comment er null");
				return null; 
			}
		}


		public async Task<CommentDTO?> DeleteCommentAsync(int commentId, int inloggedUserId)
		{
			var loggedinUser = _userRepository.GetUserByIdAsync(inloggedUserId);
			if(loggedinUser == null) 
			{
				_logger?.LogError($"Uatorisert bruker: {inloggedUserId} prøver å slette en kommentar {commentId}");
				throw new UnauthorizedAccessException($"Bruker {inloggedUserId} har ikke tilgang til å se alle innlegg, må være innlogget");
			}
			
			var deletedComment = await _commentRepository.DeleteCommentAsync(commentId, inloggedUserId);
			if (deletedComment == null) 
			{
				_logger?.LogWarning("Feil ved sletting av kommentar med ID: {@comment} ", commentId);
				return null;
			}
			return _commentMapper.MapToDto(deletedComment);
		}

		public async Task<IEnumerable<CommentDTO?>> GetAllCommentsAsync(int pageNr, int pageSize)
		{
			var comments = await _commentRepository.GetAllCommentsAsync(pageNr, pageSize);
			_logger?.LogInformation($"Bruker prøver å hente kommentarer");

			var dtos = comments.Select(users => _commentMapper.MapToDto(users)).ToList();
			return dtos;

		}
		
		

		public async Task<CommentDTO?> GetCommentByIdAsync(int id)
		{
			var comment = await _commentRepository.GetCommentByIdAsync(id);

			if (comment == null)
			{
				_logger?.LogWarning("Kommentar med ID {CommentId} ble ikke funnet", id);
				return null;
			}

			return _commentMapper.MapToDto(comment);

		}

		public async Task<CommentDTO?> UpdateCommentAsync(int commentId, CommentDTO commentDTO, int inloggedUserId)
		{
			var existingComment = await _commentRepository.GetCommentByIdAsync(commentId);
			if(existingComment == null)
			{
				_logger?.LogError("Forøkte å oppdate en ikke-eksisterende kommentar {@commentID}.", commentId);
				return null;
			}
			existingComment.Content = commentDTO.Content;
			existingComment.Updates = DateTime.Now;

			var updatedComment = await _commentRepository.UpdateCommentAsync(commentId, existingComment);
			return _commentMapper.MapToDto(updatedComment);
		
		}

	}
}