using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Persistence.Entities;

namespace UrlShortener.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	: DbContext(options)
{
	public DbSet<Url> Urls { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		base.OnModelCreating(modelBuilder);
	}
}