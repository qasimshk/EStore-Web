using System.Net.Http.Headers;
using EStore.Infrastructure.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();

builder.Services.AddHttpClient<IEStoreService, EStoreService>((serviceProvider, client) =>
{
    var serviceUrl = builder.Configuration.GetSection("ServiceEndpoint").Value!;

    client.DefaultRequestHeaders.Accept.Clear();

    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    client.BaseAddress = new Uri(serviceUrl);
})
.ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
{
    PooledConnectionLifetime = TimeSpan.FromMinutes(15)
})
.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

builder.Services.AddMemoryCache();

var app = builder.Build();
{
    app.UseExceptionHandler("/Home/Error"); // ToDo: set error page
    app.UseHsts();
    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{area=Identity}/{controller=Account}/{action=Index}/{name?}");

    app.Run();
}
