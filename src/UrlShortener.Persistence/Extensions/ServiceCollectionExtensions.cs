using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UrlShortener.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddPersistence(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
								?? configuration.GetConnectionString(nameof(ApplicationDbContext));

		services.AddDbContextPool<ApplicationDbContext>(
			options =>
			{
				options.UseNpgsql(connectionString);
				options.UseSnakeCaseNamingConvention();
			},
			128);

		// services.AddDbContextPool<ApplicationDbContext>(
		// 	options =>
		// 	{
		// 		options.UseMySql(
		// 			connectionString,
		// 			new MySqlServerVersion(new Version(10, 3, 0)),
		// 			mySqlOptions =>
		// 			{
		// 				mySqlOptions.EnableRetryOnFailure();
		// 			}
		// 		);
		// 		options.UseSnakeCaseNamingConvention();
		// 	},
		// 	128);

		return services;
	}
}