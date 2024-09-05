using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Repository.Interfaces;
using StudentBloggAPI.Services;
using StudentBloggAPI.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentBloggAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
	private readonly IPostService _postService;
	public PostController(IPostService userService)
	{
		_postService = userService;
	}
	// GET: api/<PostController>
	[HttpGet(Name = "GetAllPosts")]
	public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPostsAsync()
	{
		return Ok(await _postService.GetAllPostsAsync());
	}

	// GET api/<PostController>/5
	[HttpGet("{postId}", Name = "GetPostById")]
	public async Task<ActionResult<PostDTO>> GetPostById(int postId)
	{
		var post = await _postService.GetPostByIdAsync(postId);
		return post != null ? Ok(post) : NotFound();
	}

	// POST api/<PostController>
	[HttpPost]
	public async Task<ActionResult<PostDTO>> AddPostAsync([FromBody] PostDTO postDTO)
	{
		if (postDTO == null)
		{
			return BadRequest("Ugyldig innlegg");
		}

		int loginUserId = (int)HttpContext.Items["UserId"]!;
		var addedPost = await _postService.AddPostAsync(postDTO, loginUserId);

		if (addedPost != null)
		{
			return CreatedAtAction(nameof(GetPostById), new { addedPost.PostId }, addedPost);
		}

		return BadRequest("Feil ved opprettning av innlegg");
	}

	// PUT api/<PostController>/5
	[HttpPut("{postId}", Name = "UpdatePost")]
	public async Task<ActionResult<PostDTO>> UpdatePost(int postId, PostDTO postDTO)
	{
		int loginUserId = (int)HttpContext.Items["UserId"]!;

		var updatedPost = await _postService.UpdatePostAsync(postId, postDTO, loginUserId);

		if (updatedPost != null)
		{
			return Ok(updatedPost);
		}
		return NotFound($"Innlegget med ID {postId} ble ikke funnet");
	}

	// DELETE api/<PostController>/5
	[HttpDelete("{id}", Name = "Delete Post")]
	public async Task<ActionResult<PostDTO>> DeletePost(int id)
	{
		int loginUserId = (HttpContext.Items["UserId"] as int?) ?? 0;

		var deletedPost = await _postService.DeletePostAsync(id, loginUserId);

		if (deletedPost != null)
		{
			return Ok($"Innlegget med ID {id} ble slettet vellykket");
		}
		return NotFound($"Innlegget med ID {id} ble ikke funnet");
	}
}

