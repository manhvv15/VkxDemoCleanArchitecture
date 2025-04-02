using VkxDemoCleanArchitecture.Application.Common.Models;

namespace VkxDemoCleanArchitecture.Application.Stocks.Queries.GetsPaging;
public class GetsPagingStockQuery : IRequest<PaginatedList<GetsPagingStockResponse>>
{
    public string Keyword { get; init; } = string.Empty;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
