using LeaveManagement.Web.Data.Models;
using LeaveManagement.Web.Data.ViewModels;

namespace LeaveManagement.Web.Contracts
{
   public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
   {
      Task CreateLeaveRequest(LeaveRequestCreateVm model);
   }
}