using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LeaveManagement.Web.Data.Models;

namespace LeaveManagement.Web.Data.ViewModels
{
   public class LeaveRequestVm : LeaveRequestCreateVm
   {
      public int Id { get; set; }

      [Display(Name ="Leave Type")]
      public LeaveTypeViewModel LeaveType { get; set; }

      [Display(Name ="Date Requested")]
      public DateTime DateRequested { get; set; }

      public bool? Approved { get; set; }
      public bool Cancelled { get; set; }
   }
}