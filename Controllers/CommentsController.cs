using Microsoft.AspNetCore.Mvc;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentBloggAPI.Controllers
{
    [Route("api/v1/[controller]")]
	[ApiController]
	public class CommentsController : ControllerBase
	{
		private readonly ICommentService _commentService;

		public CommentsController(ICommentService commentService)
		{
			_commentService = commentService;
		}

		// GET: api/<CommentsCotroller>
		[HttpGet(Name = "GetAllComments")]
		public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments(int pageNr = 1, int pageSize = 10)
		{
			var comments = await _commentService.GetAllCommentsAsync(pageNr, pageSize);
			return Ok(comments);
		}


		// POST api/<CommentsCotroller>
		[HttpPost(Name = "PostCommentOnAStatus")]
		public async Task<ActionResult<CommentDTO>> PostComment(int postId, CommentDTO commentDTO)
		{
			int loginUserId = (int)this.HttpContext.Items["UserId"]!;

			var addedComment = await _commentService.AddCommentAsync(postId, commentDTO, loginUserId);

			if (addedComment == null)
			{
				return BadRequest("Kunne ikke legge til kommentar.");
			}
			return Ok(addedComment);
		}


		// PUT api/<CommentsCotroller>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<CommentDTO>> UpdateComments(int commentId, [FromBody] CommentDTO commentDTO)
		{
			
			var updatedComment = await _commentService.UpdateCommentAsync(commentId, commentDTO, 0);
			if (updatedComment == null)
			{
				return NotFound();
			}
			return Ok(updatedComment);
			
		}

		// DELETE api/<CommentsCotroller>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<CommentDTO>>	DeleteComments(int commentId)
		{
			int inloggedUser = (int)this.HttpContext.Items["UserId"]!;

			var deletedComment = await _commentService.DeleteCommentAsync(commentId, inloggedUser);
			if (deletedComment == null)
			{
				return NotFound();
			}
			return Ok(deletedComment);
		}
	}
}
