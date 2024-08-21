using Feedboard.Contracts.DTOs;

namespace Feedboard.Core.Interfaces.Oauth;

public interface IGitHubOauthService
{
	Uri GetLoginUrl();
	Task<GitHubAccountDto> AuthenticateAndStoreUser(string code);
}
