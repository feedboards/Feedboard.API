using Feedboard.Core.Interfaces.Oauth;
using Microsoft.AspNetCore.Mvc;

namespace Feedboard.API.Controllers;

[ApiController]
[Route("[Controller]")]
public class GitHubOauthController : Controller
{
	private readonly IGitHubOauthService gitHubOauthService;

	public GitHubOauthController(IGitHubOauthService gitHubOauthService)
	{
		this.gitHubOauthService = gitHubOauthService;
	}

	[HttpGet("login")]
	public IActionResult GitHubLogin()
	{
		try
		{
			return Ok(new { url = gitHubOauthService.GetLoginUrl().ToString() });
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}

	[HttpGet("callback")]
	public async Task<IActionResult> GitHubCallback(string code)
	{
		if (string.IsNullOrEmpty(code))
		{
			return BadRequest("Authorization code is missing.");
		}

		try
		{
			var userAccount = await gitHubOauthService.AuthenticateAndStoreUser(code);

			return Ok(userAccount);
		}
		catch (Exception ex)
		{
			return BadRequest($"Error during authentication: {ex.Message}");
		}
	}
}
