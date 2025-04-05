using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Domain.Common;

namespace VkxDemoCleanArchitecture.Application.Objects.Commands.Update;
public class UpdateObjectCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class UpdateObjectCommandHandler : IRequestHandler<UpdateObjectCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateObjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateObjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Objects.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null) throw new Exception(ErrorCode.OBJECT_NOT_FOUND);

        entity.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
