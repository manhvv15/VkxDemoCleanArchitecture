using Microsoft.Extensions.Options;
using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Application.Common.Models;

namespace VkxDemoCleanArchitecture.Application.Stocks.Queries.GetsPaging;
public class GetsPagingStockQuery : IRequest<PaginatedList<GetsPagingStockResponse>>
{
    public string Keyword { get; init; } = string.Empty;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetsPagingStockQueryHandler : IRequestHandler<GetsPagingStockQuery, PaginatedList<GetsPagingStockResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly AppSettingsOptions _options;

    public GetsPagingStockQueryHandler(IApplicationDbContext context, IOptions<AppSettingsOptions> options)
    {
        _context = context;
        _options = options.Value;
    }

    public async Task<PaginatedList<GetsPagingStockResponse>> Handle(GetsPagingStockQuery request, CancellationToken cancellationToken)
    {
        long marketCapOptions = _options.MarketCap;
        var query = _context.Stocks
            .Where(x => x.Symbol.Contains(request.Keyword))
            .Where(x => x.MarketCap > marketCapOptions)
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
