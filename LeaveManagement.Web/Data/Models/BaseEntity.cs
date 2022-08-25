namespace LeaveManagement.Web.Data.Models
{
   public partial class BaseEntity
   {
      public int Id { get; set; }
      public DateTime DateCreated { get; set; }
      public DateTime DateModified { get; set; }
   }
}
