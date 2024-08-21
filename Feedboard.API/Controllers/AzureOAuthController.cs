using Feedboard.Core.Interfaces.Oauth;
using Microsoft.AspNetCore.Mvc;

namespace Feedboard.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AzureOAuthController : Controller
{
	private readonly IAzureOAuthService azureOAuthService;

	public AzureOAuthController(IAzureOAuthService azureOAuthService)
	{
		this.azureOAuthService = azureOAuthService;
	}

	[HttpGet("login-url")]
	public IActionResult GetLoginUrl()
	{
		try
		{
			return Ok(new { url = azureOAuthService.GetLoginUrl().ToString() });
		}
		catch (Exception ex)
		{
			return BadRequest(ex);
		}
	}

	[HttpGet("process-code")]
	public async Task<IActionResult> ProcessCode([FromQuery] string code, [FromQuery] string state)
	{
		try
		{
			if (string.IsNullOrEmpty(code))
			{
				return BadRequest("Authorization code is missing.");
			}

			if (string.IsNullOrEmpty(state))
			{
				return BadRequest("Authorization state is missing.");
			}

			return Ok(await azureOAuthService.ProcessCodeAsync(
				new()
				{
					Code = code,
					State = state
				}));
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("update-access-token")]
	public async Task<IActionResult> RefreshAccessTokenAsync([FromQuery] string refreshToken)
	{
		try
		{
			if (string.IsNullOrEmpty(refreshToken))
			{
				return BadRequest("Authorization refresh token is missing.");
			}

			return Ok(await azureOAuthService.RefreshAccessTokenAsync(refreshToken));
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}

