namespace Feedboard.Contracts.DTOs;

public class AzureAccountDto
{
	public string Email { get; set; }

	public string IdToken { get; set; } = null!;

	public string AccessToken { get; set; } = null!;

	public string RefreshToken { get; set; } = null!;

	public DateTime AccessTokenExpiredAt { get; set; }

	public DateTime? CreatedAt { get; set; }

	public DateTime? UpdatedAt { get; set; }

	public bool IsActive { get; set; }
}
