using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Data.Models;

namespace LeaveManagement.Web.Repositories
{
   public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
   {
      public LeaveRequestRepository(ApplicationDbContext context)
         : base(context)
      {
      }
   }
}