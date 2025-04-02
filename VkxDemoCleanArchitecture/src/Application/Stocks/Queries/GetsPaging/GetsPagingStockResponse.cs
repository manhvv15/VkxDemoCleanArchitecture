using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Application.Common.Models;

namespace VkxDemoCleanArchitecture.Application.Stocks.Queries.GetsPaging;
public class GetsPagingStockResponse
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }
}
public class GetsPagingStockQueryHandler : IRequestHandler<GetsPagingStockQuery, PaginatedList<GetsPagingStockResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetsPagingStockQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<GetsPagingStockResponse>> Handle(GetsPagingStockQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Stocks
            .Where(x => x.Symbol.Contains(request.Keyword))
            .Select(x => new GetsPagingStockResponse
            {
                Id = x.Id,
                Symbol = x.Symbol,
                CompanyName = x.CompanyName,
                Purchase = x.Purchase,
                LastDiv = x.LastDiv,
                Industry = x.Industry,
                MarketCap = x.MarketCap
            });

        return await PaginatedList<GetsPagingStockResponse>.CreateAsync(query, request.PageNumber, request.PageSize);
    }
}
