namespace EStore.Domain.ViewModels;

using EStore.Domain.Common.Pagination;
using EStore.Domain.Responses;

public class ProductViewModel
{
    public required PaginationInfo Pagination { get; set; }

    public required ICollection<ProductResponse> Products { get; set; }

    public string CategoryDescription { get; set; } = string.Empty;
}
