using LeaveManagement.Web.Data.Models;
using LeaveManagement.Web.Data.ViewModels;

namespace LeaveManagement.Web.Contracts
{
   public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
   {
      Task LeaveAllocation(int leaveTypeId);
      Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period);
      Task<EmployeeAllocationVm> GetEmployeeAllocations(string employeeId);
      Task<LeaveAllocationEditVm> GetEmployeeAllocation(int leaveAllocationId);
      Task<bool> UpdateEmployeeAllocation(LeaveAllocationEditVm model);
      Task<LeaveAllocation?> GetEmployeeAllocation(string employeeId, int leaveTypeId);
   }
}