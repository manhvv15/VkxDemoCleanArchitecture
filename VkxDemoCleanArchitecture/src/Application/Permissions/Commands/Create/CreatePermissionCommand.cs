using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Application.Permissions.Queries.GetById;
using VkxDemoCleanArchitecture.Domain.Common;
using VkxDemoCleanArchitecture.Domain.Entities;

namespace VkxDemoCleanArchitecture.Application.Permissions.Commands;

public class CreatePermissionsCommand : IRequest<Unit>
{
    public List<GetPermissionByIdResponse> Permissions { get; set; } = new();
}

public class CreatePermissionsCommandHandler : IRequestHandler<CreatePermissionsCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public CreatePermissionsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreatePermissionsCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        foreach (var item in request.Permissions)
        {
            await ValidateRequestAsync(item, cancellationToken);

            var permission = new Permission
            {
                Id = Guid.NewGuid(),
                ActionId = item.ActionId,
                ObjectId = item.ObjectId,
                Method = item.Method,
                Endpoint = item.Endpoint
            };

            _context.Permissions.Add(permission);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    private async Task ValidateRequestAsync(GetPermissionByIdResponse item, CancellationToken cancellationToken)
    {
        var actionExists = await _context.Actions.AnyAsync(a => a.Id == item.ActionId, cancellationToken);
        if (!actionExists)
        {
            throw new Exception(ErrorCode.ACTION_NOT_FOUND);
        }

        var objectExists = await _context.Objects.AnyAsync(o => o.Id == item.ObjectId, cancellationToken);
        if (!objectExists)
        {
            throw new Exception(ErrorCode.OBJECT_NOT_FOUND);
        }
    }
}

