using System.ComponentModel.DataAnnotations.Schema;
using VkxDemoCleanArchitecture.Domain.Constants;

namespace VkxDemoCleanArchitecture.Domain.Entities;
[Table(TableNameConstants.Object)]
public class Object
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty; 

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
