using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UrlShortener.Persistence.Entities;

namespace UrlShortener.Persistence.Extensions;

public static class UrlsSeedExtensions
{
	public static async Task SeedUrlsAsync(this IApplicationBuilder app)
	{
		using var scope = app.ApplicationServices.CreateScope();
		var services = scope.ServiceProvider;
		var dbContext = services.GetRequiredService<ApplicationDbContext>();
		var logger = services.GetRequiredService<ILogger<IApplicationBuilder>>();

		try
		{
			var url = new Url(
				"http://localhost:7000/api/wcsZKJVx",
				"https://github.com/SlavaKuntsov",
				"wcsZKJVx");
			
			await dbContext.Urls.AddAsync(url);
			await dbContext.SaveChangesAsync();
			
			logger.LogInformation("Exchanges seeded successfully.");
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred while seeding exchanges");
		}
	}
}