namespace UrlShortener.Web.Models;

public class UrlViewModel
{
	public Guid Id { get; set; }
	public string LongUrl { get; set; } = string.Empty;
}