using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Domain.Entities;
using VkxDemoCleanArchitecture.Infrastructure.Identity;

namespace VkxDemoCleanArchitecture.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Stock> Stocks => Set<Stock>();
    public DbSet<User> AppUsers => Set<User>();
    public DbSet<Role> AppRoles => Set<Role>();
    public DbSet<UserRole> AppUserRoles => Set<UserRole>();
    public DbSet<Domain.Entities.Action> Actions => Set<Domain.Entities.Action>();
    public DbSet<Domain.Entities.Object> Objects => Set<Domain.Entities.Object>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
