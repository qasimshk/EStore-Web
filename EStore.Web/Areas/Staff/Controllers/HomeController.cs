namespace EStore.Web.Areas.Staff.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

[Area("Staff")]
public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Product() => View();

    public IActionResult Privacy() => View();

    public IActionResult Settings() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
