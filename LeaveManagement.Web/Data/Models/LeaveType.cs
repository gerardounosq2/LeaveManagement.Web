namespace LeaveManagement.Web.Data.Models
{
   public class LeaveType : BaseEntity
   {
      public string Name { get; set; }

      public int DefaultDays { get; set; }
   }
}