using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Persistence.Entities;

namespace UrlShortener.Persistence.Configurations;

public class UrlConfiguration : IEntityTypeConfiguration<Url>
{
	public void Configure(EntityTypeBuilder<Url> builder)
	{
		builder.Property(t => t.Id)
			.IsRequired();

		builder.Property(t => t.Code)
			.HasMaxLength(10);

		builder.Property(t => t.Counter)
			.HasDefaultValue(0);

		builder.HasIndex(u => u.Code)
			.IsUnique();
	}
}