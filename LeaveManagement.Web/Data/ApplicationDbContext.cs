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

      protected override void OnModelCreating(ModelBuilder builder)
      {
         base.OnModelCreating(builder);
         builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
      }
   }
}