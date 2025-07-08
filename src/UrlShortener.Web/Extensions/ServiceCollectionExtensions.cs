using Asp.Versioning;
using Microsoft.OpenApi.Models;
using UrlShortener.Web.Middlewares;

namespace UrlShortener.Web.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddCommon(this IServiceCollection services)
	{
		services.AddProblemDetails();

		services.AddHttpContextAccessor();

		services.AddControllers();
		services.AddControllersWithViews();

		services.AddEndpointsApiExplorer();

		services.AddExceptionHandler<GlobalExceptionHandler>();
		
		return services;
	}
	
	public static IServiceCollection AddSwagger(this IServiceCollection services)
	{
		var apiVersioningBuilder = services.AddApiVersioning(
			o =>
			{
				o.AssumeDefaultVersionWhenUnspecified = true;
				o.DefaultApiVersion = new ApiVersion(1, 0);
				o.ReportApiVersions = true;
				o.ApiVersionReader = new UrlSegmentApiVersionReader();
			});

		apiVersioningBuilder.AddApiExplorer(
			options =>
			{
				options.GroupNameFormat = "'v'VVV";
				options.SubstituteApiVersionInUrl = true;
			});
		
		services.AddSwaggerGen(
			options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API v1", Version = "v1" });
			});

		return services;
	}
}