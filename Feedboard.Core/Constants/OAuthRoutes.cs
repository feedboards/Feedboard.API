namespace Feedboard.Core.Constants;

public static class OAuthRoutes
{
	public const string GITHUB_REDIRECT_URI = "http://localhost:17989";

    public const string SCOPES = "offline_access user.read mail.read Subscription.Read.All";
    public const string RESOURCE = "https://management.azure.com";
    public const string AZURE_REDIRECT_URI = "http://localhost:17988";
	public const string AZURE_REDIRECT_STATE = "feeedboard_azure_state-d6a102c6-7eaa-4ac7-b873-3a686546a1ca";
}
