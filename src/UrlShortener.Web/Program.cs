using Serilog;
using UrlShortener.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.Host.UseSerilog(
	(context, config) =>
		config.ReadFrom.Configuration(context.Configuration).Enrich.FromLogContext());

services.AddPersistence(configuration);

var app = builder.Build();

app.ApplyMigrations();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();