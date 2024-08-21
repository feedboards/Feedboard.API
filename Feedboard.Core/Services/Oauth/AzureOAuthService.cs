using Feedboard.Core.Constants;
using Feedboard.Core.Converters;
using Feedboard.Contracts.DTOs.Request;
using Feedboard.Contracts.DTOs.Response;
using Feedboard.Core.Interfaces;
using Feedboard.Core.Interfaces.Oauth;
using Feedboard.Core.Processors;
using Newtonsoft.Json;
using Octokit;
using RestSharp;

namespace Feedboard.Core.Services.Oauth;

public class AzureOAuthService : IAzureOAuthService
{
	private readonly IAzureService azureService;

	private readonly string clientId;
	private readonly string clientSecret;
	private readonly string tenantId;

	public AzureOAuthService(IAzureService azureService)
	{
		this.azureService = azureService;

		this.clientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID") ??
			throw new Exception("AZURE_CLIENT_ID can't be null");

		this.clientSecret = Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET") ??
			throw new Exception("AZURE_CLIENT_SECRET can't be null");

		this.tenantId = Environment.GetEnvironmentVariable("AZURE_TENANT_ID") ??
			throw new Exception("AZURE_TENANT_ID can't be null");
	}

	public Uri GetLoginUrl()
	{
		return new Uri(
			$@"https://login.microsoftonline.com/{tenantId}/oauth2/authorize?client_id={clientId}"
			+ $@"&response_type=code&redirect_uri={OAuthRoutes.AZURE_REDIRECT_URI}"
			+ $@"&response_mode=query&scope={OAuthRoutes.SCOPES}&state={OAuthRoutes.AZURE_REDIRECT_STATE}"
		);
	}

	public async Task<AzureOAuthTokenDTO> ProcessCodeAsync(AuthorizationCodeDTO model)
	{
		var response = await GetProcessCodeAzureResponseAsync(model.Code);

		if (response.IsSuccessful)
		{
			var tokenResult = JsonConvert.DeserializeObject<TokenDTO>(response?.Content ??
				throw new Exception("response can't be null"));

			var tokenExpiry = new TokenExpiryConverter(tokenResult.ExpiresIn);

			await azureService.UpdateOrInsertAsync(new()
			{
				IdToken = tokenResult.IdToken,
				AccessToken = tokenResult.AccessToken,
				RefreshToken = tokenResult.RefreshToken,
				AccessTokenExpiredAt = tokenExpiry.TokenExpiresAt,
				Email = JwtProcessor.ExtractEmailFromToken(tokenResult.AccessToken),
			});

			return new()
			{
				AccessToken = tokenResult.AccessToken,
				RefreshToken = tokenResult.RefreshToken,
				IdToken = tokenResult.IdToken,
				AccessTokenExpiredAt = tokenExpiry.TokenExpiresAt,
			};
		}
		else
		{
			throw new ApplicationException($"Failed to retrieve access token. Status code: {response.StatusCode}, Response: {response.Content}");
		}
	}

	public async Task<AzureOAuthTokenDTO> RefreshAccessTokenAsync(string refreshToken)
	{
		var response = await GetRefreshAccessTokenAzureResponseAsync(refreshToken);

		if (response.IsSuccessful)
		{
			var tokenResult = JsonConvert.DeserializeObject<TokenDTO>(response?.Content ??
				throw new Exception("response can't be null"));

			var tokenExpiry = new TokenExpiryConverter(tokenResult.ExpiresIn);

			await azureService.UpdateOrInsertAsync(new()
			{
				IdToken = tokenResult.IdToken,
				AccessToken = tokenResult.AccessToken,
				RefreshToken = tokenResult.RefreshToken,
				AccessTokenExpiredAt = tokenExpiry.TokenExpiresAt,
				Email = JwtProcessor.ExtractEmailFromToken(tokenResult.AccessToken),
			});

			return new AzureOAuthTokenDTO
			{
				AccessToken = tokenResult.AccessToken,
				RefreshToken = tokenResult.RefreshToken,
				IdToken = tokenResult.IdToken,
				AccessTokenExpiredAt = tokenExpiry.TokenExpiresAt,
			};
		}
		else
		{
			throw new ApplicationException($"Failed to refresh access token. Status code: {response.StatusCode}, Response: {response.Content}");
		}
	}

	private async Task<RestResponse> GetRefreshAccessTokenAzureResponseAsync(string refreshToken)
	{
		var request = new RestRequest
		{
			Method = Method.Post
		};

		request.AddParameter("client_id", clientId, ParameterType.GetOrPost);
		request.AddParameter("grant_type", "refresh_token", ParameterType.GetOrPost);
		request.AddParameter("refresh_token", refreshToken, ParameterType.GetOrPost);
		request.AddParameter("client_secret", clientSecret, ParameterType.GetOrPost);

		return await new RestClient("https://login.microsoftonline.com/common/oauth2/token")
			.ExecuteAsync(request);
	}

	private async Task<RestResponse> GetProcessCodeAzureResponseAsync(string code)
	{
		var request = new RestRequest($"/{tenantId}/oauth2/token", Method.Post);

		request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

		request.AddParameter("client_id", clientId, ParameterType.GetOrPost);
		request.AddParameter("scope", OAuthRoutes.SCOPES, ParameterType.GetOrPost);
		request.AddParameter("code", code, ParameterType.GetOrPost);
		request.AddParameter("redirect_uri", OAuthRoutes.AZURE_REDIRECT_URI, ParameterType.GetOrPost);
		request.AddParameter("grant_type", "authorization_code", ParameterType.GetOrPost);
		request.AddParameter("client_secret", clientSecret, ParameterType.GetOrPost);
		request.AddParameter("resource", OAuthRoutes.RESOURCE, ParameterType.GetOrPost);

		return await new RestClient("https://login.microsoftonline.com")
			.ExecuteAsync(request);
	}
}
