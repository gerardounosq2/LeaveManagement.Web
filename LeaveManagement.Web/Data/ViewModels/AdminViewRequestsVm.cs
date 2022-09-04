using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Web.Data.ViewModels
{
	public class AdminViewRequestsVm
	{
		[Display(Name ="Total Number of Requests")]
		public int TotalRequests { get; set; }

		[Display(Name = "Approved Requests")]
		public int ApprovedRequests { get; set; }

		[Display(Name = "Pending Requests")]
		public int PendingRequests { get; set; }

		[Display(Name = "Rejected Requests")]
		public int RejectedRequests { get; set; }

		public IEnumerable<LeaveRequestVm> LeaveRequests { get; set; }
	}
}