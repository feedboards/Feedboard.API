using Feedboard.Core.Interfaces;
using Feedboard.Core.Interfaces.Oauth;
using Feedboard.Core.Services;
using Feedboard.Core.Services.Oauth;
using Microsoft.Extensions.DependencyInjection;

namespace Feedboard.Core;

public static class DI
{
	public static IServiceCollection AddCore(this IServiceCollection services)
	{
		services.AddTransient<IGitHubService, GitHubService>();
		services.AddTransient<IGitHubOauthService, GitHubOauthService>();
		services.AddTransient<IAzureOAuthService, AzureOAuthService>();
		services.AddTransient<IAzureService, AzureService>();

		return services;
	}
}