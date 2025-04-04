using System.ComponentModel.DataAnnotations.Schema;
using VkxDemoCleanArchitecture.Domain.Constants;

namespace VkxDemoCleanArchitecture.Domain.Entities;
[Table(TableNameConstants.Role)]
public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
