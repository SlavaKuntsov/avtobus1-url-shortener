namespace UrlShortener.Application.Abstractions.Infrastructure;

public interface IUniqueCodeGenerator
{
	Task<string> GenerateRandomCode(string longUrl, CancellationToken ct = default);
}