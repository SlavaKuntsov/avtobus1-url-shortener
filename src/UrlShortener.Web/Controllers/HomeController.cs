using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Abstractions.Services;
using UrlShortener.Application.Dtos;
using UrlShortener.Web.Models;

namespace UrlShortener.Web.Controllers;

public class HomeController(IUrlService service) : Controller
{
	public async Task<IActionResult> Index(CancellationToken ct = default)
	{
		return View(await service.GetAsync(ct));
	}

	[HttpGet]
	public new async Task<IActionResult> Url(Guid? id, CancellationToken ct = default)
	{
		if (id == null)
			return View(new UrlViewModel());

		var entity = await service.GetAsync(id.Value, ct);

		var vm = new UrlViewModel
		{
			Id = entity.Id,
			LongUrl = entity.LongUrl
		};

		return View(vm);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public new async Task<IActionResult> Url(UrlViewModel vm, CancellationToken ct = default)
	{
		if (!Uri.TryCreate(vm.LongUrl, UriKind.Absolute, out _))
			ModelState.AddModelError(nameof(vm.LongUrl), "This url is not valid");

		if (!ModelState.IsValid)
			return View(vm);

		if (vm.Id == Guid.Empty)
		{
			await service.AddAsync(vm.LongUrl, HttpContext, ct);
		}
		else
		{
			var existing = await service.GetAsync(vm.Id, ct);
			existing.LongUrl = vm.LongUrl;
			var url = new UrlUpdateDto(existing.Id, existing.LongUrl);
			await service.UpdateAsync(url, ct);
		}

		return RedirectToAction(nameof(Index));
	}
	
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
	{
		await service.DeleteAsync(id, ct);
		return RedirectToAction(nameof(Index));
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}