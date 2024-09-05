using StudentBloggAPI.Services.Interfaces;
using System.Diagnostics;

namespace StudentBloggAPI.Middleware;

public class StudentBloggBasicAuthentication : IMiddleware
{

	private readonly IUserService _userService;
    public StudentBloggBasicAuthentication(IUserService userService)
    {
		_userService = userService;

    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		// gyldig path /api/v1/users/register -> gyldig path som ikke trenger authentication

		if (context.Request.Path.StartsWithSegments("/api/v1/Users/register") &&
			context.Request.Method == "POST")
		{
			await next(context);
			return;
		}
		try
		{
			if (!context.Request.Headers.ContainsKey("Authorization"))
				throw new UnauthorizedAccessException("Authorization mangler i HTTP-Reader");

			var authHeader = context.Request.Headers.Authorization;

			//  Basic cmF5YW46aGVtbWVsaWc=
			string base64string = authHeader.ToString().Split(" ")[1];

			string user_password = decodeBase64String(base64string);

			string[] arr = user_password.Split(":");
			string username = arr[0];
			string password = arr[1];	

			//Username -> slå opp i database
			int? userId = await _userService.GetAuthenticatedIdAsync(username,password);
			if (userId == 0)
			{
				throw new UnauthorizedAccessException("Ingen tilgang til dette APi");
			}
			context.Items["UserId"] = userId;
			context.Items["UserName"] = username;

			//Console.WriteLine(authHeader);

			await next(context);
		}
		catch (UnauthorizedAccessException ex )
		{
			await Results.Problem(title: "Unauthorized: ikke lov til å bruke API",
				statusCode: StatusCodes.Status401Unauthorized,
				detail: ex.Message,
				extensions: new Dictionary<string, object?>
				{
					{"traceId", Activity.Current?.Id }
				})
				.ExecuteAsync(context);
		}
	
	}
	private string decodeBase64String(string base64string)
	{
		byte[] base64bytes = System.Convert.FromBase64String(base64string);
		string username_and_password = System.Text.Encoding.UTF8.GetString(base64bytes);
		return username_and_password;
	}
}
