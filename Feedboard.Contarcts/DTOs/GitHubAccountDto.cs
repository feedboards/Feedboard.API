namespace Feedboard.Contracts.DTOs;

public class GitHubAccountDto
{
	public string UserId { get; set; } = null!;

	public string AccessToken { get; set; } = null!;

	public string Scopes { get; set; } = null!;

	public string Username { get; set; } = null!;

	public string Email { get; set; } = null!;

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }

	public bool IsActive { get; set; }

	public string? PublicEmail { get; set; }
}
