using Newtonsoft.Json;

namespace Feedboard.Contracts.DTOs.Response;

public class TokenDTO
{
	[JsonProperty("access_token")]
	public required string AccessToken { get; set; }
	[JsonProperty("refresh_token")]
	public required string RefreshToken { get; set; }
	[JsonProperty("id_token")]
	public required string IdToken { get; set; }
	[JsonProperty("expires_in")]
	public int ExpiresIn { get; set; }
	[JsonProperty("token_type")]
	public required string TokenType { get; set; }
	[JsonProperty("scope")]
	public required string Scope { get; set; }
}
