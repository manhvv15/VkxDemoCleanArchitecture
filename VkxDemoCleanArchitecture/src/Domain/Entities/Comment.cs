namespace VkxDemoCleanArchitecture.Domain.Entities;
public class Comment
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }

    public int? StockId { get; set; }
    public virtual Stock? Stock { get; set; }
}
