using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Application.Common.Models;
using VkxDemoCleanArchitecture.Application.Permissions.Queries.GetById;

namespace VkxDemoCleanArchitecture.Application.Permissions.Queries.GetsPaging;
public class GetsPagingPermissionQuery : IRequest<PaginatedList<GetPermissionByIdResponse>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetsPagingPermissionQueryHandler : IRequestHandler<GetsPagingPermissionQuery, PaginatedList<GetPermissionByIdResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetsPagingPermissionQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<GetPermissionByIdResponse>> Handle(GetsPagingPermissionQuery request, CancellationToken cancellationToken)
    {

        var query = _context.Permissions
             .Select(permission => new GetPermissionByIdResponse
             {
                 Id = permission.Id,
                 ActionId = permission.ActionId,
                 ObjectId = permission.ObjectId,
                 Method = permission.Method,
                 Endpoint = permission.Endpoint
             });

        return await PaginatedList<GetPermissionByIdResponse>.CreateAsync(query, request.PageNumber, request.PageSize);
    }
}
