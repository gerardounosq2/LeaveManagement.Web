using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Web.Data.Configurations
{
   public class UserRoleSeedConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
   {
      public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
      {
         builder.HasData(
            new IdentityUserRole<string>
            {
               RoleId = "dceb4c48-51ed-42d0-a3af-b4978b74b770",
               UserId = "94fdc00f-e4f6-4c09-81bf-776862cad6eb"
            },
            new IdentityUserRole<string>
            {
               RoleId = "24bf64f3-b8e7-4e65-8033-38e2724341cb",
               UserId = "3086a4c6-c709-4ae7-9434-5b945a6e8e81"
            });
      }
   }
}