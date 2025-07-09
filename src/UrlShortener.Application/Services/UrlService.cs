using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Abstractions.Infrastructure;
using UrlShortener.Application.Abstractions.Services;
using UrlShortener.Application.Dtos;
using UrlShortener.Application.Exceptions;
using UrlShortener.Persistence;
using UrlShortener.Persistence.Entities;

namespace UrlShortener.Application.Services;

public class UrlService(
	ApplicationDbContext dbContext,
	IUniqueCodeGenerator codeGenerator) : IUrlService
{
	public async Task<IEnumerable<Url>> GetAsync(CancellationToken ct = default)
	{
		return await dbContext.Urls
			.AsNoTracking()
			.OrderBy(x => x.CreatedOn)
			.ToListAsync(ct);
	}

	public async Task<Url> GetAsync(Guid id, CancellationToken ct = default)
	{
		return await dbContext.Urls
					.AsNoTracking()
					.FirstOrDefaultAsync(x => x.Id == id, ct)
				?? throw new NotFoundException($"Url with id '{id}' not found");
	}
	
	public async Task<string> TransitionsAsync(string code, CancellationToken ct = default)
	{
		var existEntity = await dbContext.Urls
			.FirstOrDefaultAsync(x => x.Code == code, ct)
			?? throw new NotFoundException($"Url with code '{code}' not found");

		existEntity.Counter++;

		await dbContext.SaveChangesAsync(ct);

		return existEntity.LongUrl;
	}

	public async Task<string> AddAsync(
		string longUrl,
		HttpContext httpContext,
		CancellationToken ct = default)
	{
		var code = await codeGenerator.GenerateRandomCode(longUrl, ct);

		var shortUrl =
			$"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/{code}";

		var url = new Url(shortUrl, longUrl, code);

		await dbContext.Urls.AddAsync(url, ct);
		await dbContext.SaveChangesAsync(ct);

		return shortUrl;
	}

	public async Task<Url> UpdateAsync(UrlUpdateDto url, CancellationToken ct = default)
	{
		var existing = await dbContext.Urls
			.FindAsync([url.Id], ct);

		if (existing is null)
			throw new NotFoundException($"Url with id '{url.Id}' not found");

		existing.LongUrl = url.LongUrl;

		await dbContext.SaveChangesAsync(ct);

		return existing;
	}

	public async Task DeleteAsync(Guid id, CancellationToken ct = default)
	{
		var existing = await dbContext.Urls
			.FindAsync([id], ct);

		if (existing is null)
			throw new NotFoundException($"Url with id '{id}' not found");

		dbContext.Urls.Remove(existing);
		await dbContext.SaveChangesAsync(ct);
	}
}