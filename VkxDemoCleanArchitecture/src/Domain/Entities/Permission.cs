using System.ComponentModel.DataAnnotations.Schema;
using VkxDemoCleanArchitecture.Domain.Constants;

namespace VkxDemoCleanArchitecture.Domain.Entities;
[Table(TableNameConstants.Permission)]
public class Permission
{
    public Guid Id { get; set; }
    public Guid ActionId { get; set; }
    public Guid ObjectId { get; set; }

    public virtual Action Action { get; set; } = null!;
    public virtual Object Object { get; set; } = null!;
    public string? Method { get; set; } 
    public string? Endpoint { get; set; } 

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
