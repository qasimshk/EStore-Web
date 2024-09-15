namespace EStore.Web.Areas.Identity.Controllers;

[Area("Identity")]
public class AccountController : Controller
{
    public IActionResult Index() => View();
}
