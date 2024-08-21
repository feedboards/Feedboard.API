using Feedboard.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Feedboard.DAL;

public static class DI
{
	public static IServiceCollection AddDAL(this IServiceCollection services, string connetionString)
	{
		return services.AddDbContext<FeedboardDbContext>(option =>
			option.UseSqlServer(connetionString, ss =>
			{
				ss.EnableRetryOnFailure(3);
			}).EnableSensitiveDataLogging());
	}
}