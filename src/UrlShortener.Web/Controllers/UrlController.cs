using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Abstractions.Services;
using UrlShortener.Application.Dtos;
using UrlShortener.Application.Exceptions;
using UrlShortener.Persistence.Entities;

namespace UrlShortener.Web.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/url")]
[ApiVersion("1.0")]
public class UrlController(IUrlService service) : ControllerBase
{
	
	
	[HttpGet]
	public async Task<IActionResult> Get(CancellationToken ct = default)
	{
		return Ok(await service.GetAsync(ct));
	}

	[HttpGet("{id:Guid}")]
	public async Task<IActionResult> Get(Guid id, CancellationToken ct = default)
	{
		return Ok(await service.GetAsync(id, ct));
	}

	[HttpGet("/api/{code}")]
	public async Task<IActionResult> GetShorten([FromRoute] string code, CancellationToken ct = default)
	{
		var longUrl = await service.TransitionsAsync(code, ct);
		return Redirect(longUrl);
	}

	[HttpPost]
	public async Task<IActionResult> Add([FromBody] UrlCreateDto request, CancellationToken ct = default)
	{
		if (!Uri.TryCreate(request.LongUrl, UriKind.Absolute, out _))
			throw new BadRequestException("This url is not valid");

		var shortUrl = await service.AddAsync(request.LongUrl, HttpContext, ct);

		return Ok(shortUrl);
	}
	
	[HttpPatch]
	public async Task<IActionResult> Update(
		[FromBody] Url dto,
		CancellationToken ct = default)
	{
		return Ok(await service.UpdateAsync(dto, ct));
	}

	[HttpDelete("{id:Guid}")]
	public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
	{
		await service.DeleteAsync(id, ct);
		return NoContent();
	}
}