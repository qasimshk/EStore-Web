namespace EStore.Domain.Abstraction.Service;

using EStore.Domain.Responses;
using EStore.Domain.ViewModels;

public interface IEStoreService
{
    Task<ProductViewModel?> GetProducts(int pageNumber, string categoryName);
    Task<ICollection<CategoryResponse>> GetCategory();
}
