namespace VkxDemoCleanArchitecture.Application.Comments.Commands;
public class CommentCreatedMessage
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public Guid? StockId { get; set; }
}
