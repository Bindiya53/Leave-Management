using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Leave_Management.Web.Models
{
    public class LeaveRequestCreateVM : IValidatableObject
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int LeaveTypeId { get; set; }
        public SelectList? LeaveType{get; set;}
        public string? RequestComments { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(StartDate > EndDate)
            {
                yield return new ValidationResult("Start Date Must Be Before End Date", new[] {nameof(StartDate), nameof(EndDate)});
            }
            if(RequestComments?.Length > 250)
            {
                yield return new ValidationResult("Comments are too logn", new[] {nameof(RequestComments)});
            }
        }
    }
}