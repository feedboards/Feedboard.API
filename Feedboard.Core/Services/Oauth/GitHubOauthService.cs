using Feedboard.Core.Constants;
using Feedboard.Core.Converters;
using Feedboard.Contracts.DTOs;
using Feedboard.Core.Interfaces;
using Feedboard.Core.Interfaces.Oauth;
using Octokit;

namespace Feedboard.Core.Services.Oauth;

public class GitHubOauthService : IGitHubOauthService
{
	private readonly GitHubClient gitHubClient;
	private readonly IGitHubService githubService;
	private readonly string clientId;
	private readonly string clientSecret;

	public GitHubOauthService(IGitHubService githubService)
	{
		this.githubService = githubService;
		this.gitHubClient = new GitHubClient(new ProductHeaderValue("feedboard"));

		this.clientId = Environment.GetEnvironmentVariable("GITHUB_CLIENT_ID") ??
				throw new InvalidOperationException("GITHUB_CLIENT_ID can not be null");

		this.clientSecret = Environment.GetEnvironmentVariable("GITHUB_CLIENT_SECRET") ??
				throw new InvalidOperationException("GITHUB_CLIENT_SECRET can not be null");
	}

	public Uri GetLoginUrl()
	{
		return gitHubClient.Oauth.GetGitHubLoginUrl(new OauthLoginRequest(clientId)
		{
			Scopes = { "user:email", "read:user" },
			RedirectUri = new Uri(OAuthRoutes.GITHUB_REDIRECT_URI)
		});
	}

	public async Task<GitHubAccountDto> AuthenticateAndStoreUser(string code)
	{
		try
		{
			var token = await gitHubClient.Oauth.CreateAccessToken(
				new OauthTokenRequest(clientId, clientSecret, code)
				{
					RedirectUri = new Uri(OAuthRoutes.GITHUB_REDIRECT_URI)
				});

			var user = await GetUserInformation(token.AccessToken);

			var test = await githubService.UpdateOrInsertByUserIdAsync(new GitHubAccountDto()
			{
				UserId = user.Id.ToString(),
				AccessToken = token.AccessToken,
				Scopes = new ScopeConverter<string>(token.Scope).ConvertScopeToString(),
				Username = user.Login,
				Email = await GetUserEmails(token.AccessToken),
			});

			return test;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}

	private async Task<string> GetUserEmails(string accessToken)
	{
		gitHubClient.Credentials = new Credentials(accessToken);

		var emails = await gitHubClient.User.Email.GetAll();
		var privateEmails = emails
			.Where(email => email.Visibility == "private")
			.Select(email => email.Email)
			.ToList();

		return privateEmails?.FirstOrDefault() ??
			throw new ArgumentNullException("email can not be null");
	}

	private async Task<User> GetUserInformation(string accessToken)
	{
		gitHubClient.Credentials = new Credentials(accessToken);
		return await gitHubClient.User.Current();
	}
}
