using VkxDemoCleanArchitecture.Domain.Entities;
using Action = VkxDemoCleanArchitecture.Domain.Entities.Action;
using Object = VkxDemoCleanArchitecture.Domain.Entities.Object;

namespace VkxDemoCleanArchitecture.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<Stock> Stocks { get; }
    DbSet<Comment> Comments { get; }
    DbSet<User> AppUsers { get; }
    DbSet<Role> AppRoles { get; }
    DbSet<UserRole> AppUserRoles { get; }
    DbSet<Action> Actions { get; }
    DbSet<Object> Objects { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<RolePermission> RolePermissions { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
