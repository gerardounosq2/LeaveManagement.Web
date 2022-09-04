using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagement.Web.Data.ViewModels
{
   public class LeaveRequestCreateVm : IValidatableObject
   {
      [Required]
      [Display(Name = "Start Date")]
      [DisplayFormat(DataFormatString = "{0:yyyy-MM-DD}")]
      [DataType(DataType.Date)]
      public DateTime? StartDate { get; set; }

      [Required]
      [Display(Name = "End Date")]
      [DisplayFormat(DataFormatString = "{0:yyyy-MM-DD}")]
      [DataType(DataType.Date)]
      public DateTime? EndDate { get; set; }

      [Required]
      [Display(Name = "Leave Type")]
      public int LeaveTypeId { get; set; }

      public SelectList LeaveTypes { get; set; }

      [Display(Name = "Comments")]
      public string? RequestComments { get; set; }

      public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
      {
         if (StartDate > EndDate)
         {
            yield return new ValidationResult("Start Date must be before End Date", new[] { nameof(StartDate), nameof(EndDate) });
         }
         if (RequestComments?.Length > 250)
         {
            yield return new ValidationResult("Comments are too long", new[] { nameof(RequestComments) });
         }
      }
   }
}