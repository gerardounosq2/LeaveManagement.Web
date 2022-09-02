namespace LeaveManagement.Web.Data.ViewModels
{
   public class EmployeeAllocationVm : EmployeeListVM
   {
      public IEnumerable<LeaveAllocationVM> LeaveAllocations { get; set; }
   }
}