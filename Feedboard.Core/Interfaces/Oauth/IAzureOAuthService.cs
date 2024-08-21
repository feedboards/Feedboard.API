using Feedboard.Contracts.DTOs.Request;
using Feedboard.Contracts.DTOs.Response;

namespace Feedboard.Core.Interfaces.Oauth;

public interface IAzureOAuthService
{
	Task<AzureOAuthTokenDTO> ProcessCodeAsync(AuthorizationCodeDTO model);
	Task<AzureOAuthTokenDTO> RefreshAccessTokenAsync(string refreshToken);
	Uri GetLoginUrl();
}
