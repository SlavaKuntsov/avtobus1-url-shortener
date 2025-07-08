using Microsoft.AspNetCore.Http;
using UrlShortener.Persistence.Entities;

namespace UrlShortener.Application.Abstractions.Services;

public interface IUrlService
{
	Task<IEnumerable<Url>> GetAsync(CancellationToken ct = default);
	Task<Url> GetAsync(Guid id, CancellationToken ct = default);
	Task<string> TransitionsAsync(string code, CancellationToken ct = default);
	Task<string> AddAsync(string longUrl, HttpContext httpContext, CancellationToken ct = default);
	Task<Url> UpdateAsync(Url url, CancellationToken ct = default);
	Task DeleteAsync(Guid id, CancellationToken ct = default);
}