using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Domain.Common;

namespace VkxDemoCleanArchitecture.Application.Stocks.Command.Update;
public class UpdateStockCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }
}
public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateStockCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var entity = await _context.Stocks.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null) throw new Exception(ErrorCode.STOCK_NOT_FOUND);

        entity.Symbol = request.Symbol;
        entity.CompanyName = request.CompanyName;
        entity.Purchase = request.Purchase;
        entity.LastDiv = request.LastDiv;
        entity.Industry = request.Industry;
        entity.MarketCap = request.MarketCap;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
