using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Web.Data.ViewModels
{
   public class LeaveRequestVm : LeaveRequestCreateVm
   {
      public int Id { get; set; }

      [Display(Name = "Leave Type")]
      public LeaveTypeViewModel LeaveType { get; set; }

      [Display(Name = "Date Requested")]
      [DisplayFormat(DataFormatString = "{0:yyyy-MM-DD}")]
      [DataType(DataType.Date)]
      public DateTime DateRequested { get; set; }

      public bool? Approved { get; set; }
      public bool Cancelled { get; set; }

      public EmployeeListVM Employee { get; set; }

      public string? RequestingEmployeeId { get; set; }
   }
}