namespace EStore.Web.Areas.Staff.Controllers;

using System.Globalization;

[Area("Staff")]
public class ProductController(IEStoreService eStoreService) : Controller
{
    private readonly IEStoreService _eStoreService = eStoreService;

    public async Task<IActionResult> Category(string name, string? pageNumber)
    {
        var page = string.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber, NumberStyles.Number, new CultureInfo("en-US"));

        var result = await _eStoreService.GetProducts(page, name);

        ViewData["Title"] = result != null ? result.CategoryDescription : "Invalid Details Provided!";

        return View(result);
    }

    public IActionResult Settings() => View();
}
