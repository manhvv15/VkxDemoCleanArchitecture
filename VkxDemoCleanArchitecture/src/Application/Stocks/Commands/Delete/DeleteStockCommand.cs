using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Domain.Common;

namespace VkxDemoCleanArchitecture.Application.Stocks.Command.Delete;
public class DeleteStockCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
public class DeleteStockCommandHandler : IRequestHandler<DeleteStockCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteStockCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteStockCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var entity = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null) throw new Exception(ErrorCode.STOCK_NOT_FOUND);

        _context.Stocks.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
