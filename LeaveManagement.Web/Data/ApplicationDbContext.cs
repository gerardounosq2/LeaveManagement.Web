using System.Reflection;
using LeaveManagement.Web.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Data
{
   public class ApplicationDbContext : IdentityDbContext<Employee>
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
      {
      }

      public DbSet<LeaveType> LeaveTypes { get; set; }
      public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
      public DbSet<LeaveRequest> LeaveRequests { get; set; }

      protected override void OnModelCreating(ModelBuilder builder)
      {
         base.OnModelCreating(builder);
         builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
      }

      public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
      {
         foreach (var entry in base.ChangeTracker.Entries<BaseEntity>().Where(r => r.State == EntityState.Added || r.State == EntityState.Modified))
         {
            entry.Entity.DateModified = DateTime.Now;
            if (entry.State == EntityState.Added)
            {
               entry.Entity.DateCreated = DateTime.Now;
            }
         }
         return base.SaveChangesAsync(cancellationToken);
      }
   }
}