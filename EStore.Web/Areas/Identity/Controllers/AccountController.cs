namespace EStore.Web.Areas.Identity.Controllers;
using Microsoft.AspNetCore.Mvc;

[Area("Identity")]
public class AccountController : Controller
{
    public IActionResult Index() => View();
}
