namespace Feedboard.Contracts.DTOs.Response;

public class AzureOAuthTokenDTO
{
	public required string AccessToken { get; set; }

	public required string RefreshToken { get; set; }

	public required string IdToken { get; set; }

	public DateTime AccessTokenExpiredAt { get; set; }
}
