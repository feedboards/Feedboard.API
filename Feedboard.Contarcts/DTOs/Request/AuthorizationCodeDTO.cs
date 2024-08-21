namespace Feedboard.Contracts.DTOs.Request;

public class AuthorizationCodeDTO
{
	public required string Code { get; set; }
	public required string State { get; set; }
	public string? SessionState { get; set; }
}
