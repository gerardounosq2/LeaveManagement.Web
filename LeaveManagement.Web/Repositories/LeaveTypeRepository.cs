using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Data.Models;

namespace LeaveManagement.Web.Repositories
{
   public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
   {
      readonly ApplicationDbContext context;

      public LeaveTypeRepository(ApplicationDbContext context)
         : base(context)
      {
         this.context = context;
      }
   }
}