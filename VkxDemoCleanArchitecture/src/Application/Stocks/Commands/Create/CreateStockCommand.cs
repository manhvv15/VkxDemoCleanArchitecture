
using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Domain.Entities;

namespace VkxDemoCleanArchitecture.Application.Stocks.Command.Create;
public class CreateStockCommand : IRequest<Unit>
{
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }
    public Guid UserId { get; set; }
}

public class CreateStockCommandHandler : IRequestHandler<CreateStockCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public CreateStockCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(CreateStockCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var entity = new Stock
        {
            Symbol = request.Symbol,
            CompanyName = request.CompanyName,
            Purchase = request.Purchase,
            LastDiv = request.LastDiv,
            Industry = request.Industry,
            MarketCap = request.MarketCap,
            UserId = request.UserId
        };

        _context.Stocks.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
