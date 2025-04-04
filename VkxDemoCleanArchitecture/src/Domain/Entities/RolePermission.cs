using System.ComponentModel.DataAnnotations.Schema;
using VkxDemoCleanArchitecture.Domain.Constants;

namespace VkxDemoCleanArchitecture.Domain.Entities;
[Table(TableNameConstants.RolePermission)]
public class RolePermission
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    public bool IsActive { get; set; } = true;

    public virtual Role Role { get; set; } = null!;
    public virtual Permission Permission { get; set; } = null!;
}
