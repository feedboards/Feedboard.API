namespace Feedboard.Core.Converters;

public class TokenExpiryConverter
{
	public int TokenExpiresIn { get; private set; }
	public DateTime TokenExpiresAt { get; private set; }

	public TokenExpiryConverter(int tokenExpiresIn)
	{
		TokenExpiresIn = tokenExpiresIn;
		TokenExpiresAt = ConvertIntToDateTime(tokenExpiresIn);
	}

	public TokenExpiryConverter(DateTime tokenExpiresAt)
	{
		TokenExpiresAt = tokenExpiresAt;
		TokenExpiresIn = ConvertDateTimeToInt(tokenExpiresAt);
	}

	public int ConvertDateTimeToInt(DateTime dateTime)
	{
		return (int)(dateTime - DateTime.UtcNow).TotalSeconds;
	}

	public DateTime ConvertIntToDateTime(int seconds)
	{
		return DateTime.UtcNow.AddSeconds(seconds);
	}
}
