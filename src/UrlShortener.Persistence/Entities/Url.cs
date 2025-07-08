namespace UrlShortener.Persistence.Entities;

public class Url
{
	public Guid Id { get; set; }
	public string ShortUrl { get; set; }
	public string LongUrl { get; set; }
	public string Code { get; set; }
	public int Counter { get; set; }
	public DateTime CreatedOn { get; set; }

	public Url(
		string shortUrl,
		string longUrl,
		string code)
	{
		Id = Guid.NewGuid();
		ShortUrl = shortUrl;
		LongUrl = longUrl;
		Code = code;
		Counter = 0;
		CreatedOn = DateTime.UtcNow;
	}
}