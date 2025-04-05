using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Domain.Common;

namespace VkxDemoCleanArchitecture.Application.Permissions.Commands.Update;
public class UpdatePermissionCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid ActionId { get; set; }
    public Guid ObjectId { get; set; }
    public string? Method { get; set; }
    public string? Endpoint { get; set; }
}

public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdatePermissionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var permission = await _context.Permissions.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (permission == null)
            throw new Exception(ErrorCode.PERMISSION_NOT_FOUND);

        permission.ActionId = request.ActionId;
        permission.ObjectId = request.ObjectId;
        permission.Method = request.Method;
        permission.Endpoint = request.Endpoint;

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
