using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Abstractions.Infrastructure;
using UrlShortener.Application.Abstractions.Services;
using UrlShortener.Application.Infrastructure;
using UrlShortener.Application.Services;

namespace UrlShortener.Application.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddScoped<IUrlService, UrlService>();
		
		services.AddSingleton<Random>();
		services.AddScoped<IUniqueCodeGenerator, UniqueCodeGenerator>();

		return services;
	}
}