using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Domain.Common;

namespace VkxDemoCleanArchitecture.Application.Permissions.Commands.Delete;
public class DeletePermissionCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeletePermissionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var permission = await _context.Permissions.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (permission == null)
            throw new Exception(ErrorCode.PERMISSION_NOT_FOUND);

        _context.Permissions.Remove(permission);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
