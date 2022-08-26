using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Web.Data.ViewModels
{
   public class LeaveTypeViewModel
   {
      public int Id { get; set; }

      [Required(ErrorMessage = "Required field")]
      [Display(Name = "Leave Type Name")]
      public string Name { get; set; }

      [Display(Name = "Default Number of Days")]
      [Range(1, 25, ErrorMessage = "Please enter a valid number")]
      public int DefaultDays { get; set; }
   }
}