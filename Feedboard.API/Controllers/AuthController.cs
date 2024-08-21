using Feedboard.Core.Interfaces.Oauth;
using Microsoft.AspNetCore.Mvc;

namespace Feedboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAzureOAuthService _azureOAuthService;
        private readonly IGitHubOauthService _gitHubOauthService;

        public AuthController(IAzureOAuthService azureOAuthService, IGitHubOauthService gitHubOauthService)
        {
            _azureOAuthService = azureOAuthService;
            _gitHubOauthService = gitHubOauthService;
        }

        [HttpGet("github/login-url")]
        public IActionResult GetGitHubLoginUrl()
        {
            return Ok(new
            {
                url = _gitHubOauthService.GetLoginUrl().ToString()
            });
        }

        [HttpGet("github/callback")]
        public async Task<IActionResult> GitHubCallback([FromQuery] string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Authorization code is missing.");
            }

            return Ok(await _gitHubOauthService.AuthenticateAndStoreUser(code));
        }

        [HttpGet("azure/login-url")]
        public IActionResult GetAzureLoginUrl()
        {
            return Ok(new
            {
                url = _azureOAuthService.GetLoginUrl().ToString()
            });
        }

        [HttpGet("azure/callback")]
        public async Task<IActionResult> AzureCallback(
            [FromQuery] string code,
            [FromQuery] string state)
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state))
            {
                return BadRequest("Authorization code/state is missing.");
            }

            return Ok(await _azureOAuthService.ProcessCodeAsync(new()
            {
                Code = code,
                State = state
            }));
        }

        [HttpGet("azure/refresh")]
        public async Task<IActionResult> AzureRefreshAccessToken([FromQuery] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest("Authorization refresh token is missing.");
            }

            return Ok(await _azureOAuthService.RefreshAccessTokenAsync(refreshToken));
        }
    }
}
