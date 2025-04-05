using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VkxDemoCleanArchitecture.Domain.Constants;

namespace VkxDemoCleanArchitecture.Domain.Entities;

[Table(TableNameConstants.Stock)]
public class Stock
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [StringLength(10)]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string CompanyName { get; set; } = string.Empty;
    [Range(0, double.MaxValue)]
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    [Required]
    [StringLength(100)]
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }
    public Guid UserId { get; set; }

    public virtual User? User { get; set; } = null!;
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
