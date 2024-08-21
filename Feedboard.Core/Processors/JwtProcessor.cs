using System.IdentityModel.Tokens.Jwt;

namespace Feedboard.Core.Processors;

public static class JwtProcessor
{
	public static string? ExtractEmailFromToken(string jwtToken)
	{
		try
		{
			var token = new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken 
				?? throw new ArgumentException("Invalid JWT token provided.");

			var email = token.Claims.FirstOrDefault(claim => claim.Type == "email").Value;

			return email == null 
				? throw new InvalidOperationException("Email claim not found in token.") 
				: email;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred: {ex.Message}");
			return null;
		}
	}
}
