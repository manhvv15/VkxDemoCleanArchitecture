using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Domain.Common;
using VkxDemoCleanArchitecture.Domain.Entities;

namespace VkxDemoCleanArchitecture.Application.Comments.Commands;
public class CreateCommentCommand : IRequest<Unit>
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public Guid? StockId { get; set; }
}
public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public CreateCommentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var stockExists = await _context.Stocks.AnyAsync(x => x.Id == request.StockId, cancellationToken);
        if (!stockExists)
        {
            throw new ApplicationException(ErrorCode.STOCK_NOT_FOUND); 
        }
        var comment = new Comment
        {
            Title = request.Title,
            Content = request.Content,
            StockId = request.StockId,
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
