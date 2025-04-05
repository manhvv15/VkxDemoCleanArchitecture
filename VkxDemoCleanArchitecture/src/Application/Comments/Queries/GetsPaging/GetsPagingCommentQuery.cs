using VkxDemoCleanArchitecture.Application.Common.Interfaces;

namespace VkxDemoCleanArchitecture.Application.Comments.Queries.GetsPaging;
public class GetsPagingCommentQuery : IRequest<List<GetsPagingCommentResponse>>
{
    public Guid? LastCommentId { get; set; } 
    public int PageSize { get; set; } = 10;
}


public class GetsPagingCommentQueryHandler : IRequestHandler<GetsPagingCommentQuery, List<GetsPagingCommentResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetsPagingCommentQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<GetsPagingCommentResponse>> Handle(GetsPagingCommentQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Comments.AsQueryable();

        if (request.LastCommentId.HasValue)
        {
            query = query.Where(c => c.Id.CompareTo(request.LastCommentId.Value) > 0);
        }

        var comments = await query
            .OrderBy(c => c.Id)
            .Take(request.PageSize)
            .Select(c => new GetsPagingCommentResponse
            {
                Id = c.Id,
                Title = c.Title,
                Content = c.Content,
                StockId = c.StockId
            })
            .ToListAsync(cancellationToken);

        return comments;
    }
}
