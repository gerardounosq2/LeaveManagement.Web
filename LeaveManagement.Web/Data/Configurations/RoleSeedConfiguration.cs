using LeaveManagement.Web.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Web.Data.Configurations
{
   public class RoleSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
   {
      public void Configure(EntityTypeBuilder<IdentityRole> builder)
      {
         builder.HasData(
            new IdentityRole
            {
               Id = "dceb4c48-51ed-42d0-a3af-b4978b74b770",
               Name = Roles.Administrator,
               NormalizedName = Roles.Administrator.ToUpper()
            },
            new IdentityRole
            {
               Id = "24bf64f3-b8e7-4e65-8033-38e2724341cb",
               Name = Roles.User,
               NormalizedName = Roles.User.ToUpper()
            });
      }
   }
}
