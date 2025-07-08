using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Abstractions.Services;
using UrlShortener.Web.Models;

namespace UrlShortener.Web.Controllers;

public class HomeController(IUrlService service) : Controller
{
	public async Task<IActionResult> Index(CancellationToken ct = default)
	{
		return View(await service.GetAsync(ct));
	}

	public new IActionResult Url()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}