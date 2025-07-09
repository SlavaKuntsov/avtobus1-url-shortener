using Microsoft.EntityFrameworkCore;

using Serilog;

using UrlShortener.Application.Extensions;
using UrlShortener.Persistence;
using UrlShortener.Persistence.Extensions;
using UrlShortener.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

builder.Host.UseSerilog(
	(context, config) =>
		config.ReadFrom.Configuration(context.Configuration).Enrich.FromLogContext());

builder.AddServiceDefaults();

builder.AddMySqlDbContext<ApplicationDbContext>(
	connectionName: "url-shortener",
	configureDbContextOptions: dbOptions =>
	{
		dbOptions.UseSnakeCaseNamingConvention();
	});

services
	.AddCommon()
	.AddSwagger()
	.AddApplication()
	.AddPersistence(configuration);

var app = builder.Build();

app.ApplyMigrations();

await app.SeedUrlsAsync();

app.UseSwagger();
app.UseSwaggerUI(
	c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API v1");
	});

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}
else
{
	app.UseExceptionHandler();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	"default",
	"{controller=Home}/{action=Index}/{id?}");
app.MapControllers();

app.Run();
