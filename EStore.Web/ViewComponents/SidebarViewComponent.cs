namespace EStore.Web.ViewComponents;

using EStore.Domain.Abstraction.Service;

public class SidebarViewComponent(IEStoreService eStoreService) : ViewComponent
{
    private readonly IEStoreService _eStoreService = eStoreService;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var response = await _eStoreService.GetCategory();

        return View(response);
    }
}
