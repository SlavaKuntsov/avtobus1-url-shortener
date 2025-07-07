using UrlShortener.Web.Middlewares;

namespace UrlShortener.Web.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddCommon(this IServiceCollection services)
	{
		services.AddProblemDetails();

		services.AddHttpContextAccessor();

		services.AddControllersWithViews();

		services.AddEndpointsApiExplorer();

		services.AddExceptionHandler<GlobalExceptionHandler>();
		
		return services;
	}
}