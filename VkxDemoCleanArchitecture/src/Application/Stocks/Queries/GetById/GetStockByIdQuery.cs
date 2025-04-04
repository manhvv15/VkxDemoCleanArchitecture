using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Domain.Common;

namespace VkxDemoCleanArchitecture.Application.Stocks.Queries.GetById;
public class GetStockByIdQuery : IRequest<GetStockByIdResponse>
{
    public Guid Id { get; set; }
}
public class GetStockByIdQueryHandler : IRequestHandler<GetStockByIdQuery, GetStockByIdResponse>
{
    private readonly IApplicationDbContext _context;

    public GetStockByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetStockByIdResponse> Handle(GetStockByIdQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var entity = await _context.Stocks.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null) throw new Exception(ErrorCode.STOCK_NOT_FOUND);

        return new GetStockByIdResponse
        {
            Id = entity.Id,
            Symbol = entity.Symbol,
            CompanyName = entity.CompanyName,
            Purchase = entity.Purchase,
            LastDiv = entity.LastDiv,
            Industry = entity.Industry,
            MarketCap = entity.MarketCap
        };
    }
}
