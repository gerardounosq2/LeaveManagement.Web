using LeaveManagement.Web.Data.Models;
using LeaveManagement.Web.Data.ViewModels;

namespace LeaveManagement.Web.Contracts
{
   public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
   {
      Task CreateLeaveRequest(LeaveRequestCreateVm model);
      Task<EmployeeLeaveRequestVm> GetMyLeaveDetails();
      Task<LeaveRequestVm?> GetLeaveRequestAsync(int? id);
      Task<IEnumerable<LeaveRequest>> GetAllAsync(string employeeId);
      Task<AdminViewRequestsVm> GetAdminLeaveRequestList();
      Task ChangeApprovalStatus(int leaveRequestId, bool approved);
   }
}