namespace LeaveManagement.Web.Data.ViewModels
{
	public class EmployeeLeaveRequestVm
	{
		public EmployeeLeaveRequestVm(IEnumerable<LeaveAllocationVM> leaveAllocations, IEnumerable<LeaveRequestVm> leaveRequests)
		{
			LeaveAllocations = leaveAllocations;
			LeaveRequests = leaveRequests;
		}

		public IEnumerable<LeaveAllocationVM> LeaveAllocations { get; set; }
		public IEnumerable<LeaveRequestVm> LeaveRequests { get; set; }
	}
}