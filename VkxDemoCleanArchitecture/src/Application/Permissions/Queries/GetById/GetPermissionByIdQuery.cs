using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Domain.Common;

namespace VkxDemoCleanArchitecture.Application.Permissions.Queries.GetById;
public class GetPermissionByIdQuery : IRequest<GetPermissionByIdResponse>
{
    public Guid Id { get; set; }
}
public class GetPermissionByIdQueryHandler : IRequestHandler<GetPermissionByIdQuery, GetPermissionByIdResponse>
{
    private readonly IApplicationDbContext _context;

    public GetPermissionByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetPermissionByIdResponse> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var permission = await _context.Permissions
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (permission == null)
            throw new Exception(ErrorCode.PERMISSION_NOT_FOUND);

        return new GetPermissionByIdResponse
        {
            Id = permission.Id,
            ActionId = permission.ActionId,
            ObjectId = permission.ObjectId,
            Method = permission.Method,
            Endpoint = permission.Endpoint
        };
    }
}
