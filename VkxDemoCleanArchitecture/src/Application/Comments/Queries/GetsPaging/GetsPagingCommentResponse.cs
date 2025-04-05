namespace VkxDemoCleanArchitecture.Application.Comments.Queries.GetsPaging;
public class GetsPagingCommentResponse
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public Guid? StockId { get; set; }
}
