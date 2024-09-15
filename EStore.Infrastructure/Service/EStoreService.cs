namespace EStore.Infrastructure.Service;

using System.Net.Http.Json;
using System.Text.Json;
using EStore.Domain.Abstraction.Service;
using EStore.Domain.Common.Pagination;
using EStore.Domain.Common.Results;
using EStore.Domain.Responses;
using EStore.Domain.ViewModels;
using Microsoft.Extensions.Caching.Memory;

public class EStoreService(HttpClient httpClient, IMemoryCache memoryCache) : IEStoreService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IMemoryCache _memoryCache = memoryCache;
    private const string CacheKey = "categories";

    public async Task<ProductViewModel?> GetProducts(int pageNumber, string categoryName)
    {
        var cache = _memoryCache.Get(CacheKey) as List<CategoryResponse>;
        var urlEndpoint = string.Empty;
        var categoryDesc = string.Empty;

        if (categoryName == "All")
        {
            urlEndpoint = $"products?PageNumber={pageNumber}&PageSize=9";
            categoryDesc = "General Catewgory";
        }
        else if (cache != null && cache.Any(x => x.Url == categoryName))
        {
            var category = cache!.First(x => x.Url == categoryName);

            urlEndpoint = $"products?CategoryId={category.CategoryId}&PageNumber={pageNumber}&PageSize=9";
            categoryDesc = category.Description;
        }
        else
        {
            return null;
        }

        var response = await _httpClient.GetAsync(urlEndpoint);

        if (response.IsSuccessStatusCode)
        {
            var pagination = JsonSerializer.Deserialize<PaginationInfo>(response.Headers.GetValues("X-Pagination").Single());

            var products = (await response.Content.ReadFromJsonAsync<List<ProductResponse>>())!;

            return new ProductViewModel
            {
                Pagination = pagination!,
                Products = products,
                CategoryDescription = categoryDesc
            };
        }

        return default;
    }

    public async Task<ICollection<CategoryResponse>> GetCategory()
    {
        var cacheCategories = new List<CategoryResponse>();

        if(!_memoryCache.TryGetValue(CacheKey, out cacheCategories))
        {
            var response = await _httpClient.GetAsync("categories");
            var result = (await response.Content.ReadFromJsonAsync<Result<List<CategoryResponse>>>())!;

            cacheCategories = result.Value.Select(x => new CategoryResponse
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                Url = RemoveSpaceAndSlash(x.CategoryName),
                Css = GetCss(x.CategoryId),
                Description = x.Description,
            }).ToList();

            _memoryCache.Set(CacheKey, cacheCategories);
        }
        return cacheCategories!;
    }

    private static string RemoveSpaceAndSlash(string input) => input.Replace(" ", "").Replace("/", "");

    private static string GetCss(int Id)
    {
        var cssList = new Dictionary<int, string>
        {
            { 1, "mdi mdi-liquor menu-icon" },
            { 2, "mdi mdi-chili-hot menu-icon" },
            { 3, "mdi mdi-cake menu-icon" },
            { 4, "mdi mdi-baguette menu-icon" },
            { 5, "mdi mdi-shaker menu-icon" },
            { 6, "mdi mdi-food-steak menu-icon" },
            { 7, "mdi mdi-food-turkey menu-icon" },
            { 8, "mdi mdi-fish menu-icon" }
        };

        return cssList[Id];
    }
}
