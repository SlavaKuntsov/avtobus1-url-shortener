namespace UrlShortener.Persistence.Entities;

public class Url
{
	public Guid Id { get; set; }
	public string ShortUrl { get; set; } = string.Empty;
	public string LongUrl { get; set; } = string.Empty;
	public string Code { get; set; } = string.Empty;
}