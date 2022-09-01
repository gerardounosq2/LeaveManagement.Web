using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Web.Data.Configurations
{
   public class UserSeedConfiguration : IEntityTypeConfiguration<Employee>
   {
      public void Configure(EntityTypeBuilder<Employee> builder)
      {
         var hasher = new PasswordHasher<Employee>();
         builder.HasData(
            new Employee
            {
               Id = "94fdc00f-e4f6-4c09-81bf-776862cad6eb",
               Email = "admin@localhost.com",
               NormalizedEmail = "ADMIN@LOCALHOST.COM",
               UserName = "admin@localhost.com",
               NormalizedUserName = "ADMIN@LOCALHOST.COM",
               FirstName = "System",
               LastName = "Admin",
               PasswordHash = hasher.HashPassword(null, "Pa$$w0rd"),
               EmailConfirmed = true
            },
            new Employee
            {
               Id = "3086a4c6-c709-4ae7-9434-5b945a6e8e81",
               Email = "user@localhost.com",
               NormalizedEmail = "USER@LOCALHOST.COM",
               UserName = "user@localhost.com",
               NormalizedUserName = "USER@LOCALHOST.COM",
               FirstName = "System",
               LastName = "User",
               PasswordHash = hasher.HashPassword(null, "Pa$$w0rd"),
               EmailConfirmed = true
            });
      }
   }
}