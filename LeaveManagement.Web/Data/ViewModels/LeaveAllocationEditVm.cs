namespace LeaveManagement.Web.Data.ViewModels
{
   public class LeaveAllocationEditVm : LeaveAllocationVM
   {
      public string EmployeeId { get; set; }
      public int LeaveTypeId { get; set; }
      public EmployeeListVM Employee { get; set; }
   }
}