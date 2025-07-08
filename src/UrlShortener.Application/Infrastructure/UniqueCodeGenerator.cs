using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Abstractions.Infrastructure;
using UrlShortener.Application.Constants;
using UrlShortener.Persistence;

namespace UrlShortener.Application.Infrastructure;

public class UniqueCodeGenerator(
	Random random,
	ApplicationDbContext dbContext) : IUniqueCodeGenerator
{
	public async Task<string> GenerateRandomCode(string longUrl, CancellationToken ct = default)
	{
		var chars = new char[UrlConstants.CodeLength];

		while (true)
		{
			for (var i = 0; i < UrlConstants.CodeLength; i++)
			{
				var randomChar = random.Next(UrlConstants.Alphabet.Length - 1);

				chars[i] = UrlConstants.Alphabet[randomChar];
			}

			var code = new string(chars);

			if (!await dbContext.Urls.AnyAsync(u => u.Code == code, ct))
				return code;
		}
	}
}